using System.Collections.Generic;
using UnityEngine;

public class ObjectStream : MonoBehaviour
{
    // 実行キューのデリゲート
    public delegate void StreamTask();

    // 実行キューの処理
    private Queue<StreamTask> queue = new Queue<StreamTask>();
    private bool executing = false;
    
    // グローバル格納値
    private ObjectStateList varList = ObjectStateList.getInstance();

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

    // 実行キューの開始
    private void Execute()
    {
        if (Size() > 0 && !executing)
        {
            executing = true;
            var task = queue.Peek();
            task();
        }
    }

    // 実行キューの移動
    public void Next()
    {
        queue.Dequeue();
        executing = false;
    }

    // タイマーへの追加
    public void AddTimer(string name, float limit, StreamTask task)
    {
        var state = varList.timerTaskMap.SyncState(name, limit);
        state.AddTrigger(name, (x) => {
            if (x <= 0f)
            {
                task();
                varList.timerTaskMap.RemoveState(name);
            }
        });
    }

    // タイマーの実行
    private void ExecuteTimer()
    {
        foreach (var elem in varList.timerTaskMap.GetList())
        {
            elem.SetValue(elem.GetValue() - Time.deltaTime);
        }
    }

    // タイマーから削除
    public void RemoveTimer(string name)
    {
        varList.timerTaskMap.RemoveState(name);
    }

    // ループへの追加
    public void AddLoop(string name, StreamTask task)
    {
        var state = varList.loopTaskMap.SyncState(name, true);
        state.AddTrigger(name, (x) => {
            if (x)
            {
                task();
            }
        });
    }

    // ループの実行
    private void ExecuteLoop()
    {
        foreach (var elem in varList.loopTaskMap.GetList())
        {
            elem.SetValue(elem.GetValue());
        }
    }

    // ループから削除
    public void RemoveLoop(string name)
    {
        varList.loopTaskMap.RemoveState(name);
    }

    // 各タスクの随時実行
    void Update()
    {
        // キューの実行
        Execute();

        if (Size() == 0)
        {
            ExecuteTimer();
            ExecuteLoop();
        }
    }
}
