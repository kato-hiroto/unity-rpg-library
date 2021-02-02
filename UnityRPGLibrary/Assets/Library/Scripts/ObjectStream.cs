using System.Collections.Generic;
using UnityEngine;

public class ObjectStream : MonoBehaviour
{
    // 実行キューのデリゲート
    public delegate void StreamTask();

    // 実行キューの処理
    private Queue<StreamTask> queue = new Queue<StreamTask>();
    private bool executing = false;

    // シングルトンインスタンス
    private static ObjectStream mInstance;

    // シングルトンの取得
    public static ObjectStream getInstance() {
        if (mInstance == null) {
            var tmp = new GameObject("ObjectStream");
            mInstance = tmp.AddComponent<ObjectStream>();
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

    // キューの随時実行
    void Update()
    {
        Execute();
    }
}
