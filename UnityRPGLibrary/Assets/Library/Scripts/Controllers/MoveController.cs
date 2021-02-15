using System;
using UnityEngine;
using UnityEngine.AI;

public class MoveController : ObjectBehaviour<Character>, InputDirAdapter
{
    static float EPS = 0.001f;

    // セーブデータ
    [NonSerialized]
    public ObjectState<float> moveSpeed;    // 移動速度
    [NonSerialized]
    public ObjectState<Vector3> startPos;     // 開始位置
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
    public ObjectBehaviour<InputDirAdapter>[] controllers;

    // コンポーネント
    private NavMeshPath path;

    // データロード時の初期化処理
    override protected void Init(){}

    // 呼び出し時の初期化処理
    public override void Setting(string uId, Character s)
    {
        status = s;
        SetID($"{uId}/move");

        // Stateの同期
        moveSpeed = varList.floatMap.SyncState($"{uniqueId}/ms", s.status.initMoveSpeed);
        startPos = varList.vectorMap.SyncState($"{uniqueId}/sp", transform.position);
        endPos = varList.vectorMap.SyncState($"{uniqueId}/ep", transform.position);
        nowPos = varList.vectorMap.SyncState($"{uniqueId}/np", transform.position);
        nowRot = varList.vectorMap.SyncState($"{uniqueId}/nr", transform.rotation.eulerAngles);
        moving = taskStream.AddTimer($"{uniqueId}/rc", 1f / moveSpeed.GetValue(), MoveLoop, MoveEnd);

        // statusへの逆同期
        nowPos.AddTrigger($"{uniqueId}", (x)=>{
            status.transform.position = x;
        });
        nowRot.AddTrigger($"{uniqueId}", (x)=>{
            status.transform.rotation = Quaternion.Euler(x);
        });

        // Setting
        controllers = GetComponents<ObjectBehaviour<InputDirAdapter>>();
        foreach(var elem in controllers)
        {
            elem.Setting(uniqueId, this);
        }
    }

    // 移動中
    private void MoveLoop()
    {
        if (path.corners.Length < 2) return;
        var moveDir = path.corners[1] - nowPos.GetValue();
        var moveVec = moveDir.normalized * moveSpeed.GetValue() * Time.deltaTime;
        var edge = moveDir.magnitude < moveVec.magnitude;
        moveVec = edge ? moveDir : moveVec;
        nowPos.SetValue(nowPos.GetValue() + moveVec);
        if (moveVec.magnitude > EPS)
        {
            float zRot = Mathf.Atan2(moveVec.x, -moveVec.y) * 180f / Mathf.PI;
            nowRot.SetValue(new Vector3(0f, 0f, zRot));
        }
        if (edge) Move(endPos.GetValue());
    }

    // 移動 1/speed 秒経過
    private void MoveEnd()
    {
        var vec = endPos.GetValue() - nowPos.GetValue();
        if (vec.magnitude > EPS) Move(endPos.GetValue());
    }

    // targetPosまである速度で移動
    public void Move(Vector3 targetPos)
    {
        path = new NavMeshPath();
        bool result = NavMesh.CalculatePath(nowPos.GetValue(), targetPos, NavMesh.AllAreas, path);
        if (result && path.corners.Length > 1)
        {
            startPos.SetValue(nowPos.GetValue());
            endPos.SetValue(targetPos);
            taskStream.StartTimer(moving.GetName(), 1f / moveSpeed.GetValue());
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
