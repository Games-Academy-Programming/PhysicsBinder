using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AABBNS
{
    [System.Serializable]
    public class MyAABB
    {
        public float OffsetX => offsetX;
        public float OffsetY => offsetY;
        public float Width => width;
        public float Height => height;

        public Vector3 Position
        {
            get => new Vector3(offsetX, offsetY, 0);
            set
            {
                offsetX = value.x;
                offsetY = value.y;
            }
        }

        public Vector3 Size { 
            get => new Vector3(width, height, 0); 
            set 
            { 
                width = value.x; 
                height = value.y; 
            }
        }

        [SerializeField] private float offsetX;
        [SerializeField] private float offsetY;
        [SerializeField] private float width;
        [SerializeField] private float height;

        public MyAABB(float offsetX, float offsetY, float width, float height)
        {
            this.offsetX = offsetX;
            this.offsetY = offsetY;
            this.width = width;
            this.height = height;
        }

        public Rect GetUnityRect()
        {
            return new Rect(offsetX, offsetY, width, height);
        }

        public static bool AreOverlapping(MyAABB a, MyAABB b)
        {
            return
                (a.offsetX < (b.offsetX + b.width)) &&
                ((a.offsetX + a.width) > b.offsetX) &&
                (a.offsetY < (b.offsetY + b.height)) &&
                ((a.offsetY + a.height) > b.offsetY);
        }
    }

    [System.Serializable]
    public struct AABBCollissionPoint
    {
        [SerializeField] private MyAABB collider1;
        [SerializeField] private MyAABB collider2;

        public AABBCollissionPoint(MyAABB collider1, MyAABB collider2)
        {
            this.collider1 = collider1;
            this.collider2 = collider2;
        }

        public Vector3 CollissionPoint
        {
            get
            {
                float Right, Left, Bottom, Top;

                Right = Mathf.Min(collider1.OffsetX + collider1.Width, collider2.OffsetX + collider2.Width);
                Left = Mathf.Max(collider1.OffsetX, collider2.OffsetX);
                Top = Mathf.Min(collider1.OffsetY + collider1.Height, collider2.OffsetY + collider2.Height);
                Bottom = Mathf.Max(collider1.OffsetY, collider2.OffsetY);

                float width = Right - Left;
                float heigth = Top - Bottom;

                return new Vector3(Left + width * .5f, Bottom + heigth * .5f, 0);
            }
        }


        public MyAABB Collider1 { get => collider1; }
        public MyAABB Collider2 { get => collider2; }
    }
}