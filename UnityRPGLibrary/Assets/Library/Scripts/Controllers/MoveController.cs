using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveController : ObjectBehaviour
{
    // 対応付け
    static private List< Tuple<int, Vector3> > eightDirs = new List< Tuple<int, Vector3> >{
        new Tuple<int, Vector3>(0, -Vector3.up),
        new Tuple<int, Vector3>(1, -Vector3.right - Vector3.up),
        new Tuple<int, Vector3>(2, -Vector3.right),
        new Tuple<int, Vector3>(3, -Vector3.right + Vector3.up),
        new Tuple<int, Vector3>(4, Vector3.up),
        new Tuple<int, Vector3>(5, Vector3.right + Vector3.up),
        new Tuple<int, Vector3>(6, Vector3.right),
        new Tuple<int, Vector3>(7, Vector3.right - Vector3.up)
    };

    // 移動対象キャラクタ
    public Character character;

    // フィールド
    private bool isMoving = false;
    private NavMeshAgent agent;
    private NavMeshHit hit;
    private NavMeshPath path;
    
    // データロード時・初期処理
    override protected void Init()
    {
        agent = GetComponent<NavMeshAgent>();
        path = new NavMeshPath();
    }

    // すべての初期処理終了後に呼ばれる関数
    override protected void AfterInit(){}

    // cos距離の計算
    private float CosSimilarity(Vector3 a, Vector3 b)
    {
        float denom = a.magnitude * b.magnitude;
        return denom == 0 ? -1 : Vector3.Dot(a, b) / denom;
    }

    // 直線による移動可能性の判定
    private bool isMovable(Vector3 targetPos)
    {
        return !agent.Raycast(targetPos, out hit);
    }

    // 移動したい方向ベスト3の決定 (Vector3)
    private List<Vector3> GetNearDirection(Vector3 nowPos, Vector3 targetPos)
    {
        bool result = NavMesh.CalculatePath(nowPos, targetPos, NavMesh.AllAreas, path);
        if (!result) return new List<Vector3>();
        Vector3 firstDir = path.corners[0] - nowPos;
        return eightDirs
            .ConvertAll(x => new Tuple<int, Vector3, float>(x.Item1, x.Item2, CosSimilarity(x.Item2, firstDir)))
            .OrderBy(x => -x.Item3)
            .ToList()
            .GetRange(0, 3)
            .ConvertAll(x => x.Item2);
    }

    // 移動
    public void Move(Vector3 targetPos)
    {
        if (isMoving) return;
        var charId = character.uniqueId;
        var nowPos = character.objPosition.GetValue();
        var dirs = GetNearDirection(nowPos, targetPos);
        if (dirs.Count <= 0) return;
        foreach (var dir in dirs)
        {
            if (isMovable(nowPos + dir))
            {
                // 移動開始
                taskStream.AddLoop($"{charId}_move", () =>
                {
                    transform.Translate((nowPos + dir) * Time.deltaTime * character.moveSpeed.GetValue());
                });
                break;
            }
        }
    }
}
