using System;
using UnityEngine;

[Serializable]
public class Test : ObjectBehaviour
{
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

    // ステータスの挿入
    public void SetStatus(string uniqueId)
    {
        // FirstSetting(uniqueId);
    }

    // データロード時・初期処理
    override protected void Init()
    {
        itemName = varList.stringMap.SyncState(uniqueId, initItemName);
        itemAmount = varList.intMap.SyncState(uniqueId, initItemAmount);
        itemAmount.AddSetTrigger($"{uniqueId}_itemAmountSet", (int value)=>{
            Debug.Log($"アイテム数が{value}個になりました。");
        });
        Debug.Log($"{this.name} {uniqueId} Init 実行！");
    }

    // すべての初期処理終了後に呼ばれる関数
    override protected void AfterInit(){
        // taskStream.AddTimer($"{uniqueId}_timing1", 5, ()=>{
        //     varList.Load("testfile2");
        // });
        taskStream.AddTimer($"{uniqueId}_timing2", 5, ()=>{
            Instantiate(this, this.transform.position + Vector3.right, Quaternion.identity);
            Debug.Log($"{this.name} {uniqueId} 複製実行！");
        });
        taskStream.AddTimer($"{uniqueId}_timing3", 10, ()=>{
            itemAmount.SetValue(15);
            varList.Save("testfile2");
        });
        // taskStream.AddTimer($"{uniqueId}_timing4", 20, ()=>{
        //     itemAmount.SetValue(20);
        //     varList.Save("testfile2");
        // });
        // taskStream.AddLoop($"{uniqueId}_loop", ()=>{
        //     Debug.Log($"現在: アイテム数{itemAmount.GetValue()}個");
        // });
        Debug.Log($"{this.name} {uniqueId} AfterInit 実行！");
    }
}
