using System;

[Serializable]
abstract public class EventBehaviour : ObjectBehaviour, IEvent
{
    abstract public void AtExecute(Character sender, Reactor receiver, Item item);
    abstract public void VsExecute(Character attacker, Character receiver, Item item);
}

public interface IEvent
{
    void AtExecute(Character sender, Reactor receiver, Item item);
    void VsExecute(Character attacker, Character receiver, Item item);
}
