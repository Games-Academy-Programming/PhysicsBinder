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

        private List<MyAABB> closedList = new List<MyAABB>();

        public void Tick(float DeltaTime)
        {
            // clear at the start to visualize contacts at the end of the Tick
            Contacts.Clear();

            foreach (MyAABB aabb1 in AABBColliders)
            {
                closedList.Add(aabb1);

                foreach (MyAABB aabb2 in AABBColliders)
                {
                    if (closedList.Contains(aabb2))
                    {
                        continue;
                    }

                    if (MyAABB.AreOverlapping(aabb1,aabb2))
                    {
                        Contacts.Add(new AABBCollissionPoint(aabb1, aabb2));
                    }
                }
            }
            closedList.Clear();
        }

        public void RegisterAABB(MyAABB collider)
        {
            AABBColliders.Add(collider);
        }
    }
}
