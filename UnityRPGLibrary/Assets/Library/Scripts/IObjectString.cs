using System;
using UnityEngine;

public interface IStringReversible
{
    string ToString();
    void FromString(string s);
}

public class RInt: IStringReversible
{
    public int value = 0;

    public override string ToString()
    {
        return this.value.ToString();
    }

    public void FromString(string s)
    {
        Int32.TryParse(s, out this.value);
    }
}

public class RString: IStringReversible
{
    public string value = "";

    public override string ToString()
    {
        return this.value;
    }

    public void FromString(string s)
    {
        this.value = s;
    }
}

public class RVector: IStringReversible
{
    public Vector3 value = Vector3.right;

    public override string ToString()
    {
        return JsonUtility.ToJson(this.value);
    }

    public void FromString(string s)
    {
        Vector3.
    }
}
