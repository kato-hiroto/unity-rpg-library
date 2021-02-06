using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Item : ObjectBehaviour
{
    // オブジェクト名
    [field: SerializeField]
    override public string objectName {get; protected set;} = "";

    // 初期値
    [field: SerializeField]
    public string itemName {get; private set;} = "";
    [field: SerializeField]
    public string skillName {get; private set;} = "";
    [field: SerializeField]
    public string description {get; private set;} = "";
    [field: SerializeField]
    public List<string> typeTag {get; private set;} = new List<string>();  // 何かしらの属性 (装備可能キャラの指定など)
    [field: SerializeField]
    public int initQuantity {get; private set;} = 0;
    [field: SerializeField]
    public float initLevel {get; private set;} = 0f;
    [field: SerializeField]
    public float initEffect {get; private set;} = 0f;
    [field: SerializeField]
    public float initCooltime {get; private set;} = 0f;
    [field: SerializeField]
    public bool usable {get; private set;} = true;
    [field: SerializeField]
    public bool consumable {get; private set;} = true;
    [field: SerializeField]
    public bool equippable {get; private set;} = true;
    [field: SerializeField]
    public bool executable {get; private set;} = true;
    [SerializeField]
    private ItemActionBehaviour useAction;   // 後でコモンMOBクラスに変更
    [SerializeField]
    private ItemActionBehaviour consumeAction;   // 後でコモンMOBクラスに変更
    [SerializeField]
    private ItemActionBehaviour equipAction;   // 後でコモンMOBクラスに変更
    [SerializeField]
    private ItemActionBehaviour removeAction;   // 後でコモンMOBクラスに変更
    [SerializeField]
    private ItemActionBehaviour executeAction;   // 後でコモンMOBクラスに変更

    // グローバル格納値
    [NonSerialized]
    public ObjectState<int> quantity;
    [NonSerialized]
    public ObjectState<float> level;
    [NonSerialized]
    public ObjectState<float> effect;
    [NonSerialized]
    public ObjectState<float> cooltime;

    // データロード時・初期処理
    override protected void Init()
    {
        quantity = varList.intMap.SyncState($"{uniqueID}_quantity", initQuantity);
        level = varList.floatMap.SyncState($"{uniqueID}_level", initLevel);
        effect = varList.floatMap.SyncState($"{uniqueID}_effect", initEffect);
        cooltime = varList.floatMap.SyncState($"{uniqueID}_cooltime", initCooltime);
    }

    // すべての初期処理終了後に呼ばれる関数
    override protected void AfterInit()
    {
    }

    // 「使用」時に実行される関数
    public void Use()
    {
        useAction.Execute(this);
    }

    // 「消費」時に実行される関数
    public void Consume()
    {
        consumeAction.Execute(this);
    }

    // 「装備をつける」時に実行される関数
    public void Equip()
    {
        equipAction.Execute(this);
    }

    // 「装備を外す」時に実行される関数
    public void Remove()
    {
        removeAction.Execute(this);
    }

    // 「技能実行」時に実行される関数
    public void Execute()
    {
        executeAction.Execute(this);
    }
}
