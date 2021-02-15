using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Anim2D", menuName = "ScriptableObjects/Create_Anim2D")]
public class Anim2dStatus : ScriptableObject
{
    // 基本情報
    public string anim2dName = "";
    public List<Anim2dTag> animTags = new List<Anim2dTag>();  // 何かしらの属性

    // 描画に関する値
    public List<Sprite> images = new List<Sprite>();

    // 変動ステータス
    public int initDir = 0;
    public int initPhase = 0;

    // アニメーション詳細
    public MappingPattern mappingPattern = MappingPattern.None;

    // コントローラ
    public List<TaskBehaviour<Anim2d>> controllers = new List<TaskBehaviour<Anim2d>>();
}

public enum Anim2dTag
{
    None
}


public enum MappingPattern
{
    None,
    Walking8d
}
