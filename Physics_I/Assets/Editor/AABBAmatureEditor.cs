using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using AABBNS;

[CustomEditor(typeof(AABBPAmature))]
public class AABBAmatureEditor : Editor
{
    AABBHandler _AABBPhysics;
    float _CollissionPointRadius = .5f;
    Color _CollissionPointColor = Color.yellow;
    Color _ColliderColor = Color.green;

    public override void OnInspectorGUI()
    {
        _CollissionPointRadius = EditorGUILayout.FloatField("CollissionPointRadius", _CollissionPointRadius);

        base.OnInspectorGUI();
    }

    public void OnSceneGUI()
    {
        _AABBPhysics = (target as AABBPAmature).AABBPhysics;

        if (_AABBPhysics == null)
        {
            return;
        }

        foreach (MyAABB aabb in _AABBPhysics.AABBColliders)
        {
            aabb.Position = Handles.PositionHandle(aabb.Position, Quaternion.identity);
            Handles.DrawSolidRectangleWithOutline(aabb.GetUnityRect(), Color.clear, _ColliderColor);
        }

        Handles.color = _CollissionPointColor;
        foreach (AABBCollissionPoint cp in _AABBPhysics.Contacts)
        {
            Handles.DrawWireDisc(cp.CollissionPoint, Vector3.forward, _CollissionPointRadius);
        }
    }
}