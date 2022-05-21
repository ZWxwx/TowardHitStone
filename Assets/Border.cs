using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Border : MonoBehaviour
{
	public RectTransform rect;
    public Vector2 center;

#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Handles.color = Color.green;

        Handles.DrawWireCube(rect.localPosition,rect.rect.size);
        center = rect.rect.position;

        //UnityEditor.Handles.ArrowHandleCap(0, this.transform.position, this.transform.rotation, 1f, EventType.Repaint);
    }
#endif
}
