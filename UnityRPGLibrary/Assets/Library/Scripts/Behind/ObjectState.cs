using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ObjectState<T>
{
    // デリゲート定義
    public delegate void Callback(T s);
    public delegate bool ConditionJudge(T s);

    // 保持する値
    [SerializeField]
    private string name;
    [SerializeField]
    private T value;

    // 値参照・更新時の処理
    private Dictionary<string, Callback> setTriggers;

    // 初期化処理
    public ObjectState<T> Init()
    {
        name = "";
        value = default(T);
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

    // 値のゲッター
    public T GetValue()
    {
        return value;
    }

    // 値のセッター
    public void SetValue(T newValue)
    {
        value = newValue;
        foreach (var setTrigger in setTriggers)
        {
            setTrigger.Value(newValue);
        }
    }

    // setTriggersへの追加
    public void InitTrigger(string name, Callback callback, ConditionJudge judge)
    {
        if (!setTriggers.ContainsKey(name)) setTriggers.Add(name, callback);
        if (judge(value)) callback(value);
    }

    // setTriggersへの追加
    public void AddTrigger(string name, Callback callback)
    {
        if (!setTriggers.ContainsKey(name)) setTriggers.Add(name, callback);
    }

    // setTriggersからの削除
    public void RemoveTrigger(string name)
    {
        if (setTriggers.ContainsKey(name)) setTriggers.Remove(name);
    }
}
