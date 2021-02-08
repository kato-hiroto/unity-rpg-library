using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Character", menuName = "ScriptableObjects/CreateCharacterAsset")]
public class CharacterObject : ReactorObject
{
    // 基本情報
    [field: SerializeField]
    public string characterName {get; private set;} = "";
    [field: SerializeField]
    public string charDescription {get; private set;} = "";
    [field: SerializeField]
    public List<CharacterTag> charTags {get; private set;} = new List<CharacterTag>();  // 何かしらの属性

    // 初期値
    [field: SerializeField]
    public float initHitPoint {get; private set;} = 0f;
    [field: SerializeField]
    public float initMagicPoint {get; private set;} = 0f;
    [field: SerializeField]
    public float initEnergyPoint {get; private set;} = 0f;
    [field: SerializeField]
    public float initMoveSpeed {get; private set;} = 0f;
    [field: SerializeField]
    public int initMovePattern {get; private set;} = 0;

    // イベント
    [SerializeField]
    public EventBehaviour targetAction = null;
}

public enum CharacterTag
{
    None
}
