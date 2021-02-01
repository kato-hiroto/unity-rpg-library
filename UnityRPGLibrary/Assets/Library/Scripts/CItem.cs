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

    // 適当
    // float timer = 0;

    // データロード時・初期処理
    override protected void Init()
    {
        this.itemName = objList.GetStringState(objectName, initItemName);
        this.itemAmount = objList.GetIntState(objectName, initItemAmount);
        // this.itemAmount.AddSetTrigger($"{objectName}_itemAmountSet", this.ChangeItemAmount);
    }

    // 開始時処理
    void Start()
    {
        AttachInit();
    }

    void Update()
    {
        // timer += Time.deltaTime;
        // if (timer > 3f) {
        //     this.itemAmount.SetValue(5);
        // }
    }

    // 変更時処理
    // void ChangeItemAmount(int value) {
    //     Debug.Log($"アイテム数が{value}個になりました。");
    // }
}
