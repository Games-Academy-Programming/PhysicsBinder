using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AABBNS
{
    public class PhysicsRunner : MonoBehaviour
    {
        // Update is called once per frame
        public float MaxTime { get => 1 / 30f; }

        [field: SerializeField] public PhysicsHandler Physics { get; private set; }

        void FixedUpdate()
        {
            float time = Time.deltaTime;

            if (time > MaxTime)
            {
                time = MaxTime;
            }

            Physics.Tick(time);
        }
    }
}
