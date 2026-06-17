using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CellNode))]
public class CellNodeEditor : Editor
{
    private void OnSceneGUI()
    {
        CellNode cell = (CellNode)target;

        foreach (CellNode next in cell.nextNodes)
        {
            if(next == null) continue;

            Handles.DrawLine(
                cell.transform.position,
                next.transform.position);
        }
    }
}