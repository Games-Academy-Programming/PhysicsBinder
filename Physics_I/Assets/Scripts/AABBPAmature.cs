using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AABBNS
{
    public class AABBPAmature : MonoBehaviour
    {
        public AABBHandler AABBPhysics = new AABBHandler();

        void FixedUpdate()
        {
            AABBPhysics.Tick(Time.fixedDeltaTime);
        }
    }
}
