using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AABBHandler
{
    public List<AABB> AABBColliders = new List<AABB>();
    public List<AABBCollissionPoint> Contacts = new List<AABBCollissionPoint>();

    public void Tick(float DeltaTime)
    {
        throw new NotImplementedException();
    }

    public void RegisterAABB(AABB collider1)
    {
        throw new NotImplementedException();
    }
}