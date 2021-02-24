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

    // 基本ステータス
    public int initPhase = 0;
    public float initQuality = 0f;
    public float initQuantity = 0f;
    public float initLevel = 0f;

    // 初期ステータス
    public float initHitPoint = 0f;
    public float initMaxHitPoint = 0f;
    public float initMagicPoint = 0f;
    public float initMaxMagicPoint = 0f;
    public float initEnergyPoint = 0f;
    public float initMaxEnergyPoint = 0f;
    public float initMoveSpeed = 1f;
    public float initMaxMoveSpeed = 1f;
}
