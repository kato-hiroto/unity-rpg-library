using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Character : Reactor
{
    // オブジェクト名
    [field: SerializeField]
    override public string objectName {get; protected set;} = "";

    // 基本情報
    [field: SerializeField]
    public string characterName {get; private set;} = "";
    [field: SerializeField]
    public List<CharacterTag> charTags {get; private set;} = new List<CharacterTag>();  // 何かしらの属性

    // 初期値
    [field: SerializeField]
    public float initHitPoint {get; private set;} = 0f;
    [field: SerializeField]
    public float initMagicPoint {get; private set;} = 0f;
    [field: SerializeField]
    public float initEnergyPoint {get; private set;} = 0f;
    [field: SerializeField]
    public float initMoveSpeed {get; private set;} = 0f;
    [field: SerializeField]
    public int initMovePattern {get; private set;} = 0;

    // イベント
    [SerializeField]
    private EventBehaviour targetAction = null;

    // グローバル格納値
    [NonSerialized]
    public ObjectState<float> hitPoint;
    [NonSerialized]
    public ObjectState<float> magicPoint;
    [NonSerialized]
    public ObjectState<float> energyPoint;
    [NonSerialized]
    public ObjectState<float> moveSpeed;
    [NonSerialized]
    public ObjectState<int> movePattern;

    // 関連オブジェクト
    [field: SerializeField]
    public ItemList equippingItems {get; private set;} = null;

    // データロード時・初期処理
    override protected void Init()
    {
        base.Init();
        hitPoint = varList.floatMap.SyncState($"{uniqueID}_hp", initHitPoint);
        magicPoint = varList.floatMap.SyncState($"{uniqueID}_mp", initMagicPoint);
        energyPoint = varList.floatMap.SyncState($"{uniqueID}_ep", initEnergyPoint);
        moveSpeed = varList.floatMap.SyncState($"{uniqueID}_spe", initMoveSpeed);
        movePattern = varList.intMap.SyncState($"{uniqueID}_pat", initMovePattern);
    }

    // すべての初期処理終了後に呼ばれる関数
    override protected void AfterInit()
    {
        base.AfterInit();
    }

    // 接近時に実行される関数
    override public void Approach(Character character)
    {
        if (approachAction == null) return;
        approachAction.VsExecute(character, this, null);
    }

    // 通過時に実行される関数
    override public void Step(Character character)
    {
        if (stepAction == null) return;
        stepAction.VsExecute(character, this, null);
    }

    // 接触時に実行される関数
    override public void Touch(Character character)
    {
        if (touchAction == null) return;
        touchAction.VsExecute(character, this, null);
    }

    // 「調べる」時に実行される関数
    override public void Check(Character character)
    {
        if (checkAction == null) return;
        checkAction.VsExecute(character, this, null);
    }

    // アイテム・技能による干渉時に実行される関数
    override public void Affect(Character character, Item item)
    {
        if (affectAction == null) return;
        affectAction.VsExecute(character, this, item);
    }

    // ターゲッティングされた時に実行される関数
    public void Target(Character character, Item item)
    {
        if (targetAction == null) return;
        targetAction.VsExecute(character, this, item);
    }
}

public enum CharacterTag
{
    None
}

// public enum AutoMovePattern
// {
//     None,
//     Random,
//     Escape,
//     Attack
// }
