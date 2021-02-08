using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Reactor", menuName = "ScriptableObjects/CreateReactorAsset")]
public class ReactorObject : ScriptableObject
{
    // 基本情報
    public string reactorName = "";
    public string description = "";
    public List<ReactorTag> reactorTags = new List<ReactorTag>();  // 何かしらの属性

    // 初期値
    public List<DirectionImage> images = new List<DirectionImage>();
    public int initImageNum = 0;
    public bool initDetectFlag = false;

    // イベント
    public List<EventBehaviour> detectActions = new List<EventBehaviour>();
    public List<EventBehaviour> leaveActions = new List<EventBehaviour>();
    public List<EventBehaviour> stepActions = new List<EventBehaviour>();
    public List<EventBehaviour> touchActions = new List<EventBehaviour>();
    public List<EventBehaviour> affectActions = new List<EventBehaviour>();
}

public enum ReactorTag
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