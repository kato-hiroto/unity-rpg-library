using System;

public interface IEventItem
{
    void Execute(Character character, Item item);
}

public interface IEventReactor
{
    void Execute(Character character, Reactor reactor, Item item);
}

public interface IEventAffected
{
    void Execute(Character character, Reactor reactor, Item item);
}

public interface IEventCharacter
{
    void Execute(Character attacker, Character receiver, Item item);
}
