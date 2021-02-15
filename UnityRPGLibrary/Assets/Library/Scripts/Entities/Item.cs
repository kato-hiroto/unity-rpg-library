using System;

[Serializable]
public class Item : ObjectBehaviour<ItemStatus>
{
    override protected void Init()
    {
        foreach(var elem in status.controllers)
        {
            elem.Setting(uniqueId, this);
        }
    }

    public override void Setting(string uniqueId, ItemStatus s)
    {
        status = s;
        SetID(uniqueId);
    }
}

// using System;
// using UnityEngine;

// [Serializable]
// public class Item : StuffBehaviour
// {
//     // 基礎ステータス
//     [field: SerializeField]
//     public ItemStatus status {get; private set;}

//     // セーブデータ
//     [NonSerialized]
//     public ObjectState<int> quantity;
//     [NonSerialized]
//     public ObjectState<float> level;

//     // ローカル反応変数
//     [NonSerialized]
//     public ObjectState<bool> use;
//     [NonSerialized]
//     public ObjectState<bool> consume;
//     [NonSerialized]
//     public ObjectState<bool> equip;
//     [NonSerialized]
//     public ObjectState<bool> execute;

//     // ステータスの挿入
//     public void Setting(string initUniqueId, ItemStatus initStatus)
//     {
//         this.status = initStatus;
//         SetID(initUniqueId);
//     }

//     // データロード時・初期処理
//     override protected void Init()
//     {
//         quantity = varList.intMap.SyncState($"{uniqueId}_q", status.initQuantity);
//         level = varList.floatMap.SyncState($"{uniqueId}_l", status.initLevel);
//         use = new ObjectState<bool>().Init(false);
//         consume = new ObjectState<bool>().Init(false);
//         equip = new ObjectState<bool>().Init(false);
//         execute = new ObjectState<bool>().Init(false);
//     }
// }
