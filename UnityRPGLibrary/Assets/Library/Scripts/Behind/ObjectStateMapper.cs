using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ObjectStateMapper<T>
{
    // 保存対象リスト
    [SerializeField]
    private List<ObjectState<T>> list;

    // 名前と変数を対応付けた辞書
    [NonSerialized]
    private Dictionary<string, ObjectState<T>> map;

    // 初期化
    private ObjectStateMapper<T> Init()
    {
        if (list == null)
        {
            list = new List<ObjectState<T>>();
        }
        if (map == null)
        {
            map = new Dictionary<string, ObjectState<T>>();
        }
        return this;
    }

    // リストの参照
    public List<ObjectState<T>> GetList()
    {
        return list;
    }

    // 追加・参照
    public ObjectState<T> SyncState(string name, T initValue)
    {
        if (map.ContainsKey(name))
        {
            return map[name].Init();
        }
        else
        {
            var state = new ObjectState<T>().Init(name, initValue);
            list.Add(state);
            map.Add(name, state);
            Debug.Log($"state.name: {state.GetName()}");
            return state;
        }
    }

    // 一括対応付け
    public ObjectStateMapper<T> makeMap()
    {
        Init();
        map.Clear();
        foreach (var elem in list)
        {
            map[elem.GetName()] = elem.Init();
        }
        return this;
    }

    // 除去
    public void RemoveState(string name)
    {
        if (map.ContainsKey(name))
        {
            var state = map[name];
            map.Remove(name);
            list.Remove(state);
        }
    }
}
