using System;
using UnityEngine;

[Serializable]
public class Character : ObjectBehaviour<CharacterStatus>
{
    override protected void Init()
    {
        foreach(var elem in status.controllers)
        {
            elem.Setting(uniqueId, this);
        }
    }

    public override void Setting(string uniqueId, CharacterStatus s)
    {
        status = s;
        SetID(uniqueId);
    }

    // // セーブデータ
    // [NonSerialized]
    // public ObjectState<int> phase;
    // [NonSerialized]
    // public ObjectState<Vector3> objPosition;
    // [NonSerialized]
    // public ObjectState<Vector3> objRotation;
    // [NonSerialized]
    // public ObjectState<float> hitPoint;
    // [NonSerialized]
    // public ObjectState<float> magicPoint;
    // [NonSerialized]
    // public ObjectState<float> energyPoint;
    // [NonSerialized]
    // public ObjectState<float> moveSpeed;
    // [NonSerialized]
    // public ObjectState<int> movePattern;    // None, Random, Escape, Attack

    // // ローカル反応変数
    // [NonSerialized]
    // public ObjectState<bool> detect = new ObjectState<bool>().Init(false);
    // [NonSerialized]
    // public ObjectState<bool> step = new ObjectState<bool>().Init(false);
    // [NonSerialized]
    // public ObjectState<bool> touch = new ObjectState<bool>().Init(false);
    // [NonSerialized]
    // public ObjectState<bool> affect = new ObjectState<bool>().Init(false);
    // [NonSerialized]
    // public ObjectState<bool> target = new ObjectState<bool>().Init(false);

    // // コンポーネント
    // [field: NonSerialized]
    // public SpriteRenderer mySprite {get; private set;} = null;

    // // 関連オブジェクト
    // [field: NonSerialized]
    // public Bag equippingItems {get; private set;} = null;

    // // Cloneオブジェクトの初期化
    // public void Setting(string initUniqueId, CharacterStatus initStatus)
    // {
    //     this.status = initStatus;
    //     SetID(initUniqueId);
    // }

    // // データロード時・初期処理
    // override protected void Init()
    // {
    //     // ObjectStateとの同期
    //     phase = varList.intMap.SyncState($"{uniqueId}_in", status.initPhase);

    //     // 
    //     if (status.charTags.Contains(CharacterTag.Movable))
    //     {
    //         objPosition = varList.vectorMap.SyncState($"{uniqueId}_p", transform.position);
    //         objRotation = varList.vectorMap.SyncState($"{uniqueId}_r", transform.rotation.eulerAngles);
    //         hitPoint = varList.floatMap.SyncState($"{uniqueId}_hp", status.initHitPoint);
    //         magicPoint = varList.floatMap.SyncState($"{uniqueId}_mp", status.initMagicPoint);
    //         energyPoint = varList.floatMap.SyncState($"{uniqueId}_ep", status.initEnergyPoint);
    //         moveSpeed = varList.floatMap.SyncState($"{uniqueId}_spe", status.initMoveSpeed);
    //         movePattern = varList.intMap.SyncState($"{uniqueId}_pat", (int)status.initMovePattern);
    //     }
    //     else
    //     {
    //         objPosition = new ObjectState<Vector3>().Init(transform.position);
    //         objRotation = new ObjectState<Vector3>().Init(transform.rotation.eulerAngles);
    //         hitPoint = new ObjectState<float>().Init(status.initHitPoint);
    //         magicPoint = new ObjectState<float>().Init(status.initMagicPoint);
    //         energyPoint = new ObjectState<float>().Init(status.initEnergyPoint);
    //         moveSpeed = new ObjectState<float>().Init(status.initMoveSpeed);
    //         movePattern = new ObjectState<int>().Init((int)status.initMovePattern);
    //     }

    //     // 位置の同期
    //     transform.position = objPosition.GetValue();
    //     transform.rotation = Quaternion.Euler(objRotation.GetValue());
    //     objPosition.AddSetTrigger($"{uniqueId}_setPos", (x) => {transform.position = x;});
    //     objRotation.AddSetTrigger($"{uniqueId}_setRot", (x) => {transform.rotation = Quaternion.Euler(x);});

    //     // 表示画像の同期
    //     mySprite = GetComponent<SpriteRenderer>();
    //     mySprite.sprite = status.images[imageNum.GetValue()].GetImage(transform.rotation);

    //     // コントローラの初期化
    //     foreach (var controller in status.controllers)
    //     {
    //         controller.Setting(this);
    //     }
    // }
}
