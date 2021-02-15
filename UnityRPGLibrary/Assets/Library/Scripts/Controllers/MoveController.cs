using System;
using UnityEngine;
using UnityEngine.AI;

public class MoveController : ObjectBehaviour<Character>, IMove
{
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
    public ObjectBehaviour<IMove>[] controllers;

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
        controllers = GetComponents<ObjectBehaviour<IMove>>();
        foreach(var elem in controllers)
        {
            elem.Setting(uniqueId, this);
        }
    }

    // 移動中
    private void MoveLoop()
    {
        
        var moveDir = path.corners[0] - nowPos.GetValue();
        var moveVec = moveDir.normalized * moveSpeed.GetValue() * Time.deltaTime;
        var edge = moveDir.magnitude < moveVec.magnitude;
        moveVec = edge ? moveDir : moveVec;
        nowPos.SetValue(nowPos.GetValue() + moveVec);
        nowRot.SetValue(Quaternion.FromToRotation(Vector3.down, moveVec).eulerAngles);
        if (edge)
        {
            Move(endPos.GetValue());
        }
    }

    // 移動 1/speed 秒経過
    private void MoveEnd()
    {
        Move(endPos.GetValue());
    }

    // targetPosまである速度で移動
    public void Move(Vector3 targetPos)
    {
        path = new NavMeshPath();
        bool result = NavMesh.CalculatePath(nowPos.GetValue(), targetPos, NavMesh.AllAreas, path);
        if (result)
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

    // 移動停止
    public Vector3 GetNowPosition()
    {
        return nowPos.GetValue();
    }
}

public interface IMove
{
    void Move(Vector3 targetPos);
    void Stop();
    Vector3 GetNowPosition();
}
