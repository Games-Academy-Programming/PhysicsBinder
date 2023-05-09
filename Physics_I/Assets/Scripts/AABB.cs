using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AABB
{
    public Vector3 Position;
    private float offsetX;
    private float offsetY;
    private int width;
    private int height;

    public AABB(float offsetX, float offsetY, int width, int height)
    {
        this.offsetX = offsetX;
        this.offsetY = offsetY;
        this.width = width;
        this.height = height;
    }

    public Rect GetUnityRect()
    {
        throw new NotImplementedException();
    }

    public static bool AreOverlapping(AABB collider1, AABB collider2)
    {
        throw new NotImplementedException();
    }
}

[System.Serializable]
public class AABBCollissionPoint
{
    private AABB collider1;
    private AABB collider2;

    public AABBCollissionPoint(AABB collider1, AABB collider2)
    {
        this.collider1 = collider1;
        this.collider2 = collider2;
    }

    public Vector3 CollissionPoint => throw new NotImplementedException();

    public AABB Collider1 { get => collider1; }
    public AABB Collider2 { get => collider2; }
}
