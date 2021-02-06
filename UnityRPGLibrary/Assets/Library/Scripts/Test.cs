using System;
using UnityEngine;

[Serializable]
public class Test : ObjectBehaviour
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
        itemName = varList.stringMap.SyncState(uniqueID, initItemName);
        itemAmount = varList.intMap.SyncState(uniqueID, initItemAmount);
        itemAmount.AddSetTrigger($"{uniqueID}_itemAmountSet", (int value)=>{
            Debug.Log($"アイテム数が{value}個になりました。");
        });
        Debug.Log($"{this.name} {uniqueID} Init 実行！");
    }

    // すべての初期処理終了後に呼ばれる関数
    override protected void AfterInit(){
        // taskStream.AddTimer($"{uniqueID}_timing1", 5, ()=>{
        //     varList.Load("testfile2");
        // });
        taskStream.AddTimer($"{uniqueID}_timing2", 5, ()=>{
            Instantiate(this, this.transform.position + Vector3.right, Quaternion.identity);
            Debug.Log($"{this.name} {uniqueID} 複製実行！");
        });
        taskStream.AddTimer($"{uniqueID}_timing3", 10, ()=>{
            itemAmount.SetValue(15);
            varList.Save("testfile2");
        });
        // taskStream.AddTimer($"{uniqueID}_timing4", 20, ()=>{
        //     itemAmount.SetValue(20);
        //     varList.Save("testfile2");
        // });
        // taskStream.AddLoop($"{uniqueID}_loop", ()=>{
        //     Debug.Log($"現在: アイテム数{itemAmount.GetValue()}個");
        // });
        Debug.Log($"{this.name} {uniqueID} AfterInit 実行！");
    }
}
