using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Border : MonoBehaviour
{
	public RectTransform rect;
    public Vector2 center;

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        UnityEditor.Handles.color = Color.green;

        UnityEditor.Handles.DrawWireCube(rect.localPosition,rect.rect.size);
        center = rect.rect.position;

        //UnityEditor.Handles.ArrowHandleCap(0, this.transform.position, this.transform.rotation, 1f, EventType.Repaint);
    }
}
