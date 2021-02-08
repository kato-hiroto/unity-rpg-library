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

    // 関数の実行
    void ActExec(List<EventBehaviour> acts, Character character, Item item)
    {
        if (acts != null && acts.Count > 0)
        {
            foreach (var act in acts)
            {
                act.VsExecute(character, this, item);
            }
        } 
    }

    // 発見時に実行される関数
    override public void Detect(Character character)
    {
        ActExec(status.detectActions, character, null);
    }

    // 発見範囲の離脱時に実行される関数
    override public void Leave(Character character)
    {
        ActExec(status.loseSightActions, character, null);
    }

    // 通過時に実行される関数
    override public void Step(Character character)
    {
        ActExec(status.stepActions, character, null);
    }

    // 接触時に実行される関数
    override public void Touch(Character character)
    {
        ActExec(status.touchActions, character, null);
    }

    // アイテム・技能による干渉時に実行される関数
    override public void Affect(Character character, Item item)
    {
        ActExec(status.affectActions, character, item);
    }

    // アイテム・技能による干渉時に実行される関数
    public void Target(Character character, Item item)
    {
        ActExec(status.targetActions, character, item);
    }

    // アイテム・技能による干渉時に実行される関数
    public void Untarget(Character character, Item item)
    {
        ActExec(status.untargetActions, character, item);
    }
}
