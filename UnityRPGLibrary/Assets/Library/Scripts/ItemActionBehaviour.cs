using System;

[Serializable]
abstract public class ItemActionBehaviour : ObjectBehaviour
{
    abstract public void Execute(Item item);
}
