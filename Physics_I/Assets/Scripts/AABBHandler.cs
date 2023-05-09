using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AABBNS
{
    [System.Serializable]
    public class AABBHandler
    {
        public List<MyAABB> AABBColliders = new List<MyAABB>();
        public List<AABBCollissionPoint> Contacts = new List<AABBCollissionPoint>();

        public void Tick(float DeltaTime)
        {
            throw new NotImplementedException();
        }

        public void RegisterAABB(MyAABB collider1)
        {
            throw new NotImplementedException();
        }
    }
}