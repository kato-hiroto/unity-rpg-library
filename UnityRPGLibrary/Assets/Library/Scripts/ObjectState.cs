using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ObjectState<T>
{
    // デリゲート定義
    public delegate void Callback(T s);

    // 値参照・更新時の処理
    private Dictionary<string, Callback> getTriggers = new Dictionary<string, Callback>();
    private Dictionary<string, Callback> setTriggers = new Dictionary<string, Callback>();

    // 保持する値
    [SerializeField]
    private string name = "";
    [SerializeField]
    private T value = default(T);

    // コンストラクタ 
    public ObjectState(string initName, T initValue)
    {
        this.name = initName;
        this.value = initValue;
    }

    // 名前のゲッター
    public String GetName()
    {
        return this.name;
    }

    // ゲッター
    public T GetValue()
    {
        foreach (var getTrigger in this.getTriggers)
        {
            getTrigger.Value(this.value);
        }
        return this.value;
    }

    // セッター
    public void SetValue(T newValue)
    {
        this.value = newValue;
        foreach (var setTrigger in this.setTriggers)
        {
            setTrigger.Value(newValue);
        }
    }

    // getTriggersへの追加
    public void AddGetTrigger(string name, Callback callback)
    {
        getTriggers.Add(name, callback);
    }

    // getTriggersからの削除
    public void DeleteGetTrigger(string name)
    {
        getTriggers.Remove(name);
    }

    // setTriggersへの追加
    public void AddSetTrigger(string name, Callback callback)
    {
        setTriggers.Add(name, callback);
    }

    // setTriggersからの削除
    public void DeleteSetTrigger(string name)
    {
        setTriggers.Remove(name);
    }
}
