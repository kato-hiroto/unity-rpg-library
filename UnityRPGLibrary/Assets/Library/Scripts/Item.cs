using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Item : ObjectBehaviour
{
    // オブジェクト名
    [field: SerializeField]
    override public string objectName {get; protected set;} = "";

    // 基本情報
    [field: SerializeField]
    public string itemName {get; private set;} = "";
    [field: SerializeField]
    public string skillName {get; private set;} = "";
    [field: SerializeField]
    public string description {get; private set;} = "";
    [field: SerializeField]
    public List<ItemTag> itemTags {get; private set;} = new List<ItemTag>();  // 何かしらの属性 (装備可能キャラの指定など)

    // 初期値
    [field: SerializeField]
    public int initQuantity {get; private set;} = 0;
    [field: SerializeField]
    public float initLevel {get; private set;} = 0f;

    // 設定パラメータ
    [field: SerializeField]
    public int worth {get; private set;} = 0;
    [field: SerializeField]
    public float effect {get; private set;} = 0f;
    [field: SerializeField]
    public int range {get; private set;} = 0;
    [field: SerializeField]
    public EffectShape effectShape {get; private set;} = EffectShape.None;
    [field: SerializeField]
    public float cooltime {get; private set;} = 0f;

    // イベント
    [SerializeField]
    public EventBehaviour useAction = null;
    [SerializeField]
    public EventBehaviour consumeAction = null;
    [SerializeField]
    public EventBehaviour equipAction = null;
    [SerializeField]
    public EventBehaviour removeAction = null;
    [SerializeField]
    public EventBehaviour executeAction = null;

    // グローバル格納値
    [NonSerialized]
    public ObjectState<int> quantity;
    [NonSerialized]
    public ObjectState<float> level;

    // データロード時・初期処理
    override protected void Init()
    {
        quantity = varList.intMap.SyncState($"{uniqueID}_q", initQuantity);
        level = varList.floatMap.SyncState($"{uniqueID}_l", initLevel);
    }

    // すべての初期処理終了後に呼ばれる関数
    override protected void AfterInit()
    {
    }

    // 「使用」時に実行される関数
    public void Use(Character character)
    {
        if (useAction == null) return;
        useAction.AtExecute(character, null, this);
    }

    // 「消費」時に実行される関数
    public void Consume(Character character)
    {
        if (consumeAction == null) return;
        consumeAction.AtExecute(character, null, this);
    }

    // 「装備をつける」時に実行される関数
    public void Equip(Character character)
    {
        if (equipAction == null) return;
        equipAction.AtExecute(character, null, this);
    }

    // 「装備を外す」時に実行される関数
    public void Remove(Character character)
    {
        if (removeAction == null) return;
        removeAction.AtExecute(character, null, this);
    }

    // 「技能実行」時に実行される関数
    public void Execute(Character character)
    {
        if (executeAction == null) return;
        executeAction.AtExecute(character, null, this);
    }
}

public enum ItemTag
{
    None,
    AlexEquippable
}

public enum EffectShape
{
    None,
    Take,
    Touch,
    Shoot,
    Observe,
    Actuate
}
