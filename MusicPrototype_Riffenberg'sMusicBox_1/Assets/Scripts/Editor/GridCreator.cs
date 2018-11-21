using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GridScript))]

public class GridCreator : Editor
{
	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();
		DrawDefaultInspector();
		if(GUILayout.Button("GridCreate",GUILayout.Width(150)))
		{
				GridScript grid = target as GridScript;
				grid.GenerateGrid();
		}
	}

}
