using System;
using UnityEngine;

[Serializable]
public class Character : ObjectBehaviour
{
    // 基礎ステータス
    [field: SerializeField]
    public bool isMobile {get; private set;}
    [field: SerializeField]
    public CharacterObject status {get; private set;}

    // セーブデータ
    [NonSerialized]
    public ObjectState<int> imageNum;
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
    public ObjectState<bool> detect;
    [NonSerialized]
    public ObjectState<bool> step;
    [NonSerialized]
    public ObjectState<bool> touch;
    [NonSerialized]
    public ObjectState<bool> affect;
    [NonSerialized]
    public ObjectState<bool> target;

    // コンポーネント
    [field: NonSerialized]
    public SpriteRenderer mySprite {get; private set;} = null;

    // 関連オブジェクト
    [field: NonSerialized]
    public OBList equippingItems {get; private set;} = null;

    // Cloneオブジェクトの初期化
    public void Setting(string initUniqueId, bool initMobile, CharacterObject initStatus)
    {
        this.status = initStatus;
        this.isMobile = initMobile;
        SetID(initUniqueId);
    }

    // データロード時・初期処理
    override protected void Init()
    {
        // ObjectStateとの同期
        if (isMobile)
        {
            objPosition = varList.vectorMap.SyncState($"{uniqueId}_p", transform.position);
            objRotation = varList.vectorMap.SyncState($"{uniqueId}_r", transform.rotation.eulerAngles);
            hitPoint = varList.floatMap.SyncState($"{uniqueId}_hp", status.initHitPoint);
            magicPoint = varList.floatMap.SyncState($"{uniqueId}_mp", status.initMagicPoint);
            energyPoint = varList.floatMap.SyncState($"{uniqueId}_ep", status.initEnergyPoint);
            moveSpeed = varList.floatMap.SyncState($"{uniqueId}_spe", status.initMoveSpeed);
            movePattern = varList.intMap.SyncState($"{uniqueId}_pat", (int)status.initMovePattern);
        }
        else
        {
            objPosition = new ObjectState<Vector3>().Init(transform.position);
            objRotation = new ObjectState<Vector3>().Init(transform.rotation.eulerAngles);
            hitPoint = new ObjectState<float>().Init(status.initHitPoint);
            magicPoint = new ObjectState<float>().Init(status.initMagicPoint);
            energyPoint = new ObjectState<float>().Init(status.initEnergyPoint);
            moveSpeed = new ObjectState<float>().Init(status.initMoveSpeed);
            movePattern = new ObjectState<int>().Init((int)status.initMovePattern);
        }
        imageNum = varList.intMap.SyncState($"{uniqueId}_in", status.initImageNum);
        detect = new ObjectState<bool>().Init(false);
        step = new ObjectState<bool>().Init(false);
        touch = new ObjectState<bool>().Init(false);
        affect = new ObjectState<bool>().Init(false);
        target = new ObjectState<bool>().Init(false);

        // 位置の同期
        transform.position = objPosition.GetValue();
        objPosition.AddSetTrigger($"{uniqueId}_setPos", (x) =>
        {
            transform.position = x;
        });
        transform.rotation = Quaternion.Euler(objRotation.GetValue());
        objRotation.AddSetTrigger($"{uniqueId}_setRot", (x) =>
        {
            transform.rotation = Quaternion.Euler(x);
        });

        // 表示画像の同期
        mySprite = GetComponent<SpriteRenderer>();
        mySprite.sprite = status.images[imageNum.GetValue()].GetImage(transform.rotation);
    }
}
