using UnityEngine;
using System.Collections.Generic;

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
    public float initReactionRange = 0;

    // イベント
    public List<EventBehaviour> detectActions = new List<EventBehaviour>();
    public List<EventBehaviour> loseSightActions = new List<EventBehaviour>();
    public List<EventBehaviour> stepActions = new List<EventBehaviour>();
    public List<EventBehaviour> touchActions = new List<EventBehaviour>();
    public List<EventBehaviour> affectActions = new List<EventBehaviour>();
}

public enum ReactorTag
{
    None
}
