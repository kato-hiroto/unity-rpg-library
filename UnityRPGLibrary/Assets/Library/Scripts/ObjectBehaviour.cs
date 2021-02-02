using System;
using UnityEngine;

[Serializable]
abstract public class ObjectBehaviour : MonoBehaviour
{
    // オブジェクト名
    abstract public string objectName {get; protected set;}

    // グローバル格納値
    protected ObjectStateList varList = ObjectStateList.getInstance();
    protected ObjectStream taskStream;

    // データロード時・初期処理
    abstract protected void Init();
    abstract protected void AfterInit();

    // InitをvarListに関連付ける
    protected void AttachInit()
    {
        taskStream = ObjectStream.getInstance();
        varList.AddInitTrigger(objectName, Init);
        Init();
    }

    // AfterInitをvarListに関連付ける
    protected void AttachAfterInit()
    {
        varList.AddAfterTrigger(objectName, AfterInit);
        AfterInit();
    }

    // Initの実行
    void Awake()
    {
        AttachInit();
    }

    // AfterInitの実行
    void Start()
    {
        AttachAfterInit();
    }
}

    // 状態に関するメソッドを定義
    // 1. 自分の状態変数をグローバルリストに登録
    // 2. 任意の状態変数を更新
    // 3. 任意の状態変数の更新通知受け取り (コールバック関数)
    // 4. すべての状態変数のファイル書き出し
    // 5. ファイルからすべての状態変数読み込み
    // 6. 自分の状態変数の更新通知を受け取って自身の状態を変化
    // 7. 処理開始通知，処理終了通知
    // 8. 他の処理を行う間停止
