using System;
using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Happening", menuName = "ScriptableObjects/Create_Happening")]
public class HappeningStatus : ScriptableObject
{
    // 基本情報
    public string eventName = "";
    public string description = "";

    // 描画に関する値
    public List<Anim2d> animImages = new List<Anim2d>();

    // 基本ステータス
    public int initPhase = 0;
    public float initQuality = 0f;
    public float initQuantity = 0f;
    public float initLevel = 0f;

    // 独自ステータス
    public List<HappeningPremise> premises = new List<HappeningPremise>();
    public List<HappeningCharacter> conditions = new List<HappeningCharacter>();
}

[Serializable]
public class HappeningPremise
{
    public HappeningStatus happening = null;
    public int count = 0;
}

[Serializable]
public class HappeningItem
{
    public ItemStatus item = null;
    public int count = 0;
}

[Serializable]
public class HappeningCharacter
{
    public CharacterStatus character = null;
    public EncounterPattern pattern = EncounterPattern.None;
    public float range = 0f;
    public List<HappeningItem> items = new List<HappeningItem>();
    // イベントが右クリックされた時
    // → キャラクターがアイテムを持っている&一定距離以内にいる場合はインタラクト成功，イベント実行
}
