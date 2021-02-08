using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Character", menuName = "ScriptableObjects/Create_Character")]
public class CharacterObject : ScriptableObject
{
    // 基本情報
    public string characterName = "";
    public string description = "";
    public List<CharacterTag> charTags = new List<CharacterTag>();  // 何かしらの属性

    // 初期値
    public int initImageNum = 0;
    public List<WalkAnimImage> images = new List<WalkAnimImage>();
    public float initHitPoint = 0f;
    public float initMagicPoint = 0f;
    public float initEnergyPoint = 0f;
    public float initMoveSpeed = 0f;
    public MovePatternTag initMovePattern = MovePatternTag.None;
}

public enum CharacterTag
{
    None,
    Playable
}

public enum MovePatternTag
{
    None = 0,
    Random = 1,
    Escape = 2,
    Attack = 3
}
