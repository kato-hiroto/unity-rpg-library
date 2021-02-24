using System;

[Serializable]
public class Item : ObjectBehaviour<ItemStatus>, IItem
{
    // コントローラ
    [NonSerialized]
    public ObjectBehaviour<IItem>[] controllers;

    override protected void Init()
    {
        controllers = GetComponents<ObjectBehaviour<IItem>>();
        foreach(var elem in controllers)
        {
            elem.Setting(uniqueId, this);
        }
    }

    public override void Setting(string uId, ItemStatus s)
    {
        status = s;
        SetID(uId);
    }
}

public interface IItem
{
}
