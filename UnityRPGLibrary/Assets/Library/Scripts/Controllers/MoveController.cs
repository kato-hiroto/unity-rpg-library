using System;
using UnityEngine;
using UnityEngine.AI;

public class MoveController : ObjectBehaviour<ICharacter>
{
    // セーブデータ
    [NonSerialized]
    public ObjectState<float> moveSpeed;    // 移動速度
    [NonSerialized]
    public ObjectState<Vector3> nxtPos;     // 次の移動位置
    [NonSerialized]
    public ObjectState<Vector3> endPos;     // 最終目標
    [NonSerialized]
    public ObjectState<Vector3> nowPos;     // 描画位置
    [NonSerialized]
    public ObjectState<Vector3> nowRot;     // 描画角度
    [NonSerialized]
    public ObjectState<bool> moving;        // 移動中

    // コントローラ
    [NonSerialized]
    public ObjectBehaviour<MoveController>[] controllers;

    // コンポーネント
    private NavMeshPath path;

    // データロード時の初期化処理
    override protected void Init(){}

    // 呼び出し時の初期化処理
    public override void Setting(string uId, ICharacter s)
    {
        status = s;
        SetID($"{uId}/move");

        // Stateの同期
        moveSpeed = varList.floatMap.SyncState($"{uniqueId}/ms", s.speed);
        nxtPos = varList.vectorMap.SyncState($"{uniqueId}/sp", transform.position);
        endPos = varList.vectorMap.SyncState($"{uniqueId}/ep", transform.position);
        nowPos = varList.vectorMap.SyncState($"{uniqueId}/np", transform.position);
        nowRot = varList.vectorMap.SyncState($"{uniqueId}/nr", transform.rotation.eulerAngles);
        moving = taskStream.AddTimer($"{uniqueId}/rc", 1f / moveSpeed.GetValue(), MoveLoop, MoveEnd);

        // statusへの逆同期
        nowPos.AddTrigger($"{uniqueId}", (x)=>{
            status.position = x;
        });
        nowRot.AddTrigger($"{uniqueId}", (x)=>{
            status.rotation = Quaternion.Euler(x);
        });

        // Setting
        controllers = GetComponents<ObjectBehaviour<MoveController>>();
        foreach(var elem in controllers)
        {
            elem.Setting(uniqueId, this);
        }
    }

    // targetPosに近接するために1グリッド移動
    public void Move(Vector3 objectPos, Vector3 targetPos)
    {
        // ルートの確認
        Stop();
        path = new NavMeshPath();
        bool result = NavMesh.CalculatePath(objectPos, targetPos, NavMesh.AllAreas, path);
        if (!result || path.corners.Length < 2) return;

        // グリッド配置
        var nowGrid = new Vector3(Mathf.Round(objectPos.x), Mathf.Round(objectPos.y), 0f);
        var pthGrid = new Vector3(Mathf.Round(path.corners[1].x), Mathf.Round(path.corners[1].y), 0f);
        var endGrid = new Vector3(Mathf.Round(targetPos.x), Mathf.Round(targetPos.y), 0f);
        if (nowGrid.Equals(pthGrid)) return;

        // 次のグリッドの決定
        var diff = pthGrid - nowGrid;
        var abs = new Vector3(Mathf.Abs(diff.x), Mathf.Abs(diff.y), 0f);
        var sign = new Vector3(Mathf.Sign(diff.x), Mathf.Sign(diff.y), 0f);
        var dir = abs.x == abs.y ? sign : (abs.x > abs.y ? Vector3.right * sign.x : Vector3.up * sign.y);

        // 計算値の設定
        nxtPos.SetValue(nowGrid + dir);
        endPos.SetValue(endGrid);
        taskStream.StartTimer(moving.GetName(), 1f / moveSpeed.GetValue());
    }

    // 向きの変更
    public void Rotate(Vector3 direction)
    {
        if (direction.magnitude > Mathf.Epsilon)
        {
            float zRot = Mathf.Atan2(direction.x, -direction.y) * 180f / Mathf.PI;
            nowRot.SetValue(new Vector3(0f, 0f, zRot));
        }
    }

    // 移動中
    private void MoveLoop()
    {
        // 移動ベクトルの計算
        var moveDir = nxtPos.GetValue() - nowPos.GetValue();
        var moveVec = moveDir.normalized * moveSpeed.GetValue() * Time.deltaTime;
        var edge = moveDir.magnitude < moveVec.magnitude;
        moveVec = edge ? moveDir : moveVec;

        // 位置の書き換え
        nowPos.SetValue(nowPos.GetValue() + moveVec);
        Rotate(moveVec);

        // パスの端なら再計算
        if (edge) Move(nowPos.GetValue(), endPos.GetValue());
    }

    // 一定時間ごとにパスを再計算
    private void MoveEnd()
    {
        Move(nowPos.GetValue(), endPos.GetValue());
    }

    // 移動停止
    public void Stop()
    {
        moving.SetValue(false);
    }

    // 現在位置の取得
    public Vector3 GetNowPosition()
    {
        return nowPos.GetValue();
    }
}
