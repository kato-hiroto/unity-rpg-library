using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Character", menuName = "ScriptableObjects/CreateCharacterAsset")]
public class CharacterObject : ReactorObject
{
    // 基本情報
    public string characterName = "";
    public string charDescription = "";
    public List<CharacterTag> charTags = new List<CharacterTag>();  // 何かしらの属性

    // 初期値
    public float initHitPoint = 0f;
    public float initMagicPoint = 0f;
    public float initEnergyPoint = 0f;
    public float initMoveSpeed = 0f;
    public int initMovePattern = 0;

    // イベント
    public List<EventBehaviour> targetActions = new List<EventBehaviour>();
    public List<EventBehaviour> untargetActions = new List<EventBehaviour>();
}

public enum CharacterTag
{
    None
}
