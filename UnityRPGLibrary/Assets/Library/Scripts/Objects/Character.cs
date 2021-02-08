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

    // ローカル反応変数
    [NonSerialized]
    public ObjectState<bool> target;

    // 関連オブジェクト
    [field: SerializeField]
    public ItemList equippingItems {get; private set;} = null;

    // 初期配置オブジェクトの初期化
    void Start()
    {
        StartSetting(uniqueId);
    }

    // ステータスの挿入
    public void Setting(string uniqueId, CharacterObject status)
    {
        this.status = status;
        StartSetting(uniqueId);
    }

    // データロード時・初期処理
    override protected void Init()
    {   
        objPosition = varList.vectorMap.SyncState($"{uniqueId}_p", transform.position);
        objRotation = varList.vectorMap.SyncState($"{uniqueId}_r", transform.rotation.eulerAngles);
        hitPoint = varList.floatMap.SyncState($"{uniqueId}_hp", status.initHitPoint);
        magicPoint = varList.floatMap.SyncState($"{uniqueId}_mp", status.initMagicPoint);
        energyPoint = varList.floatMap.SyncState($"{uniqueId}_ep", status.initEnergyPoint);
        moveSpeed = varList.floatMap.SyncState($"{uniqueId}_spe", status.initMoveSpeed);
        movePattern = varList.intMap.SyncState($"{uniqueId}_pat", status.initMovePattern);
        imageNum = varList.intMap.SyncState($"{uniqueId}_q", status.initImageNum);
        detectFlag = varList.boolMap.SyncState($"{uniqueId}_d", status.initDetectFlag);
        detect = new ObjectState<bool>().Init(false);
        step = new ObjectState<bool>().Init(false);
        touch = new ObjectState<bool>().Init(false);
        affect = new ObjectState<bool>().Init(false);
        target = new ObjectState<bool>().Init(false);
        transform.position = objPosition.GetValue();
        transform.rotation = Quaternion.Euler(objRotation.GetValue());
        mySprite = GetComponent<SpriteRenderer>();
        mySprite.sprite = status.images[imageNum.GetValue()].GetImage(transform.rotation);
    }

    // すべての初期処理終了後に呼ばれる関数
    override protected void AfterInit(){}
}
