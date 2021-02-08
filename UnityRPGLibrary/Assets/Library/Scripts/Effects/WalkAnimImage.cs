using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class WalkAnimImage
{
    // Spriteのリスト
    [field: SerializeField]
    public WalkAnimObject walkAnim {get; private set;}

    // 直前の値
    private int prevIndex = 0;
    private int prevAnim = 0;

    public Sprite GetImage(Quaternion quat, bool isStopped = false)
    {
        if (walkAnim.images.Count < 1)
        {
            return null;
        }
        else
        {
            Vector3 vec = quat.eulerAngles;
            float angleUnit = 2 * Mathf.PI / walkAnim.images.Count;
            int index = Mathf.FloorToInt(Mathf.Atan2(-vec.x, -vec.y) / angleUnit);
            prevAnim = isStopped ? 0 : (prevAnim + 1) % 4;
            prevIndex = index;
            return walkAnim.GetSprite(prevIndex, prevAnim);
        }
    }
}
