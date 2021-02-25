using System;
using UnityEngine;

public class InputController : ObjectBehaviour<ICharacter>
{
    // ウィンドウ入力
    [NonSerialized]
    public ObjectState<bool> decide;        // 決定
    [NonSerialized]
    public ObjectState<bool> cancel;        // キャンセル
    [NonSerialized]
    public ObjectState<Vector3> move;       // カーソル移動

    // フィールド入力
    [NonSerialized]
    public ObjectState<bool> fire;          // 攻撃アクション
    [NonSerialized]
    public ObjectState<bool> check;         // 調査アクション
    [NonSerialized]
    public ObjectState<float> zRot;         // 入力角度: 下から時計回りに0~360度
    [NonSerialized]
    public ObjectState<bool> receiveLoop;   // 入力受付

    // コントローラ
    [NonSerialized]
    public ObjectBehaviour<InputController>[] controllers;

    // データロード時の初期化処理
    override protected void Init(){}

    // 呼び出し時の初期化処理
    public override void Setting(string uId, ICharacter s)
    {
        this.status = s;
        SetID($"{uId}/inputDir");

        // Stateの同期
        decide = varList.boolMap.SyncState($"{uniqueId}/dc", false);
        cancel = varList.boolMap.SyncState($"{uniqueId}/ca", false);
        move = varList.vectorMap.SyncState($"{uniqueId}/mv", Vector3.zero);
        fire = varList.boolMap.SyncState($"{uniqueId}/fr", false);
        check = varList.boolMap.SyncState($"{uniqueId}/ch", false);
        zRot = varList.floatMap.SyncState($"{uniqueId}/zr", 0f);
        receiveLoop = taskStream.AddLoop($"{uniqueId}/rl", ReceiveLoop);

        // 依存コントローラのセッティング
        controllers = GetComponents<ObjectBehaviour<InputController>>();
        foreach(var elem in controllers)
        {
            elem.Setting(uniqueId, this);
        }

        // 受け付け開始，永続
        receiveLoop.SetValue(true);
    }

    // フィールド入力受付
    private void ReceiveLoop()
    {
        // クリック
        fire.SetValue(Input.GetButtonDown("LeftClick"));
        check.SetValue(Input.GetButtonDown("RightClick"));

        // 移動方向の決定
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        zRot.SetValue(x * y == 0 ? zRot.GetValue() : Mathf.Atan2(x, -y) * 180f / Mathf.PI);
    }

    // ウィンドウ入力受け付け
    void Update()
    {
        // クリック
        decide.SetValue(Input.GetButtonDown("LeftClick") || Input.GetButtonDown("RightClick"));
        cancel.SetValue(Input.GetButtonDown("Menu"));

        // 移動方向の決定
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        move.SetValue(new Vector3(x, y, 0f));
    }
}
