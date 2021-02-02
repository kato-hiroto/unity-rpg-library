using System;
using UnityEngine;

[Serializable]
public class CItem : ObjectBehaviour
{
    // オブジェクト名
    [field: SerializeField]
    override public string objectName {get; protected set;} = "";

    // 初期値
    [SerializeField]
    private string initItemName = "";
    [SerializeField]
    private int initItemAmount = 0;

    // グローバル格納値
    [NonSerialized]
    public ObjectState<string> itemName;
    [NonSerialized]
    public ObjectState<int> itemAmount;

    // データロード時・初期処理
    override protected void Init()
    {
        itemName = varList.stringMap.SyncState(objectName, initItemName);
        itemAmount = varList.intMap.SyncState(objectName, initItemAmount);
        itemAmount.AddSetTrigger($"{objectName}_itemAmountSet", (int value)=>{
            Debug.Log($"アイテム数が{value}個になりました。");
        });
    }

    // すべての初期処理終了後に呼ばれる関数
    override protected void AfterInit(){
        Debug.Log("AfterInit 実行！");
    }

    // 開始時処理
    void Start()
    {
        AttachInit();
        taskStream.AddTimer($"{objectName}_timing1", 5, ()=>{
            varList.Load("testfile2");
        });
        taskStream.AddTimer($"{objectName}_timing2", 20, ()=>{
            itemAmount.SetValue(5);
            varList.Save("testfile2");
        });
        taskStream.AddTimer($"{objectName}_timing3", 15, ()=>{
            itemAmount.SetValue(10);
            varList.Save("testfile2");
        });
        taskStream.AddTimer($"{objectName}_timing4", 10, ()=>{
            itemAmount.SetValue(20);
            varList.Save("testfile2");
        });
        taskStream.AddLoop($"{objectName}_loop", ()=>{
            Debug.Log($"現在: アイテム数{itemAmount.GetValue()}個");
        });
    }

    void Update()
    {

    }
}
