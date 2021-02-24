using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/Create_Item")]
public class ItemStatus : ScriptableObject
{
    // 基本情報
    public string itemName = "";
    public string itemDescription = "";
    public string skillName = "";
    public string skillDescription = "";

    // 描画に関する値
    public List<Anim2d> animImages = new List<Anim2d>();

    // 基本ステータス
    public int initPhase = 0;
    public float initQuality = 0f;
    public float initQuantity = 0f;
    public float initLevel = 0f;

    // 初期ステータス
    public float initEffect = 0f;
    public float initMaxEffect = 0f;
    public float initRange = 0;
    public float initMaxRange = 0;
    public float initCooltime = 0f;
    public float initMaxCooltime = 0f;
}
