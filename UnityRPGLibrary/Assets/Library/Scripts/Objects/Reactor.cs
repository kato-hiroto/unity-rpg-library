using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Reactor : ObjectBehaviour
{
    // 基礎ステータス
    [SerializeField]
    public ReactorObject status;

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

    // ステータスの挿入
    public void SetStatus(string uniqueId, ReactorObject status)
    {
        this.status = status;
        FirstSetting(uniqueId);
    }

    // データロード時・初期処理
    override protected void Init()
    {
        imageNum = varList.intMap.SyncState($"{uniqueId}_q", status.initImageNum);
        objPosition = varList.vectorMap.SyncState($"{uniqueId}_p", transform.position);
        objRotation = varList.vectorMap.SyncState($"{uniqueId}_r", transform.rotation.eulerAngles);
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
    virtual public void Approach(Character character)
    {
        if (status.approachAction == null) return;
        status.approachAction.AtExecute(character, this, null);
    }

    // 通過時に実行される関数
    virtual public void Step(Character character)
    {
        if (status.stepAction == null) return;
        status.stepAction.AtExecute(character, this, null);
    }

    // 接触時に実行される関数
    virtual public void Touch(Character character)
    {
        if (status.touchAction == null) return;
        status.touchAction.AtExecute(character, this, null);
    }

    // 「調べる」時に実行される関数
    virtual public void Check(Character character)
    {
        if (status.checkAction == null) return;
        status.checkAction.AtExecute(character, this, null);
    }

    // アイテム・技能による干渉時に実行される関数
    virtual public void Affect(Character character, Item item)
    {
        if (status.affectAction == null) return;
        status.affectAction.AtExecute(character, this, item);
    }
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
