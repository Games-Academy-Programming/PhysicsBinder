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
            // clear at the start to visualize contacts at the end of the Tick
            Contacts.Clear();
            Debug.Assert(AABBColliders.Count < 15, "Too many AABBColliders, needs further optimization in collissiondetection!");

            for (int i = 0; i < AABBColliders.Count - 1; i++)
            {
                MyAABB aabb1 = AABBColliders[i];
                for (int j = i + 1; j < AABBColliders.Count; j++)
                {
                    MyAABB aabb2 = AABBColliders[j];

                    if (MyAABB.AreOverlapping(aabb1,aabb2))
                    {
                        Contacts.Add(new AABBCollissionPoint(aabb1, aabb2));
                    }
                }
            }
        }

        public void RegisterAABB(MyAABB collider)
        {
            AABBColliders.Add(collider);
        }
    }
}
