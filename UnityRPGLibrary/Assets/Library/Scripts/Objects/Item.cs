using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Item : ObjectBehaviour
{
    // 基礎ステータス
    [SerializeField]
    public ItemObject status;

    // グローバル格納値
    [NonSerialized]
    public ObjectState<int> quantity;
    [NonSerialized]
    public ObjectState<float> level;

    // ステータスの挿入
    public void SetStatus(string uniqueId, ItemObject status)
    {
        this.status = status;
        FirstSetting(uniqueId);
    }

    // データロード時・初期処理
    override protected void Init()
    {
        quantity = varList.intMap.SyncState($"{uniqueId}_q", status.initQuantity);
        level = varList.floatMap.SyncState($"{uniqueId}_l", status.initLevel);
    }

    // すべての初期処理終了後に呼ばれる関数
    override protected void AfterInit()
    {
    }

    // 「使用」時に実行される関数
    public void Use(Character character)
    {
        if (status.useAction == null) return;
        status.useAction.AtExecute(character, null, this);
    }

    // 「消費」時に実行される関数
    public void Consume(Character character)
    {
        if (status.consumeAction == null) return;
        status.consumeAction.AtExecute(character, null, this);
    }

    // 「装備をつける」時に実行される関数
    public void Equip(Character character)
    {
        if (status.equipAction == null) return;
        status.equipAction.AtExecute(character, null, this);
    }

    // 「装備を外す」時に実行される関数
    public void Remove(Character character)
    {
        if (status.removeAction == null) return;
        status.removeAction.AtExecute(character, null, this);
    }

    // 「技能実行」時に実行される関数
    public void Execute(Character character)
    {
        if (status.executeAction == null) return;
        status.executeAction.AtExecute(character, null, this);
    }
}
