using System;

[Serializable]
public class Routine : ObjectBehaviour<RoutineStatus>, IRoutine
{
    // コントローラ
    [NonSerialized]
    public ObjectBehaviour<IRoutine>[] controllers;

    override protected void Init()
    {
        controllers = GetComponents<ObjectBehaviour<IRoutine>>();
        foreach(var elem in controllers)
        {
            elem.Setting(uniqueId, this);
        }
    }

    public override void Setting(string uId, RoutineStatus s)
    {
        status = s;
        SetID(uId);
    }
}

public interface IRoutine
{
}
