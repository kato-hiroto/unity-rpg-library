using System.Collections.Generic;
using UnityEngine;

public class ObjectStream : MonoBehaviour
{
    // 実行キューのデリゲート
    public delegate void StreamTask();

    // 実行キューの処理
    private Queue<StreamTask> queue = new Queue<StreamTask>();
    private bool executing = false;
    
    // データ保存領域
    public ObjectStateList varList = new ObjectStateList();

    // シングルトンインスタンス
    private static ObjectStream mInstance;

    // シングルトンの取得
    public static ObjectStream getInstance()
    {
        if (mInstance == null)
        {
            var tmpObj = new GameObject("ObjectStream");
            mInstance = tmpObj.AddComponent<ObjectStream>();
        }
        return mInstance;
    }

    // シングルトンの初期化
    public static void resetInstance()
    {
        mInstance = null;
    }

    // 実行キューへの追加
    public void Add(StreamTask task)
    {
        queue.Enqueue(task);
    }

    // 実行キューの長さ確認
    public int Size()
    {
        return queue.Count;
    }

    // 実行キューの移動
    public void Next()
    {
        queue.Dequeue();
        executing = false;
    }

    // ループへの追加
    public ObjectState<bool> AddLoop(string name, StreamTask task)
    {
        var state = varList.loopTaskMap.SyncState(name, false);
        state.AddTrigger(name, (x) => {
            if (x)
            {
                task();
            }
        });
        return state;
    }

    // ループの削除
    public void RemoveLoop(string name)
    {
        varList.loopTaskMap.RemoveState(name);
    }

    // タイマーへの追加
    public ObjectState<bool> AddTimer(string name, float limit, StreamTask loopTask, StreamTask timerTask)
    {
        var state = varList.loopTaskMap.SyncState(name, false);
        var timer = varList.timerTaskMap.SyncState(name, limit);
        
        state.AddTrigger(name, (x) => {
            if (x)
            {
                loopTask();
                timer.SetValue(timer.GetValue() - Time.deltaTime);
            }
        });
        timer.AddTrigger(name, (x) => {
            if (x <= 0f)
            {
                state.SetValue(false);
                timer.SetValue(limit);
                timerTask();
            }
        });
        return state;
    }

    // タイマーの開始
    public void StartTimer(string name, float limit)
    {
        varList.timerTaskMap.SyncState(name, limit).SetValue(limit);
        varList.loopTaskMap.SyncState(name, false).SetValue(true);
    }

    // タイマーの削除
    public void RemoveTimer(string name)
    {
        varList.loopTaskMap.RemoveState(name);
        varList.timerTaskMap.RemoveState(name);
    }

    // 各タスクの随時実行
    void Update()
    {
        // キューの実行
        if (Size() > 0 && !executing)
        {
            executing = true;
            var task = queue.Peek();
            task();
        }

        // ループ・タイマーの実行
        if (Size() == 0)
        {
            var list = varList.loopTaskMap.GetList();
            Debug.Log($"count: {list.Count}");
            foreach (var elem in list)
            {
                Debug.Log($"1 name:{elem.GetName()}, value:{elem.GetValue()}, trigger: {elem.setTriggers}");
            }
            foreach (var elem in list)
            {
                if (elem.GetValue())
                {
                    Debug.Log($"2 name: {elem.GetName()}, value: {elem.GetValue()}, trigger: {elem.setTriggers}");
                    elem.SetValue(true);
                }
            }
        }
    }
}
