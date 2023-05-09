using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AABBPAmature : MonoBehaviour
{
    public AABBHandler AABBPhysics = new AABBHandler();

    void FixedUpdate()
    {
        AABBPhysics.Tick(Time.fixedDeltaTime);
    }
}
