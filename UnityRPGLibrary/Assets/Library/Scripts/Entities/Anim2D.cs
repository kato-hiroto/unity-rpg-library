using System;

[Serializable]
public class Anim2d : ObjectBehaviour<Anim2dStatus>, IAnim2d
{
    // コントローラ
    [NonSerialized]
    public ObjectBehaviour<IAnim2d>[] controllers;

    override protected void Init()
    {
        controllers = GetComponents<ObjectBehaviour<IAnim2d>>();
        foreach(var elem in controllers)
        {
            elem.Setting(uniqueId, this);
        }
    }

    public override void Setting(string uId, Anim2dStatus s)
    {
        status = s;
        SetID(uId);
    }
}

public interface IAnim2d
{
}
