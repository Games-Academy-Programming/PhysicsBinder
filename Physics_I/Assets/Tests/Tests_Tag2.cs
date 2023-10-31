using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using AABBNS;

public class Tests_Tag2
{
    [TestCase(0, 0, ExpectedResult = true)]
    [TestCase(1, 0, ExpectedResult = false)]
    [TestCase(-1, 0, ExpectedResult = false)]
    [TestCase(0, 1, ExpectedResult = false)]
    [TestCase(0, -1, ExpectedResult = false)]
    [TestCase(.5f, 0, ExpectedResult = true)]
    [TestCase(-.5f, 0, ExpectedResult = true)]
    [TestCase(0, .5f, ExpectedResult = true)]
    [TestCase(0, -.5f, ExpectedResult = true)]
    public bool RigidBodyIntersection(float offsetX, float offsetY)
    {
        /// ----------------------------------------------------------------
        /// 
        ///                 Arrange
        /// 
        /// ----------------------------------------------------------------
        MyRigidBody rigidBody1 = new MyRigidBody();
        MyRigidBody rigidBody2 = new MyRigidBody();

        Vector2 pushboxSize = Vector2.one;

        rigidBody1.SetPushBoxExtends(pushboxSize);
        rigidBody2.SetPushBoxExtends(pushboxSize);

        Vector2 pushboxOffset = new Vector2(offsetX, offsetY);
        rigidBody1.SetPushBoxLocalPosition(pushboxOffset);

        /// ----------------------------------------------------------------
        /// 
        ///                 Act & Assert
        /// 
        /// ----------------------------------------------------------------
        return MyRigidBody.AreOverlapping(rigidBody1, rigidBody2);
    }

    [Test]
    public void PhysicsHandlerFindsContactsIfCollidersOverlap()
    {
        /// ----------------------------------------------------------------
        /// 
        ///                 Arrange
        /// 
        /// ----------------------------------------------------------------     
        MyRigidBody rigidBody1 = new MyRigidBody();
        MyRigidBody rigidBody2 = new MyRigidBody();

        Vector2 pushboxSize = new Vector2(2, 1);

        rigidBody1.SetPushBoxExtends(pushboxSize);
        rigidBody2.SetPushBoxExtends(pushboxSize);

        Vector2 pushboxOffset = new Vector2(1, 0);
        rigidBody1.SetPushBoxLocalPosition(pushboxOffset);

        PhysicsHandler handler = new PhysicsHandler();

        handler.RegisterRigidBody(rigidBody1);
        handler.RegisterRigidBody(rigidBody2);

        /// ----------------------------------------------------------------
        /// 
        ///                 Act
        /// 
        /// ----------------------------------------------------------------
        handler.Tick(0);

        bool FoundCollissionBetweenBoundingBoxes = handler.Contacts.Count == 1;

        /// ----------------------------------------------------------------
        /// 
        ///                 Assert
        /// 
        /// ----------------------------------------------------------------
        Assert.That(FoundCollissionBetweenBoundingBoxes, Is.True);
    }

    [Test]
    public void PhysicsHandlerDoesNotAccumulateContactsOverTime()
    {
        /// ----------------------------------------------------------------
        /// 
        ///                 Arrange
        /// 
        /// ----------------------------------------------------------------     
        MyRigidBody rigidBody1 = new MyRigidBody();
        MyRigidBody rigidBody2 = new MyRigidBody();

        Vector2 pushboxSize = new Vector2(2, 1);

        rigidBody1.SetPushBoxExtends(pushboxSize);
        rigidBody2.SetPushBoxExtends(pushboxSize);

        Vector2 pushboxOffset = new Vector2(1, 0);
        rigidBody1.SetPushBoxLocalPosition(pushboxOffset);

        PhysicsHandler handler = new PhysicsHandler();

        handler.RegisterRigidBody(rigidBody1);
        handler.RegisterRigidBody(rigidBody2);

        /// ----------------------------------------------------------------
        /// 
        ///                 Act
        /// 
        /// ----------------------------------------------------------------
        handler.Tick(0);

        /// next frame of the simulation
        handler.Tick(0);

        bool FoundCollissionBetweenBoundingBoxes = handler.Contacts.Count == 1;

        /// ----------------------------------------------------------------
        /// 
        ///                 Assert
        /// 
        /// ----------------------------------------------------------------
        Assert.That(FoundCollissionBetweenBoundingBoxes, Is.True);
    }

