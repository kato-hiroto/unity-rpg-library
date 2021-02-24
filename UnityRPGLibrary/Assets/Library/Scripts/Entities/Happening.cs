using System;

[Serializable]
public class Happening : ObjectBehaviour<HappeningStatus>, IHappening
{
    // コントローラ
    [NonSerialized]
    public ObjectBehaviour<IHappening>[] controllers;

    override protected void Init()
    {
        controllers = GetComponents<ObjectBehaviour<IHappening>>();
        foreach(var elem in controllers)
        {
            elem.Setting(uniqueId, this);
        }
    }

    public override void Setting(string uId, HappeningStatus s)
    {
        status = s;
        SetID(uId);
    }
}

public interface IHappening
{
}
