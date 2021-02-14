using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Anim2D
{
    // Spriteのリスト
    [field: SerializeField]
    public Anim2DStatus walkAnim {get; private set;}

    // 直前の値
    private int prevIndex = 0;
    private int prevAnim = 0;

    public Sprite GetImage(Quaternion quat, bool isStopped = false)
    {
        int count = walkAnim.images.Count;
        if (count < 1)
        {
            return null;
        }
        else
        {
            Vector3 vec = quat * Vector3.right;
            float baseAngle = 7f * Mathf.PI / 2f;   // (3/2pi + 2pi)
            float angleUnit = 2 * Mathf.PI / count;
            int index = Mathf.FloorToInt((baseAngle - Mathf.Atan2(-vec.x, -vec.y)) / angleUnit);
            prevAnim = isStopped ? 0 : (prevAnim + 1) % 4;
            prevIndex = index % count;
            return walkAnim.GetSprite(prevIndex, prevAnim);
        }
    }
}