    [Test]
    public void PhysicsHandlerDoesNotReportCollissionsOfAnObjectWithItself()
    {
        /// ----------------------------------------------------------------
        /// 
        ///                 Arrange
        /// 
        /// ----------------------------------------------------------------     
        MyRigidBody rigidBody1 = new MyRigidBody();
        MyRigidBody rigidBody2 = new MyRigidBody();

        Vector2 pushboxSize = new Vector2(2, 1);

        rigidBody1.SetPushBoxExtends(pushboxSize);
        rigidBody2.SetPushBoxExtends(pushboxSize);

        Vector2 pushboxOffset = new Vector2(1, 0);
        rigidBody1.SetPushBoxLocalPosition(pushboxOffset);

        PhysicsHandler handler = new PhysicsHandler();

        handler.RegisterRigidBody(rigidBody1);
        handler.RegisterRigidBody(rigidBody2);

        /// ----------------------------------------------------------------
        /// 
        ///                 Act
        /// 
        /// ----------------------------------------------------------------
        handler.Tick(0);        

        /// ----------------------------------------------------------------
        /// 
        ///                 Assert
        /// 
        /// ----------------------------------------------------------------
        bool AABBHandlerFoundSelfCollission = handler.Contacts[0].Body1 == handler.Contacts[0].Body2;

        Assert.That(AABBHandlerFoundSelfCollission, Is.False);
    }

    [Test]
    public void CollissionPointResolutionDoesNotmoveStaticBodies()
    {
        /// ----------------------------------------------------------------
        /// 
        ///                 Arrange
        /// 
        /// ----------------------------------------------------------------     
        MyRigidBody rigidBody1 = new MyRigidBody();
        MyRigidBody rigidBody2 = new MyRigidBody();

        Vector2 pushboxSize = new Vector2(2, 1);

        rigidBody1.SetPushBoxExtends(pushboxSize);
        rigidBody2.SetPushBoxExtends(pushboxSize);

        rigidBody1.SetIsStatic(true);
        rigidBody2.SetIsStatic(true);

        Vector2 OffsetRigidbody1 = new Vector2(1, 0);
        rigidBody1.SetPosition(OffsetRigidbody1);

        RigidBodyContactPoint contactPoint = new RigidBodyContactPoint(rigidBody1, rigidBody2);

        /// ----------------------------------------------------------------
        /// 
        ///                 Act
        /// 
        /// ----------------------------------------------------------------
        Vector2 PositionOfBody_1_BeforeSolving = rigidBody1.GetPosition();
        Vector2 PositionOfBody_2_BeforeSolving = rigidBody2.GetPosition();

        contactPoint.SolveOverlap();
        /// ----------------------------------------------------------------
        /// 
        ///                 Assert
        /// 
        /// ----------------------------------------------------------------
        Assert.That(rigidBody1.GetPosition(), Is.EqualTo(PositionOfBody_1_BeforeSolving));
        Assert.That(rigidBody2.GetPosition(), Is.EqualTo(PositionOfBody_2_BeforeSolving));
    }

    [TestCase(true, false)]
    [TestCase(false, true)]
    public void CollissionPointResolutionMovesNoneStaticBody(bool body1Static, bool body2Static)
    {
        /// ----------------------------------------------------------------
        /// 
        ///                 Arrange
        /// 
        /// ----------------------------------------------------------------     
        MyRigidBody rigidBody1 = new MyRigidBody();
        MyRigidBody rigidBody2 = new MyRigidBody();

        Vector2 pushboxSize = new Vector2(2, 1);

        rigidBody1.SetPushBoxExtends(pushboxSize);
        rigidBody2.SetPushBoxExtends(pushboxSize);

        rigidBody1.SetIsStatic(body1Static);
        rigidBody2.SetIsStatic(body2Static);

        Vector2 OffsetRigidbody1 = new Vector2(1, 0);
        rigidBody1.SetPosition(OffsetRigidbody1);

        RigidBodyContactPoint contactPoint = new RigidBodyContactPoint(rigidBody1, rigidBody2);

        /// ----------------------------------------------------------------
        /// 
        ///                 Act
        /// 
        /// ----------------------------------------------------------------
        Vector2 PositionOfBody_1_BeforeSolving = rigidBody1.GetPosition();
        Vector2 PositionOfBody_2_BeforeSolving = rigidBody2.GetPosition();

        contactPoint.SolveOverlap();
        /// ----------------------------------------------------------------
        /// 
        ///                 Assert
        /// 
        /// ----------------------------------------------------------------
        bool body1WasMovedByCollissionResolution = rigidBody1.GetPosition() == PositionOfBody_1_BeforeSolving;
        bool body2WasMovedByCollissionResolution = rigidBody2.GetPosition() == PositionOfBody_2_BeforeSolving;

        Assert.That(body1WasMovedByCollissionResolution, Is.EqualTo(body1Static));
        Assert.That(body2WasMovedByCollissionResolution, Is.EqualTo(body2Static));
    }

