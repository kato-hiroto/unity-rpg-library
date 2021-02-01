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
    private List<ObjectState<bool>> boolList = new List<ObjectState<bool>>();
    [SerializeField]
    private List<ObjectState<int>> intList = new List<ObjectState<int>>();
    [SerializeField]
    private List<ObjectState<string>> stringList = new List<ObjectState<string>>();
    [SerializeField]
    private List<ObjectState<float>> floatList = new List<ObjectState<float>>();
    [SerializeField]
    private List<ObjectState<Vector2>> vectorList = new List<ObjectState<Vector2>>();

    // 名前と対応付けた辞書
    private Dictionary<string, ObjectState<bool>> boolDic = new Dictionary<string, ObjectState<bool>>();
    private Dictionary<string, ObjectState<int>> intDic = new Dictionary<string, ObjectState<int>>();
    private Dictionary<string, ObjectState<string>> stringDic = new Dictionary<string, ObjectState<string>>();
    private Dictionary<string, ObjectState<float>> floatDic = new Dictionary<string, ObjectState<float>>();
    private Dictionary<string, ObjectState<Vector2>> vectorDic = new Dictionary<string, ObjectState<Vector2>>();

    // シングルトンの取得
    public static ObjectStateList getInstance() {
        if (mInstance == null) {
            mInstance = new ObjectStateList();
        }
        return mInstance;
    }

    // initTriggersへの追加
    public void AddInitTrigger(string name, Initialize callback)
    {
        initTriggers.Add(name, callback);
    }

    // initTriggersから削除
    public void DeleteInitTrigger(string name)
    {
        initTriggers.Remove(name);
    }

    // 初期化処理
    public void Init() {
        foreach (var initTrigger in this.initTriggers)
        {
            initTrigger.Value();
        }
    }

    // 名前と変数の対応付け
    private void makeDictionary() {
        foreach (var elem in ObjectStateList.getInstance().boolList)
        {
            boolDic[elem.GetName()] = elem;
        }
        foreach (var elem in ObjectStateList.getInstance().intList)
        {
            intDic[elem.GetName()] = elem;
        }
        foreach (var elem in ObjectStateList.getInstance().stringList)
        {
            stringDic[elem.GetName()] = elem;
        }
        foreach (var elem in ObjectStateList.getInstance().floatList)
        {
            floatDic[elem.GetName()] = elem;
        }
        foreach (var elem in ObjectStateList.getInstance().vectorList)
        {
            vectorDic[elem.GetName()] = elem;
        }
    }

    // 名前によるbool値の参照
    public ObjectState<bool> GetBoolState(string name, bool initValue) {
        return boolDic.ContainsKey(name) ? boolDic[name] : new ObjectState<bool>(name, initValue);
    }

    // 名前によるint値の参照
    public ObjectState<int> GetIntState(string name, int initValue) {
        return intDic.ContainsKey(name) ? intDic[name] : new ObjectState<int>(name, initValue);
    }

    // 名前によるstring値の参照
    public ObjectState<string> GetStringState(string name, string initValue) {
        return stringDic.ContainsKey(name) ? stringDic[name] : new ObjectState<string>(name, initValue);
    }

    // 名前によるfloat値の参照
    public ObjectState<float> GetFloatState(string name, float initValue) {
        return floatDic.ContainsKey(name) ? floatDic[name] : new ObjectState<float>(name, initValue);
    }

    // 名前によるVector2値の参照
    public ObjectState<Vector2> GetVectorState(string name, Vector2 initValue) {
        return vectorDic.ContainsKey(name) ? vectorDic[name] : new ObjectState<Vector2>(name, initValue);
    }

    // セーブ
    public void Save(string filename) {
        string json = JsonUtility.ToJson(this);
        File.WriteAllText(Application.persistentDataPath + $"/savedata_{filename}.json", json);
    }

    // ロード
    public void Load(string filename) {
        string json = File.ReadAllText(Application.persistentDataPath + $"/savedata_{filename}.json");
        JsonUtility.FromJsonOverwrite(json, this);
        this.makeDictionary();
        this.Init();
    }
}
