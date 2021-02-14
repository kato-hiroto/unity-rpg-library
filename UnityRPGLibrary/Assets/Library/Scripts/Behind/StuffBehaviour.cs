using System;
using UnityEngine;

[Serializable]
abstract public class StuffBehaviour : MonoBehaviour
{
    // オブジェクトID
    [field: SerializeField]
    public string uniqueId {get; private set;} = "";

    // グローバル格納値
    protected ObjectStateList varList = ObjectStateList.getInstance();

    // データロード時・初期処理
    abstract protected void Init();

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
