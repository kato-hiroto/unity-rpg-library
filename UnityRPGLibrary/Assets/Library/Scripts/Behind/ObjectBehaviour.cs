using System;
using UnityEngine;
using System.Collections.Generic;

[Serializable]
abstract public class ObjectBehaviour : MonoBehaviour
{
    // オブジェクトID
    public string uniqueId {get; private set;} = "";

    // グローバル格納値
    protected ObjectStateList varList = ObjectStateList.getInstance();
    protected ObjectStream taskStream;

    // データロード時・初期処理
    abstract protected void Init();
    abstract protected void AfterInit();

    // Awake is called when the script instance is being loaded.
    void Awake()
    {
        taskStream = ObjectStream.getInstance();
    }

    // IDの設定，Initの登録
    protected void StartSetting(string uniqueId)
    {
        this.uniqueId = uniqueId;
        varList.AddInitTrigger(uniqueId, Init);
        Init();
        this.enabled = true;
    }

    // AfterInitの登録
    private void EndSetting()
    {
        if (uniqueId == "") return;
        varList.AddAfterTrigger(uniqueId, AfterInit);
        AfterInit();
        this.enabled = false;
    }

    // EndSetting 実行専用
    void Update()
    {
        EndSetting();
    }
}
