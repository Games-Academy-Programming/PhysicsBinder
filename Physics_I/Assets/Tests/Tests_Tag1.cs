using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using AABBNS;

public class AABBTests
{
    [TestCase(0,0, ExpectedResult = true)]
    [TestCase(1, 0, ExpectedResult = false)]
    [TestCase(-1, 0, ExpectedResult = false)]
    [TestCase(0, 1, ExpectedResult = false)]
    [TestCase(0, -1, ExpectedResult = false)]
    [TestCase(.5f, 0, ExpectedResult = true)]
    [TestCase(-.5f, 0, ExpectedResult = true)]
    [TestCase(0, .5f, ExpectedResult = true)]
    [TestCase(0, -.5f, ExpectedResult = true)]
    public bool AABBIntersection(float offsetX, float offsetY)
    {       
        MyAABB collider1 = new MyAABB(0,0,1,1);
        MyAABB collider2 = new MyAABB(offsetX, offsetY, 1, 1);

        return MyAABB.AreOverlapping(collider1, collider2);
    }

    [Test]
    public void GetRectReturnsCorrectRepresentation()
    {
        MyAABB collider = new MyAABB(1, 1, 2, 2);
        Rect expectedUnityRect = new Rect(1, 1, 2, 2);

        Rect ColliderFromGetUnityRect = collider.GetUnityRect();

        Assert.That(ColliderFromGetUnityRect, Is.EqualTo(expectedUnityRect));
    }
}

public class CollissionPointTests
{
    [Test]
    public void AABBIntersectionPointInCenterOfOverlappingArea()
    {
        MyAABB collider1 = new MyAABB(0, 0, 2, 1);
        MyAABB collider2 = new MyAABB(1, 0, 2, 1);

        AABBCollissionPoint collissionPoint = new AABBCollissionPoint(collider1, collider2);

        // colliider overlap in intervals x [1,2] and y [0,1] -> overlap center at {1.5, 0.5}
        Vector2 ExpectedIntersectionCenter = new Vector2(1.5f, 0.5f);
        Vector2 intersectionCenter = collissionPoint.CollissionPoint;
    }
}

public class AABBHandlerTests
{
    [Test]
    public void AABBHandlerFindsContactsIfCollidersOverlap()
    {
        MyAABB collider1 = new MyAABB(0, 0, 2, 1);
        MyAABB collider2 = new MyAABB(1, 0, 2, 1);

        AABBHandler handler = new AABBHandler();

        handler.RegisterAABB(collider1);
        handler.RegisterAABB(collider2);

        handler.Tick(0);

        bool FoundCollissionBetweenBoundingBoxes = handler.Contacts.Count == 1;

        Assert.That(FoundCollissionBetweenBoundingBoxes, Is.True);
    }

    [Test]
    public void AABBHandlerClearsContactBetweenTicks()
    {
        MyAABB collider1 = new MyAABB(0, 0, 2, 1);
        MyAABB collider2 = new MyAABB(1, 0, 2, 1);

        AABBHandler handler = new AABBHandler();

        handler.RegisterAABB(collider1);
        handler.RegisterAABB(collider2);

        handler.Tick(0);

        bool ContactClearedBetweenTicks = handler.Contacts.Count == 1;

        Assert.That(ContactClearedBetweenTicks, Is.True);
    }

    [Test]
    public void AABBHandlerFindsContactsIsNotSelfCollission()
    {
        MyAABB collider1 = new MyAABB(0, 0, 2, 1);
        MyAABB collider2 = new MyAABB(1, 0, 2, 1);

        AABBHandler handler = new AABBHandler();

        handler.RegisterAABB(collider1);
        handler.RegisterAABB(collider2);

        handler.Tick(0);

        bool AABBHandlerFoundSelfCollission = handler.Contacts[0].Collider1 == handler.Contacts[0].Collider2;

        Assert.That(AABBHandlerFoundSelfCollission, Is.False);
    }

    [Test]
    public void AABBHandlerSimulatesBoundingBoxAfterRegistering()
    {
        MyAABB collider = new MyAABB(0, 0, 2, 1);

        AABBHandler handler = new AABBHandler();

        handler.RegisterAABB(collider);

        bool simulationContainsBoundingBox = handler.AABBColliders.Contains(collider);

        Assert.That(simulationContainsBoundingBox, Is.True);
    }
}

