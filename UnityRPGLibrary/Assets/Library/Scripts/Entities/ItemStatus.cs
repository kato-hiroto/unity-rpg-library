using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/Create_Item")]
public class ItemStatus : ScriptableObject
{
    // 基本情報
    public string itemName = "";
    public string itemDescription = "";
    public string skillName = "";
    public string skillDescription = "";
    public List<ItemTag> itemTags = new List<ItemTag>();  // 何かしらの属性 (装備可能キャラの指定など)

    // 描画に関する値
    public List<Anim2d> animImages = new List<Anim2d>();

    // 変動ステータス
    public int initPhase = 0;
    public int initQuantity = 0;
    public float initLevel = 0f;

    // 固定値
    public int worth = 0;
    public int range = 0;
    public float effect = 0f;
    public float cooltime = 0f;

    // エフェクトに関する値
    public EffectShape effectShape = EffectShape.None;

    // コントローラ
    public List<TaskBehaviour<Item>> controllers = new List<TaskBehaviour<Item>>();
}

public enum ItemTag
{
    None
}

public enum EffectShape
{
    None,
    Take,
    Touch,
    Shoot,
    Observe,
    Actuate
}
