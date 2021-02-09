using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WalkAnim", menuName = "ScriptableObjects/Create_WalkAnim")]
public class WalkAnimObject : ScriptableObject
{
    // 下(正面)から時計回りに0-7を対応付けると 0,1,2,5,6,7,4,3 の順番
    // 1 → 0,2 → 3
    [SerializeField]
    public List<Sprite> images = new List<Sprite>();

    // 下(正面)から時計回りに0-7，かつアニメーションを0-3に対応付け
    public Sprite GetSprite(int dir, int phase)
    {
        if (images.Count == 24)
        {
            int imgDir = dir <= 2 ? dir : (dir <= 5 ? dir + 2 : (dir <= 6 ? 4 : 3));
            int imgPhase = phase <= 0 ? 1 : (phase <= 3 ? phase - 1 : 0);
            return images[imgDir * 3 + imgPhase];
        }
        else
        {
            int imgDir = images.Count < 3 ? 0 : dir % (images.Count / 3);
            int imgPhase = phase <= 0 ? 1 : (phase <= 3 ? phase - 1 : 0);
            return images[imgDir * 3 + imgPhase];
        }
    }
}
