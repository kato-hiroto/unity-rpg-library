using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Character : Reactor
{
    // 基礎ステータス
    [SerializeField]
    new public CharacterObject status;

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
    public ObjectState<int> movePattern;    // None, Random, Escape, Attack

    // コンポーネント
    [NonSerialized]
    private SpriteRenderer mySprite;

    // 関連オブジェクト
    [field: SerializeField]
    public ItemList equippingItems {get; private set;} = null;

    // ステータスの挿入
    public void SetStatus(string uniqueId, CharacterObject status)
    {
        this.status = status;
        FirstSetting(uniqueId);
    }

    // データロード時・初期処理
    override protected void Init()
    {
        objPosition = varList.vectorMap.SyncState($"{uniqueId}_p", transform.position);
        objRotation = varList.vectorMap.SyncState($"{uniqueId}_r", transform.rotation.eulerAngles);
        imageNum = varList.intMap.SyncState($"{uniqueId}_q", status.initImageNum);
        hitPoint = varList.floatMap.SyncState($"{uniqueId}_hp", status.initHitPoint);
        magicPoint = varList.floatMap.SyncState($"{uniqueId}_mp", status.initMagicPoint);
        energyPoint = varList.floatMap.SyncState($"{uniqueId}_ep", status.initEnergyPoint);
        moveSpeed = varList.floatMap.SyncState($"{uniqueId}_spe", status.initMoveSpeed);
        movePattern = varList.intMap.SyncState($"{uniqueId}_pat", status.initMovePattern);
        transform.position = objPosition.GetValue();
        transform.rotation = Quaternion.Euler(objRotation.GetValue());
        mySprite = GetComponent<SpriteRenderer>();
        mySprite.sprite = status.images[imageNum.GetValue()].GetImage(transform.rotation);
    }

    // すべての初期処理終了後に呼ばれる関数
    override protected void AfterInit()
    {
    }

    // 接近時に実行される関数
    override public void Approach(Character character)
    {
        if (status.approachAction == null) return;
        status.approachAction.VsExecute(character, this, null);
    }

    // 通過時に実行される関数
    override public void Step(Character character)
    {
        if (status.stepAction == null) return;
        status.stepAction.VsExecute(character, this, null);
    }

    // 接触時に実行される関数
    override public void Touch(Character character)
    {
        if (status.touchAction == null) return;
        status.touchAction.VsExecute(character, this, null);
    }

    // 「調べる」時に実行される関数
    override public void Check(Character character)
    {
        if (status.checkAction == null) return;
        status.checkAction.VsExecute(character, this, null);
    }

    // アイテム・技能による干渉時に実行される関数
    override public void Affect(Character character, Item item)
    {
        if (status.affectAction == null) return;
        status.affectAction.VsExecute(character, this, item);
    }

    // ターゲッティングされた時に実行される関数
    public void Target(Character character, Item item)
    {
        if (status.targetAction == null) return;
        status.targetAction.VsExecute(character, this, item);
    }
}
