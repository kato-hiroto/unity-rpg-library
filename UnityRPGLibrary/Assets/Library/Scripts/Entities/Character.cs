using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Character : ObjectBehaviour<CharacterStatus>, ICharacter
{
    // コントローラ
    [NonSerialized]
    public ObjectBehaviour<ICharacter>[] controllers;

    override protected void Init()
    {
        controllers = GetComponents<ObjectBehaviour<ICharacter>>();
        foreach(var elem in controllers)
        {
            elem.Setting(uniqueId, this);
        }
    }

    public override void Setting(string uId, CharacterStatus s)
    {
        status = s;
        SetID(uId);
    }

    // インタフェースプロパティ
    public Vector3 position
    {
        get {return transform.position;}
        set {transform.position = value;}
    }

    public Quaternion rotation
    {
        get {return transform.rotation;}
        set {transform.rotation = value;}
    }

    public float speed
    {
        get {return status.initMoveSpeed;}
    }
}

public interface ICharacter
{
    Vector3 position {get; set;}
    Quaternion rotation {get; set;}
    float speed {get;}
}
