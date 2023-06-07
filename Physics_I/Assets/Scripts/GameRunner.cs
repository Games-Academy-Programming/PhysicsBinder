using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AABBNS
{
    public class GameRunner : MonoBehaviour
    {
        [SerializeField] private PhysicsRunner _PhysicsRunner;

        [SerializeField] private MyRigidBody[] Environment;
        [SerializeField] private CharacterControllerAmature Player;

        // Start is called before the first frame update
        void Start()
        {
            // register all rigidbodies with the physics handler 
            _PhysicsRunner.Physics.RegisterRigidBody(Player.Controller.GetRigidBody());

            foreach (MyRigidBody rb in Environment)
            {
                _PhysicsRunner.Physics.RegisterRigidBody(rb);
            }
        }

        // Update is called once per frame
        void Update()
        {
            // advance the state of the controller 
            Player.Controller.Tick(Player.InputModule.GetInput());
        }
    }
}
