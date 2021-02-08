using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/CreateItemAsset")]
public class ItemObject : ScriptableObject
{
    // 基本情報
    public string itemName = "";
    public string skillName = "";
    public string description = "";
    public List<ItemTag> itemTags {get; private set;} = new List<ItemTag>();  // 何かしらの属性 (装備可能キャラの指定など)

    // 初期値
    public int initQuantity = 0;
    public float initLevel = 0f;

    // 設定パラメータ
    public int worth = 0;
    public float effect = 0f;
    public int range = 0;
    public EffectShape effectShape = EffectShape.None;
    public float cooltime = 0f;

    // イベント
    public List<EventBehaviour> useActions = new List<EventBehaviour>();
    public List<EventBehaviour> consumeActions = new List<EventBehaviour>();
    public List<EventBehaviour> equipActions = new List<EventBehaviour>();
    public List<EventBehaviour> removeActions = new List<EventBehaviour>();
    public List<EventBehaviour> executeActions = new List<EventBehaviour>();
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