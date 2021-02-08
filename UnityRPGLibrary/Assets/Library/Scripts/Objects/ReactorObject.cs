using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Reactor", menuName = "ScriptableObjects/CreateReactorAsset")]
public class ReactorObject : ScriptableObject
{
    // 基本情報
    [field: SerializeField]
    public string reactorName {get; private set;} = "";
    [field: SerializeField]
    public string description {get; private set;} = "";
    [field: SerializeField]
    public List<ReactorTag> reactorTags {get; private set;} = new List<ReactorTag>();  // 何かしらの属性

    // 初期値
    [field: SerializeField]
    public List<DirectionImage> images {get; private set;}
    [field: SerializeField]
    public int initImageNum {get; private set;} = 0;

    // イベント
    [SerializeField]
    public EventBehaviour approachAction = null;
    [SerializeField]
    public EventBehaviour stepAction = null;
    [SerializeField]
    public EventBehaviour touchAction = null;
    [SerializeField]
    public EventBehaviour checkAction = null;
    [SerializeField]
    public EventBehaviour affectAction = null;
}

public enum ReactorTag
{
    None
}
