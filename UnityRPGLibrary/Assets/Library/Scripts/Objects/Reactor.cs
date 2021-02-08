using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Reactor : ObjectBehaviour
{
    // 基礎ステータス
    [field: SerializeField]
    public CharacterObject status {get; private set;}
    [field: SerializeField]
    public string initUniqueId {get; private set;}

    // グローバル格納値
    [NonSerialized]
    public ObjectState<int> imageNum;

    // ローカル反応変数
    [NonSerialized]
    public ObjectState<bool> detect;
    [NonSerialized]
    public ObjectState<bool> step;
    [NonSerialized]
    public ObjectState<bool> touch;
    [NonSerialized]
    public ObjectState<bool> affect;

    // コンポーネント
    [NonSerialized]
    protected SpriteRenderer mySprite;

    // 初期配置オブジェクトの初期化
    void Start()
    {
        StartSetting(initUniqueId);
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
        imageNum = varList.intMap.SyncState($"{uniqueId}_q", status.initImageNum);
        detect = new ObjectState<bool>().Init(false);
        step = new ObjectState<bool>().Init(false);
        touch = new ObjectState<bool>().Init(false);
        affect = new ObjectState<bool>().Init(false);
        mySprite = GetComponent<SpriteRenderer>();
        mySprite.sprite = status.images[imageNum.GetValue()].GetImage(transform.rotation);
    }

    // すべての初期処理終了後に呼ばれる関数
    override protected void AfterInit(){}
}
