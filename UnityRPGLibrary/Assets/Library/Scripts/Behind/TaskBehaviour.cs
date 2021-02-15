// using System;
// using UnityEngine;

// [Serializable]
// abstract public class TaskBehaviour<T> : MonoBehaviour
// {
//     // オブジェクトID
//     [field: SerializeField]
//     public string uniqueId {get; private set;} = "";

//     // グローバル格納値
//     protected ObjectStateList varList = ObjectStateList.getInstance();
//     protected ObjectStream taskStream;

//     // データロード時・初期処理
//     abstract protected void AfterInit();
//     abstract public void Setting(T entity);

//     // Awake is called when the script instance is being loaded.
//     void Awake()
//     {
//         taskStream = ObjectStream.getInstance();
//     }

//     // Start is called on the frame when a script is enabled just before any of the Update methods is called the first time.
//     void Start()
//     {
//         SetID(this.uniqueId);
//     }

//     // IDの設定，AfterInitの登録
//     protected void SetID(string initUniqueId)
//     {
//         if (initUniqueId == "") return;
//         this.uniqueId = initUniqueId;
//         varList.AddAfterTrigger(uniqueId, AfterInit);
//         AfterInit();
//     }
// }
