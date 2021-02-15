using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveController : ObjectBehaviour<Character>
{
    // staticなグリッド
    [NonSerialized]
    static public Dictionary<int, Dictionary<int, Character>> gridField;

    // セーブデータ
    [NonSerialized]
    public ObjectState<float> moveSpeed;    // 移動速度
    [NonSerialized]
    public ObjectState<Vector3> startPos;   // 開始位置
    [NonSerialized]
    public ObjectState<Vector3> endPos;     // 移動目標
    [NonSerialized]
    public ObjectState<Vector3> nowPos;     // 描画位置
    [NonSerialized]
    public ObjectState<Vector3> nowRot;     // 描画角度
    [NonSerialized]
    public ObjectState<bool> moving;        // 移動中と到達時

    // データロード時の初期化処理
    override protected void Init(){}

    // 呼び出し時の初期化処理
    public override void Setting(string uId, Character s)
    {
        status = s;
        SetID($"{uId}/move");

        // Stateの同期
        moveSpeed = varList.floatMap.SyncState($"{uniqueId}/ms", 1f);
        startPos = varList.vectorMap.SyncState($"{uniqueId}/sp", transform.position);
        endPos = varList.vectorMap.SyncState($"{uniqueId}/ep", transform.position);
        nowPos = varList.vectorMap.SyncState($"{uniqueId}/np", transform.position);
        nowRot = varList.vectorMap.SyncState($"{uniqueId}/nr", transform.rotation.eulerAngles);
        moving = taskStream.AddTimer($"{uniqueId}/rc", 1f / moveSpeed.GetValue(), MoveLoop, MoveEnd);

        // statusへの逆同期
        nowPos.AddTrigger($"{uniqueId}", (x)=>{
            status.transform.position = x;
        });
        nowRot.AddTrigger($"{uniqueId}", (x)=>{
            status.transform.rotation = Quaternion.Euler(x);
        });
    }

    // 移動の継続
    private void MoveLoop()
    {
        // var moveDir = endPos.GetValue() - character.objPosition.GetValue();
        // var speed = character.moveSpeed.GetValue() * Time.deltaTime;
        // transform.Translate(moveDir * speed);
        // nowPos.SetValue(transform.position);
    }

    // 移動終了
    private void MoveEnd()
    {
        // taskStream.RemoveLoop(uniqueId);
        // character.objPosition.SetValue(endPos.GetValue());
        // isMoving.SetValue(false);
    }
}

//     // 基礎ステータス: 対象
//     [field: SerializeField]
//     public Character character {get; private set;}

//     // セーブデータ
//     [NonSerialized]
//     public ObjectState<bool> isMoving;
//     [NonSerialized]
//     public ObjectState<Vector3> nowPos;     // 描画位置
//     [NonSerialized]
//     public ObjectState<Vector3> endPos;     // 移動目標

//     // ローカル反応変数
//     [NonSerialized]
//     public ObjectState<bool> move;

//     // コンポーネント
//     [field: NonSerialized]
//     public NavMeshAgent agent {get; private set;}
//     private NavMeshHit hit;
//     private NavMeshPath path;

//     // 対応付け
//     static private List< Tuple<int, Vector3> > eightDirs = new List< Tuple<int, Vector3> >{
//         new Tuple<int, Vector3>(0, -Vector3.up),
//         new Tuple<int, Vector3>(1, -Vector3.right - Vector3.up),
//         new Tuple<int, Vector3>(2, -Vector3.right),
//         new Tuple<int, Vector3>(3, -Vector3.right + Vector3.up),
//         new Tuple<int, Vector3>(4, Vector3.up),
//         new Tuple<int, Vector3>(5, Vector3.right + Vector3.up),
//         new Tuple<int, Vector3>(6, Vector3.right),
//         new Tuple<int, Vector3>(7, Vector3.right - Vector3.up)
//     };

//     // ステータスの挿入
//     override public void Setting(Character initCharacter)
//     {
//         this.character = initCharacter;
//         SetID($"{initCharacter.uniqueId}_move");
//     }

//     // データロード時・初期処理
//     override protected void AfterInit()
//     {
//         // コンポーネントの初期化
//         agent = GetComponent<NavMeshAgent>();
//         hit = new NavMeshHit();
//         path = new NavMeshPath();

//         // Stateの同期
//         isMoving = varList.boolMap.SyncState($"{uniqueId}_im", false);
//         nowPos = varList.vectorMap.SyncState($"{uniqueId}_np", transform.position);
//         endPos = varList.vectorMap.SyncState($"{uniqueId}_ep", Vector3.zero);

//         // 関数の登録
//         isMoving.InitSetTrigger(uniqueId, true, (x) => AddTask());
//     }

//     // 関数の登録
//     private void AddTask()
//     {   
//         taskStream.AddLoop(uniqueId, () => MoveLoop());
//         taskStream.AddTimer(uniqueId, 1f / character.moveSpeed.GetValue(), () => MoveEnd());
//     }

//     // 移動の継続
//     private void MoveLoop()
//     {
//         var moveDir = endPos.GetValue() - character.objPosition.GetValue();
//         var speed = character.moveSpeed.GetValue() * Time.deltaTime;
//         transform.Translate(moveDir * speed);
//         nowPos.SetValue(transform.position);
//     }

//     // 移動終了
//     private void MoveEnd()
//     {
//         taskStream.RemoveLoop(uniqueId);
//         character.objPosition.SetValue(endPos.GetValue());
//         isMoving.SetValue(false);
//     }

//     // cos距離の計算
//     private float CosSimilarity(Vector3 a, Vector3 b)
//     {
//         float denom = a.magnitude * b.magnitude;
//         return denom == 0 ? -1 : Vector3.Dot(a, b) / denom;
//     }

//     // 移動したい方向ベスト3の決定 (Vector3)
//     private List<Vector3> GetNearDirection(Vector3 startPos, Vector3 targetPos)
//     {
//         bool result = NavMesh.CalculatePath(startPos, targetPos, NavMesh.AllAreas, path);
//         if (!result) return new List<Vector3>();
//         Vector3 firstDir = path.corners[0] - startPos;
//         return eightDirs
//             .ConvertAll(x => new Tuple<int, Vector3, float>(x.Item1, x.Item2, CosSimilarity(x.Item2, firstDir)))
//             .OrderBy(x => -x.Item3)
//             .ToList()
//             .GetRange(0, 3)
//             .ConvertAll(x => x.Item2);
//     }

//     // targetPos へ行くために1マス移動
//     public void Move(Vector3 targetPos)
//     {
//         if (isMoving.GetValue()) return;
//         Vector3 startPos = character.objPosition.GetValue();
//         List<Vector3> dirs = GetNearDirection(startPos, targetPos);
//         if (dirs.Count <= 0) return;
//         foreach (var dir in dirs)
//         {
//             if (agent.Raycast(targetPos, out hit)) continue;    // 直線上に障害物がある
//             endPos.SetValue(startPos + dir);
//             isMoving.SetValue(true);
//             break;
//         }
//     }
// }
