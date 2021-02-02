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
    // [NonSerialized]
    public ObjectState<string> itemName;
    // [NonSerialized]
    public ObjectState<int> itemAmount;

    // 適当
    float timer = 0;
    bool loadFinished = false;
    bool saveFinished = false;
    bool saveFinished2 = false;
    bool saveFinished3 = false;

    // データロード時・初期処理
    override protected void Init()
    {
        itemName = objList.stringMap.SyncState(objectName, initItemName);
        itemAmount = objList.intMap.SyncState(objectName, initItemAmount);
        itemAmount.AddSetTrigger($"{objectName}_itemAmountSet", ChangeItemAmount);
    }

    // すべての初期処理終了後に呼ばれる関数
    override protected void AfterInit(){}

    // 開始時処理
    void Start()
    {
        AttachInit();
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 1f && !loadFinished) {
            objList.Load("testfile2");
            loadFinished = true;
        }
        if (timer > 3f && !saveFinished) {
            itemAmount.SetValue(5);
            objList.Save("testfile2");
            saveFinished = true;
        }
        if (timer > 5f && !saveFinished2) {
            itemAmount.SetValue(10);
            objList.Save("testfile2");
            saveFinished2 = true;
        }
        if (timer > 7f && !saveFinished3) {
            itemAmount.SetValue(20);
            objList.Save("testfile2");
            saveFinished3 = true;
        }
        Debug.Log($"現在: アイテム数{itemAmount.GetValue()}個");
    }

    // 変更時処理
    void ChangeItemAmount(int value) {
        Debug.Log($"アイテム数が{value}個になりました。");
    }
}
