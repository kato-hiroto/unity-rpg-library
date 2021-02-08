using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Reactor : ObjectBehaviour
{
    // オブジェクト名
    [field: SerializeField]
    override public string objectName {get; protected set;} = "";

    // 基本情報
    [field: SerializeField]
    public string reactorName {get; private set;} = "";
    [field: SerializeField]
    public string description {get; private set;} = "";
    [field: SerializeField]
    public List<ReactorTag> reactorTags {get; private set;} = new List<ReactorTag>();  // 何かしらの属性

    // 初期値
    [field: SerializeField]
    public List<DirectionImage> images {get; private set;}
    [field: SerializeField]
    public int initImageNum {get; private set;} = 0;

    // イベント
    [SerializeField]
    public EventBehaviour approachAction = null;
    [SerializeField]
    public EventBehaviour stepAction = null;
    [SerializeField]
    public EventBehaviour touchAction = null;
    [SerializeField]
    public EventBehaviour checkAction = null;
    [SerializeField]
    public EventBehaviour affectAction = null;

    // グローバル格納値
    [NonSerialized]
    public ObjectState<int> imageNum;
    [NonSerialized]
    public ObjectState<Vector3> objPosition;
    [NonSerialized]
    public ObjectState<Vector3> objRotation;

    // コンポーネント
    [NonSerialized]
    private SpriteRenderer mySprite;


    // データロード時・初期処理
    override protected void Init()
    {
        imageNum = varList.intMap.SyncState($"{uniqueID}_q", initImageNum);
        objPosition = varList.vectorMap.SyncState($"{uniqueID}_p", transform.position);
        objRotation = varList.vectorMap.SyncState($"{uniqueID}_r", transform.rotation.eulerAngles);
        transform.position = objPosition.GetValue();
        transform.rotation = Quaternion.Euler(objRotation.GetValue());
        mySprite = GetComponent<SpriteRenderer>();
        mySprite.sprite = images[imageNum.GetValue()].GetImage(transform.rotation);
    }

    // すべての初期処理終了後に呼ばれる関数
    override protected void AfterInit()
    {
    }

    // 接近時に実行される関数
    virtual public void Approach(Character character)
    {
        if (approachAction == null) return;
        approachAction.AtExecute(character, this, null);
    }

    // 通過時に実行される関数
    virtual public void Step(Character character)
    {
        if (stepAction == null) return;
        stepAction.AtExecute(character, this, null);
    }

    // 接触時に実行される関数
    virtual public void Touch(Character character)
    {
        if (touchAction == null) return;
        touchAction.AtExecute(character, this, null);
    }

    // 「調べる」時に実行される関数
    virtual public void Check(Character character)
    {
        if (checkAction == null) return;
        checkAction.AtExecute(character, this, null);
    }

    // アイテム・技能による干渉時に実行される関数
    virtual public void Affect(Character character, Item item)
    {
        if (affectAction == null) return;
        affectAction.AtExecute(character, this, item);
    }
}

public enum ReactorTag
{
    None
}

// 回転方向に基づいて変化する画像
[Serializable]
public class DirectionImage
{
    [field: SerializeField]
    public List<Sprite> images {get; private set;}
    // 下(正面)から時計回り
    // (x,y)=(0,-1)のとき，座標では(x,y)=(1,0)
    // (x,y)=(-1,0)のとき，座標では(x,y)=(0,1)

    public Sprite GetImage(Quaternion quat)
    {
        if (images.Count < 1)
        {
            return null;
        }
        else
        {
            Vector3 vec = quat.eulerAngles;
            float angleUnit = 2 * Mathf.PI / images.Count;
            int index = Mathf.FloorToInt(Mathf.Atan2(-vec.x, -vec.y) / angleUnit);
            return images[index];
        }
    }
}
