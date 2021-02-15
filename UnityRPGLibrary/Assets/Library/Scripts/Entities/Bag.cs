using System;

[Serializable]
public class Bag : ObjectBehaviour<BagStatus>
{
    override protected void Init()
    {
        foreach(var elem in status.controllers)
        {
            elem.Setting(uniqueId, this);
        }
    }

    public override void Setting(string uniqueId, BagStatus s)
    {
        status = s;
        SetID(uniqueId);
    }
}