    [Test]
    public void CollissionPointResolutionResolvesCollission()
    {
        /// ----------------------------------------------------------------
        /// 
        ///                 Arrange
        /// 
        /// ----------------------------------------------------------------     
        MyRigidBody rigidBody1 = new MyRigidBody();
        MyRigidBody rigidBody2 = new MyRigidBody();

        Vector2 pushboxSize = new Vector2(2, 1);

        rigidBody1.SetPushBoxExtends(pushboxSize);
        rigidBody2.SetPushBoxExtends(pushboxSize);

        rigidBody1.SetIsStatic(true);

        Vector2 OffsetRigidbody1 = new Vector2(1, 0);
        rigidBody1.SetPosition(OffsetRigidbody1);

        RigidBodyContactPoint contactPoint = new RigidBodyContactPoint(rigidBody1, rigidBody2);

        /// ----------------------------------------------------------------
        /// 
        ///                 Act
        /// 
        /// ----------------------------------------------------------------

        contactPoint.SolveOverlap();
        /// ----------------------------------------------------------------
        /// 
        ///                 Assert
        /// 
        /// ----------------------------------------------------------------
        Assert.That(MyRigidBody.AreOverlapping(rigidBody1, rigidBody2), Is.False);
    }

    [Test]
    public void PhysicsHandlerAppliesGravity()
    {
        /// ----------------------------------------------------------------
        /// 
        ///                 Arrange
        /// 
        /// ----------------------------------------------------------------     
        MyRigidBody rigidBodyWithGravity = new MyRigidBody();
     
        PhysicsHandler handler = new PhysicsHandler();

        handler.RegisterRigidBody(rigidBodyWithGravity);

        rigidBodyWithGravity.SetVelocity(Vector2.zero);
        rigidBodyWithGravity.SetPosition(Vector2.zero);

        float time = 1 / 60f;
        float gravity = 2;

        handler.SetGravity(gravity);

        /// ----------------------------------------------------------------
        /// 
        ///                 Act
        /// 
        /// ----------------------------------------------------------------
        handler.Tick(time);

        float expectedPosition = -time * time * gravity;

        /// ----------------------------------------------------------------
        /// 
        ///                 Assert
        /// 
        /// ----------------------------------------------------------------
        Assert.That(rigidBodyWithGravity.GetPosition().y, Is.EqualTo(expectedPosition));
    }

    /// <summary>
    /// INputs in numpad notation. 
    /// 
    /// 6 forward
    /// 4 backward
    /// 
    /// 8 up
    /// 2 down
    /// 
    /// 3 downforward
    /// 1 downback
    /// 
    /// 9 upforward
    /// 7 upback
    /// </summary>
    /// <param name="input">input in numpad notation</param>
    [TestCase(1,    -1, -1)] 
    [TestCase(2,    0,  -1)]
    [TestCase(3,    1,  -1)]
    [TestCase(4,    -1,  0)]
    [TestCase(5,    0,   0)]
    [TestCase(6,    +1,  0)]
    [TestCase(7,    -1, +1)]
    [TestCase(8,    0,  +1)]
    [TestCase(9,    +1, +1)]
    public void InputDirectionAppliesForceToRigidBodyOfController(int input, int xVelocity, int yVelocity)
    {
        /// ----------------------------------------------------------------
        /// 
        ///                 Arrange
        /// 
        /// ----------------------------------------------------------------     
        MyCharacterController controller = new MyCharacterController
        {
            Acceleration = 1f
        };

        Vector2 expectedVelocity = new Vector2(xVelocity, yVelocity);

        /// ----------------------------------------------------------------
        /// 
        ///                 Act
        /// 
        /// ----------------------------------------------------------------
        controller.Tick(input);

        /// ----------------------------------------------------------------
        /// 
        ///                 Assert
        /// 
        /// ----------------------------------------------------------------
        /// 
        Assert.That(controller.GetRigidBody().GetVelocity(), Is.EqualTo(expectedVelocity));
    }
}