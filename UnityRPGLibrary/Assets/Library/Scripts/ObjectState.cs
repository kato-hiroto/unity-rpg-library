using System.Collections.Generic;
using UnityEngine;

public class ObjectState<T>
{
    // デリゲート定義
    public delegate void Callback(T s);

    // 値参照・更新時の処理
    Dictionary<string, Callback> getTriggers = new Dictionary<string, Callback>();
    Dictionary<string, Callback> setTriggers = new Dictionary<string, Callback>();

    // 保持する値
    T value = default(T);

    // コンストラクタ 
    ObjectState(T initValue)
    {
        this.value = initValue;
    }

    // ゲッター
    public T getValue()
    {
        foreach (var getTrigger in this.getTriggers)
        {
            getTrigger.Value(this.value);
        }
        return this.value;
    }

    // セッター
    public void setValue(T newValue)
    {
        this.value = newValue;
        foreach (var setTrigger in this.setTriggers)
        {
            setTrigger.Value(newValue);
        }
    }

    // getTriggerへの追加
    public void addGetTrigger(string name, Callback callback)
    {
        getTriggers.Add(name, callback);
    }

    // getTriggerからの削除
    public void deleteGetTrigger(string name)
    {
        getTriggers.Remove(name);
    }

    // setTriggerへの追加
    public void addSetTrigger(string name, Callback callback)
    {
        setTriggers.Add(name, callback);
    }

    // setTriggerからの削除
    public void deleteSetTrigger(string name)
    {
        setTriggers.Remove(name);
    }

    // シリアライズ
    public string serialize()
    {
        return JsonUtility.ToJson(this.value);
    }

    // ローディング
    public void loading(string s)
    {
        this.value = JsonUtility.FromJson<T>(s);
    }
}
