using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/CreateItemAsset")]
public class EnemyParamAsset : ScriptableObject
{
    // 基本情報
    [field: SerializeField]
    public string itemName = "";
    [field: SerializeField]
    public string skillName = "";
    [field: SerializeField]
    public string description = "";
    [field: SerializeField]
    public List<ItemTag> itemTags {get; private set;} = new List<ItemTag>();  // 何かしらの属性 (装備可能キャラの指定など)

    // 初期値
    [field: SerializeField]
    public int initQuantity = 0;
    [field: SerializeField]
    public float initLevel = 0f;

    // 設定パラメータ
    [field: SerializeField]
    public int worth = 0;
    [field: SerializeField]
    public float effect = 0f;
    [field: SerializeField]
    public int range = 0;
    [field: SerializeField]
    public EffectShape effectShape = EffectShape.None;
    [field: SerializeField]
    public float cooltime = 0f;

    // イベント
    [SerializeField]
    public EventBehaviour useAction = null;
    [SerializeField]
    public EventBehaviour consumeAction = null;
    [SerializeField]
    public EventBehaviour equipAction = null;
    [SerializeField]
    public EventBehaviour removeAction = null;
    [SerializeField]
    public EventBehaviour executeAction = null;
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