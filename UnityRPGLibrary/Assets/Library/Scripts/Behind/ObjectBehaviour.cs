using System;
using UnityEngine;

[Serializable]
abstract public class ObjectBehaviour : MonoBehaviour
{
    // オブジェクトID
    protected string uniqueId = "";

    // グローバル格納値
    protected ObjectStateList varList = ObjectStateList.getInstance();
    protected ObjectStream taskStream;

    // データロード時・初期処理
    abstract protected void Init();
    abstract protected void AfterInit();

    // IDの設定，初期処理開始
    protected void FirstSetting(string uniqueId)
    {
        this.uniqueId = uniqueId;
        taskStream = ObjectStream.getInstance();
        varList.AddInitTrigger(uniqueId, Init);
        Init();
        this.enabled = true;
    }

    // AfterInitをvarListに関連付ける，初期処理終了
    protected void SecondSetting()
    {
        if (uniqueId == "") return;
        varList.AddAfterTrigger(uniqueId, AfterInit);
        AfterInit();
        this.enabled = false;
    }

    void Update()
    {
        SecondSetting();
    }
}
