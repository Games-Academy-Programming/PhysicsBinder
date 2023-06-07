using System;
using System.Collections.Generic;
using UnityEngine;

namespace AABBNS
{
    [System.Serializable]
    public class MyCharacterController 
    {
        public float Acceleration;
        public float MaxVelocity;

        public void Tick(int Input)
        {
            throw new System.NotImplementedException();
        }

        public MyRigidBody GetRigidBody()
        {
            throw new NotImplementedException();
        }
    }

    [System.Serializable]
    public class MyRigidBody
    {
        public bool IsStatic()
        {
            throw new System.NotImplementedException();
        }

        public void SetIsStatic(bool isStatic)
        {
            throw new System.NotImplementedException();
        }

        public void SetPushBoxExtends(Vector2 extend)
        {
            throw new System.NotImplementedException();
        }

        public void SetPushBoxLocalPosition(Vector2 extend)
        {
            throw new System.NotImplementedException();
        }

        public Vector2 GetPosition()
        {
            throw new System.NotImplementedException();
        }

        public static bool AreOverlapping(MyRigidBody rigidBody1, MyRigidBody rigidBody2)
        {
            throw new System.NotImplementedException();
        }

        public void SetPosition(Vector2 offsetRigidbody1)
        {
            throw new NotImplementedException();
        }

        public void SetVelocity(Vector2 zero)
        {
            throw new NotImplementedException();
        }

        public Vector2 GetVelocity()
        {
            throw new NotImplementedException();
        }
    }

    [System.Serializable]
    public struct RigidBodyContactPoint
    {
        public readonly MyRigidBody Body2;
        public readonly MyRigidBody Body1;

        public RigidBodyContactPoint(MyRigidBody rigidBody1, MyRigidBody rigidBody2) 
        {
            throw new System.NotImplementedException();
        }

        public void SolveOverlap()
        {
            throw new NotImplementedException();
        }
    }

    [System.Serializable]
    public class PhysicsHandler
    {
        [field: SerializeField]
        public List<MyRigidBody> RigidBodies { get; private set; }

        [field: SerializeField]
        public List<RigidBodyContactPoint> Contacts { get; private set; }

        public void SetGravity(float Gravity)
        {
            throw new System.NotImplementedException();
        }

        public void RegisterRigidBody(MyRigidBody rigidbody)
        {
            throw new System.NotImplementedException();
        }

        public void Tick(float DeltaTime)
        {
            throw new System.NotImplementedException();
        }
    }

    [System.Serializable]
    public class MyInputModule
    {
        /// <summary>
        /// return the last pressed direction as input 
        /// 
        /// this'll later be expanded upon to also cover other 
        /// </summary>
        /// <returns></returns>
        public int GetInput()
        {
            throw new System.NotImplementedException();
        }
    }
}