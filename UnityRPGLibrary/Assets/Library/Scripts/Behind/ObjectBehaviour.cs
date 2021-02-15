using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
abstract public class ObjectBehaviour<T> : MonoBehaviour
{
    // オブジェクトID
    [field: SerializeField]
    public string uniqueId {get; private set;} = "";

    // 基礎ステータス
    [field: SerializeField]
    public T status {get; protected set;}

    // グローバル格納値
    protected static ObjectStateList varList;
    protected static ObjectStream taskStream;

    // データロード時・初期処理
    abstract protected void Init();
    abstract public void Setting(string initUniqueId, T s);

    // Awake is called when the script instance is being loaded.
    void Awake()
    {
        if (taskStream == null)
        {
            taskStream = ObjectStream.getInstance();
        }
        if (varList == null)
        {
            varList = taskStream.varList;
        }
    }

    // Start is called on the frame when a script is enabled just before any of the Update methods is called the first time.
    void Start()
    {
        SetID(this.uniqueId);
    }

    // IDの設定，Initの登録
    protected void SetID(string initUniqueId)
    {
        if (initUniqueId == "") return;
        this.uniqueId = initUniqueId;
        varList.AddInitTrigger(uniqueId, Init);
        Init();
    }
}
