using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Character : Reactor
{
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
    public OBList equippingItems {get; private set;} = null;

    // データロード時・初期処理
    override protected void Init()
    {   
        objPosition = varList.vectorMap.SyncState($"{uniqueId}_p", transform.position);
        objRotation = varList.vectorMap.SyncState($"{uniqueId}_r", transform.rotation.eulerAngles);
        hitPoint = varList.floatMap.SyncState($"{uniqueId}_hp", status.initHitPoint);
        magicPoint = varList.floatMap.SyncState($"{uniqueId}_mp", status.initMagicPoint);
        energyPoint = varList.floatMap.SyncState($"{uniqueId}_ep", status.initEnergyPoint);
        moveSpeed = varList.floatMap.SyncState($"{uniqueId}_spe", status.initMoveSpeed);
        movePattern = varList.intMap.SyncState($"{uniqueId}_pat", (int)status.initMovePattern);
        imageNum = varList.intMap.SyncState($"{uniqueId}_q", status.initImageNum);
        target = new ObjectState<bool>().Init(false);
        transform.position = objPosition.GetValue();
        transform.rotation = Quaternion.Euler(objRotation.GetValue());
        base.Init();
    }

    // すべての初期処理終了後に呼ばれる関数
    override protected void AfterInit(){}
}
