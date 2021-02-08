using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ObjectState<T>
{
    // デリゲート定義
    public delegate void Callback(T s);

    // 値参照・更新時の処理
    private Dictionary<string, Callback> getTriggers;
    private Dictionary<string, Callback> setTriggers;

    // 保持する値
    [SerializeField]
    private string name = "";
    [SerializeField]
    private T value = default(T);

    // 初期化処理
    public ObjectState<T> Init()
    {
        getTriggers = new Dictionary<string, Callback>();
        setTriggers = new Dictionary<string, Callback>();
        return this;
    }

    public ObjectState<T> Init(T initValue)
    {
        name = "";
        value = initValue;
        getTriggers = new Dictionary<string, Callback>();
        setTriggers = new Dictionary<string, Callback>();
        return this;
    }

    public ObjectState<T> Init(string initName, T initValue)
    {
        name = initName;
        value = initValue;
        getTriggers = new Dictionary<string, Callback>();
        setTriggers = new Dictionary<string, Callback>();
        return this;
    }

    // 名前のゲッター
    public String GetName()
    {
        return name;
    }

    // ゲッター
    public T GetValue()
    {
        foreach (var getTrigger in getTriggers)
        {
            getTrigger.Value(value);
        }
        return value;
    }

    // セッター
    public void SetValue(T newValue)
    {
        value = newValue;
        foreach (var setTrigger in setTriggers)
        {
            setTrigger.Value(newValue);
        }
    }

    // getTriggersへの追加
    public void AddGetTrigger(string name, Callback callback)
    {
        if (!getTriggers.ContainsKey(name)) getTriggers.Add(name, callback);
    }

    // getTriggersからの削除
    public void DeleteGetTrigger(string name)
    {
        if (getTriggers.ContainsKey(name)) getTriggers.Remove(name);
    }

    // setTriggersへの追加
    public void AddSetTrigger(string name, Callback callback)
    {
        if (!setTriggers.ContainsKey(name)) setTriggers.Add(name, callback);
    }

    // setTriggersからの削除
    public void DeleteSetTrigger(string name)
    {
        if (setTriggers.ContainsKey(name)) setTriggers.Remove(name);
    }
}
