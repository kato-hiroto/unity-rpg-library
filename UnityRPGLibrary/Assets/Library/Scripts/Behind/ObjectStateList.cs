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
    // private static ObjectStateList mInstance;

    // 保持する値
    [SerializeField]
    public ObjectStateMapper<bool> boolMap = new ObjectStateMapper<bool>().makeMap();
    [SerializeField]
    public ObjectStateMapper<int> intMap = new ObjectStateMapper<int>().makeMap();
    [SerializeField]
    public ObjectStateMapper<float> floatMap = new ObjectStateMapper<float>().makeMap();
    [SerializeField]
    public ObjectStateMapper<Vector3> vectorMap = new ObjectStateMapper<Vector3>().makeMap();
    [SerializeField]
    public ObjectStateMapper<string> stringMap = new ObjectStateMapper<string>().makeMap();
    [SerializeField]
    public ObjectStateMapper<bool> loopTaskMap = new ObjectStateMapper<bool>().makeMap();
    [SerializeField]
    public ObjectStateMapper<float> timerTaskMap = new ObjectStateMapper<float>().makeMap();

    // // シングルトンの取得
    // public static ObjectStateList getInstance()
    // {
    //     if (mInstance == null)
    //     {
    //         mInstance = new ObjectStateList();
    //     }
    //     // Debug.Log($"count2: {mInstance.loopTaskMap.GetList().Count}");
    //     return mInstance;
    // }

    // // シングルトンの初期化
    // public static void resetInstance()
    // {
    //     mInstance = null;
    // }

    // 登録した初期化処理の実行
    private void DoOthersInit()
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
            DoOthersInit();
            Debug.Log($"ロード path: {path}");
        }
    }
}
