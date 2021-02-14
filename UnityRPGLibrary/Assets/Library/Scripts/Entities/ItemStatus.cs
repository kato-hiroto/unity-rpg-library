using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/Create_Item")]
public class ItemStatus : ScriptableObject
{
    // 基本情報
    public string itemName = "";
    public string skillName = "";
    public string itemDescription = "";
    public string skillDescription = "";
    public List<ItemTag> itemTags = new List<ItemTag>();  // 何かしらの属性 (装備可能キャラの指定など)

    // 初期値
    public int initQuantity = 0;
    public float initLevel = 0f;

    // 設定パラメータ
    public int worth = 0;
    public int range = 0;
    public float effect = 0f;
    public float cooltime = 0f;
    public EffectShape effectShape = EffectShape.None;
}

public enum ItemTag
{
    None,
    AlexEquippable
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