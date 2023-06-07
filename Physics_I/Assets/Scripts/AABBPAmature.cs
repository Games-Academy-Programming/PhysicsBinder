using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AABBNS
{
    public class AABBPAmature : MonoBehaviour
    {
        public AABBHandler AABBPhysics = new AABBHandler();

        public float MaxTime { get => 1/30f; }

        void Update()
        {
            float time = Time.deltaTime;

            if (time > MaxTime)
            {
                time = MaxTime;
            }

            AABBPhysics.Tick(time);
        }
    }
}
