using System;
using UnityEngine;
using UnityEngine.AI;

public class InputDirController : ObjectBehaviour<IMoveController>
{
    // セーブデータ
    [NonSerialized]
    public ObjectState<float> inputDir; // 入力方向
    [NonSerialized]
    public ObjectState<bool> receive;   // 入力受付

    // コントローラ
    [NonSerialized]
    public ObjectBehaviour<IInputDirController>[] controllers;

    // コンポーネント
    private NavMeshHit hit;
    private NavMeshObstacle navMeshObstacle;

    // 設定値
    private float scaleEps = 0.1f;
    private float moveDistance = 0.1f;
    private float obstacleSize;

    // データロード時の初期化処理
    override protected void Init(){}

    // 呼び出し時の初期化処理
    public override void Setting(string uId, IMoveController s)
    {
        this.status = s;
        SetID($"{uId}/inputDir");

        // Stateの同期
        inputDir = varList.floatMap.SyncState($"{uniqueId}/id", 0f);
        receive = taskStream.AddLoop($"{uniqueId}/re", ReceiveLoop);

        // コンポーネント取得
        navMeshObstacle = GetComponent<NavMeshObstacle>();
        obstacleSize = navMeshObstacle != null ? navMeshObstacle.radius + scaleEps : 0f;
        Debug.Log($"obstacleSize: {obstacleSize}");

        // 受け付け開始，永続
        receive.SetValue(true);
    }

    // 入力受付
    private void ReceiveLoop()
    {
        // 移動方向の決定
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        var moveDir = new Vector3(x, y, 0f);
        if (moveDir.magnitude < Mathf.Epsilon)
        {
            status.Stop();
            return;
        }

        // 開始位置・停止位置の決定
        var dir = moveDir.normalized;
        var nowPos = status.GetNowPosition() + dir * obstacleSize;  // 自身の障害物の外側
        var endPos = nowPos + dir * moveDistance;
        if (!NavMesh.Raycast(nowPos, endPos, out hit, NavMesh.AllAreas))
        {
            status.Move(nowPos, endPos);
        }
        else
        {
            Debug.Log($"nowPos:{nowPos}, endPos:{endPos}");
            status.Stop();
        }
    }
}

public interface IInputDirController
{}
