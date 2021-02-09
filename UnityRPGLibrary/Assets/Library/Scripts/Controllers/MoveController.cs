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

    void Start()
    {
    }

    // データロード時・初期処理
    override protected void Init()
    {
        agent = GetComponent<NavMeshAgent>();
        hit = new NavMeshHit();
        path = new NavMeshPath();
    }

    // すべての初期処理終了後に呼ばれる関数
    override protected void AfterInit(){
        StartSetting($"{character.uniqueId}_move");
    }

    // cos距離の計算
    private float CosSimilarity(Vector3 a, Vector3 b)
    {
        float denom = a.magnitude * b.magnitude;
        return denom == 0 ? -1 : Vector3.Dot(a, b) / denom;
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

    // targetPos へ行くために1マス移動
    public void Move(Vector3 targetPos)
    {
        if (isMoving) return;
    
        Vector3 nowPos = character.objPosition.GetValue();
        List<Vector3> dirs = GetNearDirection(nowPos, targetPos);
        if (dirs.Count <= 0) return;

        foreach (var dir in dirs)
        {
            Vector3 endPos = nowPos + dir;
            if (agent.Raycast(targetPos, out hit)) continue;    // 直線上に障害物がある

            float speed = character.moveSpeed.GetValue();
            taskStream.AddLoop(uniqueId, () =>
            {
                transform.Translate(endPos * Time.deltaTime * speed);
            });
            taskStream.AddTimer(uniqueId, 1f / speed, () =>
            {
                taskStream.RemoveLoop(uniqueId);
                character.objPosition.SetValue(endPos);
                isMoving = false;
            });
            isMoving = true;
            break;
        }
    }
}
