using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Routine", menuName = "ScriptableObjects/Create_Routine")]
public class RoutineStatus : ScriptableObject
{
    // 基本情報
    public string routineName = "";
    public string description = "";

    // 描画に関する値
    public List<Anim2d> animImages = new List<Anim2d>();

    // 基本ステータス
    public int initPhase = 0;
    public float initQuality = 0f;
    public float initQuantity = 0f;
    public float initLevel = 0f;

    // 初期ステータス・移動
    public MovePattern initMovePattern = MovePattern.None;
    public List<Vector3> initMovePoint = new List<Vector3>();

    // 初期ステータス・行動
    public EncounterPattern initActiveEncounter = EncounterPattern.None;    // 敵以外が接近した時
    public EncounterPattern initPassiveEncounter = EncounterPattern.None;   // 敵以外に話しかけられた時
    public BattlePattern initActiveBattle = BattlePattern.None;             // 敵が接近した時
    public BattlePattern initPassiveBattle = BattlePattern.None;            // 敵に見つかった時
    public List<CharacterStatus> initEnemies = new List<CharacterStatus>(); // 敵
    public List<CharacterStatus> initFriends = new List<CharacterStatus>(); // 味方: 戦闘相手を敵認定
}

public enum MovePattern
{
    None = 0,
    Wait = 1,
    Trace = 2,
    Go = 3,
    Random = 4,
    Escape = 5,
}

public enum EncounterPattern
{
    None = 0,
    Ignore = 1,
    Avoid = 2,
    Interact = 3,
    Battle = 4      // 敵以外にも戦闘を仕掛け，相互に敵認定
}

public enum BattlePattern
{
    None = 0,
    Escape = 1,
    Save = 2,
    Attack = 3
}
