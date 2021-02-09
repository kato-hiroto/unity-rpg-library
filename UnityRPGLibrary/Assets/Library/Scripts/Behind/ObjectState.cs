using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ObjectState<T>
{
    // デリゲート定義
    public delegate void Callback(T s);

    // 値参照・更新時の処理
    private Dictionary<string, Callback> setTriggers;

    // 保持する値
    [SerializeField]
    private string name = "";
    [SerializeField]
    private T value = default(T);

    // 初期化処理
    public ObjectState<T> Init()
    {
        setTriggers = new Dictionary<string, Callback>();
        return this;
    }

    public ObjectState<T> Init(T initValue)
    {
        name = "";
        value = initValue;
        setTriggers = new Dictionary<string, Callback>();
        return this;
    }

    public ObjectState<T> Init(string initName, T initValue)
    {
        name = initName;
        value = initValue;
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

    // setTriggersへの追加
    public void AddSetTrigger(string name, Callback callback)
    {
        if (!setTriggers.ContainsKey(name)) setTriggers.Add(name, callback);
    }

    // setTriggersへの追加
    public void InitSetTrigger(string name, T condition, Callback callback)
    {
        if (!setTriggers.ContainsKey(name)) setTriggers.Add(name, callback);
        if (condition == null || value.Equals(condition)) callback(value);
    }

    // setTriggersからの削除
    public void RemoveSetTrigger(string name)
    {
        if (setTriggers.ContainsKey(name)) setTriggers.Remove(name);
    }
}
