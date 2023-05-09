using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AABBNS
{
    [System.Serializable]
    public class MyAABB
    {
        public Vector3 Position;
        private float offsetX;
        private float offsetY;
        private float width;
        private float height;

        public MyAABB(float offsetX, float offsetY, float width, float height)
        {
            this.offsetX = offsetX;
            this.offsetY = offsetY;
            this.width = width;
            this.height = height;
        }

        public Rect GetUnityRect()
        {
            throw new System.NotImplementedException();
        }

        public static bool AreOverlapping(MyAABB a, MyAABB b)
        {
            throw new System.NotImplementedException();
        }
    }

    [System.Serializable]
    public class AABBCollissionPoint
    {
        private MyAABB collider1;
        private MyAABB collider2;

        public AABBCollissionPoint(MyAABB collider1, MyAABB collider2)
        {
            this.collider1 = collider1;
            this.collider2 = collider2;
        }

        public Vector3 CollissionPoint => throw new NotImplementedException();

        public MyAABB Collider1 { get => collider1; }
        public MyAABB Collider2 { get => collider2; }
    }
}