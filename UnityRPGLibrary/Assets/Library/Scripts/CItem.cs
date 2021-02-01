using System;
using UnityEngine;

[Serializable]
public class CItem : ObjectBehaviour
{
    // オブジェクト名
    [field: SerializeField]
    override protected string objectName {get; set;} = "";

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
        this.itemName = objList.GetStringState(objectName, initItemName);
        this.itemAmount = objList.GetIntState(objectName, initItemAmount);
    }

    // 開始時処理
    void Start()
    {
        AttachInit();
    }
}
