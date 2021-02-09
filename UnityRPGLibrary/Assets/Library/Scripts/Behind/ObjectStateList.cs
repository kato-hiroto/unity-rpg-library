using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ObjectStateList
{
    // 初期化のデリゲート
    public delegate void Initialize();

    // 初期化時の処理
    private Dictionary<string, Initialize> initTriggers = new Dictionary<string, Initialize>();
    private Dictionary<string, Initialize> afterTriggers = new Dictionary<string, Initialize>();

    // シングルトンインスタンス
    private static ObjectStateList mInstance;

    // 保持する値
    [SerializeField]
    public ObjectStateMapper<bool> boolMap;
    [SerializeField]
    public ObjectStateMapper<int> intMap;
    [SerializeField]
    public ObjectStateMapper<string> stringMap;
    [SerializeField]
    public ObjectStateMapper<float> floatMap;
    [SerializeField]
    public ObjectStateMapper<Vector3> vectorMap;

    // シングルトンの取得
    public static ObjectStateList getInstance()
    {
        if (mInstance == null)
        {
            mInstance = new ObjectStateList();
        }
        return mInstance;
    }

    // 初期化処理の実行
    private void DoInit()
    {
        foreach (var initTrigger in initTriggers)
        {
            initTrigger.Value();
        }
        foreach (var afterTrigger in afterTriggers)
        {
            afterTrigger.Value();
        }
    }

    // initTriggersへの追加
    public void AddInitTrigger(string name, Initialize callback)
    {
        if (!initTriggers.ContainsKey(name)) initTriggers.Add(name, callback);
    }

    // initTriggersから削除
    public void DeleteInitTrigger(string name)
    {
        if (initTriggers.ContainsKey(name)) initTriggers.Remove(name);
    }

    // afterTriggersへの追加
    public void AddAfterTrigger(string name, Initialize callback)
    {
        if (!afterTriggers.ContainsKey(name)) afterTriggers.Add(name, callback);
    }

    // afterTriggersから削除
    public void DeleteAfterTrigger(string name)
    {
        if (afterTriggers.ContainsKey(name)) afterTriggers.Remove(name);
    }

    // セーブ
    public void Save(string filename)
    {
        string json = JsonUtility.ToJson(this);
        string path = Application.persistentDataPath + $"/savedata_{filename}.json";
        File.WriteAllText(path, json);
        Debug.Log($"セーブ path: {path}");
    }

    // ロード
    public void Load(string filename)
    {
        string path = Application.persistentDataPath + $"/savedata_{filename}.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            var data = JsonUtility.FromJson<ObjectStateList>(json);
            boolMap = data.boolMap.makeMap();
            intMap = data.intMap.makeMap();
            stringMap = data.stringMap.makeMap();
            floatMap = data.floatMap.makeMap();
            vectorMap = data.vectorMap.makeMap();
            DoInit();
            Debug.Log($"ロード path: {path}");
        }
    }
}

[Serializable]
public class ObjectStateMapper<T>
{
    // 保存対象リスト
    [SerializeField]
    private List<ObjectState<T>> list;

    // 名前と変数を対応付けた辞書
    [NonSerialized]
    public Dictionary<string, ObjectState<T>> map;

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

    // 参照
    public ObjectState<T> SyncState(string name, T initValue)
    {
        Init();
        if (map.ContainsKey(name)) return map[name].Init();
        var state = new ObjectState<T>().Init(name, initValue);
        list.Add(state);
        map.Add(name, state);
        return state;
    }

    // 一括対応付け
    public ObjectStateMapper<T> makeMap()
    {
        Init();
        map.Clear();
        foreach (var elem in list)
        {
            map[elem.GetName()] = elem;
        }
        return this;
    }
}
