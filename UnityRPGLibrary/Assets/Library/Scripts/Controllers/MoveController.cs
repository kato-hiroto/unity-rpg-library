using System;
using UnityEngine;
using UnityEngine.AI;

public class MoveController : ObjectBehaviour<ICharacter>, IMoveController
{
    // セーブデータ
    [NonSerialized]
    public ObjectState<float> moveSpeed;    // 移動速度
    [NonSerialized]
    public ObjectState<Vector3> startPos;   // 開始位置
    [NonSerialized]
    public ObjectState<Vector3> endPos;     // 移動目標
    [NonSerialized]
    public ObjectState<Vector3> nowPos;     // 描画位置
    [NonSerialized]
    public ObjectState<Vector3> nowRot;     // 描画角度
    [NonSerialized]
    public ObjectState<bool> moving;        // 移動中

    // コントローラ
    [NonSerialized]
    public ObjectBehaviour<IMoveController>[] controllers;

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
        startPos = varList.vectorMap.SyncState($"{uniqueId}/sp", transform.position);
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
        controllers = GetComponents<ObjectBehaviour<IMoveController>>();
        foreach(var elem in controllers)
        {
            elem.Setting(uniqueId, this);
        }
    }

    // 移動中
    private void MoveLoop()
    {
        // 移動ベクトルの計算
        if (path.corners.Length < 2) return;
        var moveDir = path.corners[1] - nowPos.GetValue();
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

    // targetPosまである速度で移動
    public void Move(Vector3 objectPos, Vector3 targetPos)
    {
        path = new NavMeshPath();
        bool result = NavMesh.CalculatePath(objectPos, targetPos, NavMesh.AllAreas, path);
        if (!result || path.corners.Length < 2) return;
        startPos.SetValue(nowPos.GetValue());
        endPos.SetValue(targetPos);
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

public interface IMoveController
{
    void Move(Vector3 objectPos, Vector3 targetPos);
    void Rotate(Vector3 direction);
    void Stop();
    Vector3 GetNowPosition();
}
