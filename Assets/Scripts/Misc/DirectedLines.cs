using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class DirectedLines : MonoBehaviour
{
    [SerializeField] private List<Vector3> positions;
    [SerializeField] private GameObject linePath;
    [SerializeField] private Transform linePoints;
    [SerializeField] private GameObject circlePrefab;

    public void GenerateLine()
    {
        LineRenderer line = linePath.GetComponent<LineRenderer>();

        line.positionCount = positions.Count;

        for(int i = 0; i < positions.Count; i++)
        {
            line.SetPosition(i, positions[i]);

            //GameObject circle = Instantiate(circlePrefab, positions[i], Quaternion.identity);

            //circle.transform.SetParent(linePoints,true);
        }
        
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(DirectedLines))]
public class DirectedLinesEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        DirectedLines objectCreator = (DirectedLines)target;

        if (GUILayout.Button("UpdateLine"))
        {
            objectCreator.GenerateLine();
        }
    }


}
#endif
