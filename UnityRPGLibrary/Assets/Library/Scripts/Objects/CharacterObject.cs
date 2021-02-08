using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Character", menuName = "ScriptableObjects/CreateCharacterAsset")]
public class CharacterObject : ScriptableObject
{
    // 基本情報
    public string characterName = "";
    public string description = "";
    public List<CharacterTag> charTags = new List<CharacterTag>();  // 何かしらの属性

    // 初期値
    public int initImageNum = 0;
    public List<DirectionImage> images = new List<DirectionImage>();
    public float initHitPoint = 0f;
    public float initMagicPoint = 0f;
    public float initEnergyPoint = 0f;
    public float initMoveSpeed = 0f;
    public int initMovePattern = 0;
}

public enum CharacterTag
{
    None
}

[Serializable]
public class DirectionImage
{
    [field: SerializeField]
    public List<Sprite> images {get; private set;}  // 下(正面)から時計回り

    public Sprite GetImage(Quaternion quat)
    {
        if (images.Count < 1)
        {
            return null;
        }
        else
        {
            Vector3 vec = quat.eulerAngles;
            float angleUnit = 2 * Mathf.PI / images.Count;
            int index = Mathf.FloorToInt(Mathf.Atan2(-vec.x, -vec.y) / angleUnit);
            return images[index];
        }
    }
}