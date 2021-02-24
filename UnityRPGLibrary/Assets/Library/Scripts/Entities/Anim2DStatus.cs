using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Anim2D", menuName = "ScriptableObjects/Create_Anim2D")]
public class Anim2dStatus : ScriptableObject
{
    // 基本情報
    public string anim2dName = "";
    public string description = "";

    // 描画に関する値
    public List<Anim2d> animImages = new List<Anim2d>();

    // 基本ステータス
    public int initPhase = 0;
    public float initQuality = 0f;
    public float initQuantity = 0f;
    public float initLevel = 0f;

    // 初期ステータス
    public float initDir = 0f;
    public float initScale = 0f;

    // 独自ステータス
    public List<Sprite> images = new List<Sprite>();
    public EffectShape effectShape = EffectShape.None;
    public List<int> dirMapping = new List<int>();
    public List<int> phaseMapping = new List<int>();
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
