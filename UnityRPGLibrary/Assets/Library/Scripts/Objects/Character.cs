using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Character : Reactor
{
    // 基礎ステータス
    [field: SerializeField]
    new public CharacterObject status {get; private set;} = null;

    // グローバル格納値
    [NonSerialized]
    public ObjectState<Vector3> objPosition;
    [NonSerialized]
    public ObjectState<Vector3> objRotation;
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

    // 関連オブジェクト
    [field: SerializeField]
    public ItemList equippingItems {get; private set;} = null;

    // ステータスの挿入
    public void Setting(string uniqueId, CharacterObject status)
    {
        this.status = status;
        StartSetting(uniqueId);
    }

    // データロード時・初期処理
    override protected void Init()
    {
        imageNum = varList.intMap.SyncState($"{uniqueId}_q", status.initImageNum);
        detectFlag = varList.boolMap.SyncState($"{uniqueId}_d", status.initDetectFlag);
        objPosition = varList.vectorMap.SyncState($"{uniqueId}_p", transform.position);
        objRotation = varList.vectorMap.SyncState($"{uniqueId}_r", transform.rotation.eulerAngles);
        hitPoint = varList.floatMap.SyncState($"{uniqueId}_hp", status.initHitPoint);
        magicPoint = varList.floatMap.SyncState($"{uniqueId}_mp", status.initMagicPoint);
        energyPoint = varList.floatMap.SyncState($"{uniqueId}_ep", status.initEnergyPoint);
        moveSpeed = varList.floatMap.SyncState($"{uniqueId}_spe", status.initMoveSpeed);
        movePattern = varList.intMap.SyncState($"{uniqueId}_pat", status.initMovePattern);
        transform.position = objPosition.GetValue();
        transform.rotation = Quaternion.Euler(objRotation.GetValue());
        base.Init();
    }

    // すべての初期処理終了後に呼ばれる関数
    override protected void AfterInit(){}

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
        ActExec(status.leaveActions, character, null);
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

    // ターゲッティングされた時に実行される関数
    public void Target(Character character, Item item)
    {
        ActExec(status.targetActions, character, item);
    }

    // ターゲッティングが解除された時に実行される関数
    public void Untarget(Character character, Item item)
    {
        ActExec(status.untargetActions, character, item);
    }
}
