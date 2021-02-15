using System;
using UnityEngine;
using UnityEngine.AI;

public class InputDirController : ObjectBehaviour<InputDirAdapter>
{
    // セーブデータ
    [NonSerialized]
    public ObjectState<float> inputDir; // 入力方向
    [NonSerialized]
    public ObjectState<bool> receive;   // 入力受付

    // データロード時の初期化処理
    override protected void Init(){}

    // 呼び出し時の初期化処理
    public override void Setting(string uId, InputDirAdapter s)
    {
        this.status = s;
        SetID($"{uId}/inputDir");

        // Stateの同期
        inputDir = varList.floatMap.SyncState($"{uniqueId}/id", 0f);
        receive = taskStream.AddLoop($"{uniqueId}/re", ReceiveLoop);

        // 受け付け開始，永続
        receive.SetValue(true);
    }

    // 入力受付
    private void ReceiveLoop()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        var moveDir = new Vector3(x, y, 0f);
        // Debug.Log($"moveDir: {moveDir}");
        if (moveDir.magnitude > 0.2f)
        {
            status.Move(status.GetNowPosition() + moveDir);
        }
    }
}

public interface InputDirAdapter
{
    void Move(Vector3 targetPos);
    void Stop();
    Vector3 GetNowPosition();
}
