using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Bag", menuName = "ScriptableObjects/Create_Bag")]
public class BagStatus : ScriptableObject
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
    public float initCapacity = 0f;
    public float initMaxCapacity = 0f;

    // コントローラ
    public List<ObjectBehaviour<Bag>> controllers = new List<ObjectBehaviour<Bag>>();
}
