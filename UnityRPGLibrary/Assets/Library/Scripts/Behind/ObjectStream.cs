using System.Collections.Generic;
using UnityEngine;

public class ObjectStream : MonoBehaviour
{
    // 実行キューのデリゲート
    public delegate void StreamTask();

    // 実行キューの処理
    private Queue<StreamTask> queue = new Queue<StreamTask>();
    private bool executing = false;

    // タイマーの処理
    private Dictionary<string, StreamTask> timeline = new Dictionary<string, StreamTask>();
    private Dictionary<string, float> timelimit = new Dictionary<string, float>();

    // 無限ループの処理
    private Dictionary<string, StreamTask> loop = new Dictionary<string, StreamTask>();

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

    // 実行キューの移動
    public void Next()
    {
        queue.Dequeue();
        executing = false;
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

    // タイマーへの追加
    public void AddTimer(string name, float limit, StreamTask task)
    {
        if (!timeline.ContainsKey(name))
        {
            timeline.Add(name, task);
            timelimit.Add(name, limit);
        }
    }

    // タイマーの実行
    private void ExecuteTimer(string name)
    {
        timelimit[name] -= Time.deltaTime;
        if (timelimit[name] < 0f)
        {
            timeline[name]();
            timeline.Remove(name);
            timelimit.Remove(name);
        }
    }

    // タイマーから削除
    public void RemoveTimer(string name)
    {
        if (timeline.ContainsKey(name))
        {
            timeline.Remove(name);
            timelimit.Remove(name);
        }
    }

    // 無限ループへの追加
    public void AddLoop(string name, StreamTask task)
    {
        if (!loop.ContainsKey(name))
        {
            loop.Add(name, task);
        }
    }

    // 無限ループから削除
    public void RemoveLoop(string name)
    {
        if (loop.ContainsKey(name))
        {
            loop.Remove(name);
        }
    }

    // 各タスクの随時実行
    void Update()
    {
        // キューの実行
        Execute();

        if (Size() == 0)
        {
            // タイマーの実行
            foreach (var key in new List<string>(timeline.Keys))
            {
                ExecuteTimer(key);
            }

            // ループの実行
            foreach (var elem in loop)
            {
                elem.Value();
            }
        }
    }
}
