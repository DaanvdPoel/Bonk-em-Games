using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Support : MonoBehaviour
{
    static public void DrawRay(Vector3 position, Vector3 direction, Color color)
    {
        if (direction.sqrMagnitude > 0.001f)
        {
            Debug.DrawRay(position, direction, color);
            UnityEditor.Handles.color = color;
            UnityEditor.Handles.DrawSolidDisc(position + direction, Vector3.up, 0.25f);
        }
    }

    static public void DrawLabel(Vector3 position, string label, Color color)
    {
        UnityEditor.Handles.BeginGUI();
        UnityEditor.Handles.color = color;
        UnityEditor.Handles.Label(position, label);
        UnityEditor.Handles.EndGUI();
    }

    static public void DrawSolidDisc(Vector3 position, Color color)
    {
        UnityEditor.Handles.color = color;
        UnityEditor.Handles.DrawSolidDisc(position, Vector3.up, 0.25f);
    }

    static public void DrawWireDisc(Vector3 position, float radius ,Color color)
    {
        UnityEditor.Handles.color = color;
        UnityEditor.Handles.DrawWireDisc(position,Vector3.up , radius);
    }

    static public void ChangeObjectColor(GameObject gameObject, Color color)
    {
        gameObject.GetComponent<MeshRenderer>().material.color = color;
    }
}
