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

    // シングルトンインスタンス
    private static ObjectStateList mInstance;

    // 保持する値
    [SerializeField]
    public ObjectStateMapper<bool> boolMap;
    [SerializeField]
    public ObjectStateMapper<int> intMap;
    [SerializeField]
    public ObjectStateMapper<float> floatMap;
    [SerializeField]
    public ObjectStateMapper<Vector3> vectorMap;
    [SerializeField]
    public ObjectStateMapper<string> stringMap;
    [SerializeField]
    public ObjectStateMapper<float> timerTaskMap;
    [SerializeField]
    public ObjectStateMapper<bool> loopTaskMap;

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
            timerTaskMap = data.timerTaskMap.makeMap();
            loopTaskMap = data.loopTaskMap.makeMap();
            DoInit();
            Debug.Log($"ロード path: {path}");
        }
    }
}
