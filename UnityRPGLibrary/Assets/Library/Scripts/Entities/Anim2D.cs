// using System;
// using System.Collections.Generic;
// using UnityEngine;

// [Serializable]
// public class Anim2d
// {
//     // Spriteのリスト
//     [field: SerializeField]
//     public Anim2dStatus walkAnim {get; private set;}

//     // 直前の値
//     private int prevIndex = 0;
//     private int prevAnim = 0;

//     public Sprite GetImage(Quaternion quat, bool isStopped = false)
//     {
//         int count = walkAnim.images.Count;
//         if (count < 1)
//         {
//             return null;
//         }
//         else
//         {
//             Vector3 vec = quat * Vector3.right;
//             float baseAngle = 7f * Mathf.PI / 2f;   // (3/2pi + 2pi)
//             float angleUnit = 2 * Mathf.PI / count;
//             int index = Mathf.FloorToInt((baseAngle - Mathf.Atan2(-vec.x, -vec.y)) / angleUnit);
//             prevAnim = isStopped ? 0 : (prevAnim + 1) % 4;
//             prevIndex = index % count;
//             return walkAnim.GetSprite(prevIndex, prevAnim);
//         }
//     }
// }
using System;
using UnityEngine;

[Serializable]
public class Anim2d : StuffBehaviour
{
    // 基礎ステータス
    [field: SerializeField]
    public Anim2dStatus status {get; private set;}

    // セーブデータ
    [NonSerialized]
    public ObjectState<int> initDir;
    [NonSerialized]
    public ObjectState<int> initPhase;

    // ローカル反応変数
    [NonSerialized]
    public ObjectState<bool> use;
    [NonSerialized]
    public ObjectState<bool> consume;
    [NonSerialized]
    public ObjectState<bool> equip;
    [NonSerialized]
    public ObjectState<bool> execute;

    // ステータスの挿入
    public void Setting(string initUniqueId, Anim2dStatus initStatus)
    {
        this.status = initStatus;
        SetID(initUniqueId);
    }

    // データロード時・初期処理
    override protected void Init()
    {
        quantity = varList.intMap.SyncState($"{uniqueId}_q", status.initQuantity);
        level = varList.floatMap.SyncState($"{uniqueId}_l", status.initLevel);
        use = new ObjectState<bool>().Init(false);
        consume = new ObjectState<bool>().Init(false);
        equip = new ObjectState<bool>().Init(false);
        execute = new ObjectState<bool>().Init(false);
    }
}
