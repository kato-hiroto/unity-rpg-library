using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Character", menuName = "ScriptableObjects/Create_Character")]
public class CharacterStatus : ScriptableObject
{
    // 基本情報
    public string characterName = "";
    public string description = "";

    // 描画に関する値
    public List<Anim2d> animImages = new List<Anim2d>();

    // 変動ステータス
    public int initPhase = 0;
    public float initHitPoint = 0f;
    public float initMaxHitPoint = 0f;
    public float initMagicPoint = 0f;
    public float initMaxMagicPoint = 0f;
    public float initEnergyPoint = 0f;
    public float initMaxEnergyPoint = 0f;
    public float initMoveSpeed = 0f;
    public float initMaxMoveSpeed = 0f;
    public MovePattern initMovePattern = MovePattern.None;

    // コントローラ
    public List<ObjectBehaviour<Character>> controllers = new List<ObjectBehaviour<Character>>();
}

public enum CharacterTag
{
    None,
    Movable,
    Playable
}

public enum MovePattern
{
    None = 0,
    Random = 1,
    Escape = 2,
    Attack = 3
}
