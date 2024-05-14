using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(OrderIcons))]
public class OrderIcons_Inspector : Editor
{
    OrderIcons _orderIcons;
    bool showVariablesIcons;
    bool showArrays;
    bool showPanelConfiguration;

    public override void OnInspectorGUI()
    {
        _orderIcons = (OrderIcons)target;

        showVariablesIcons = EditorGUILayout.Foldout(showVariablesIcons, "Variables Icons");
        if (showVariablesIcons)
        {
            GUILayout.BeginVertical("box");
            _orderIcons.differentImgs = EditorGUILayout.Toggle("Different Images", _orderIcons.differentImgs);
            _orderIcons.paddingX = EditorGUILayout.FloatField("Padding X", _orderIcons.paddingX);
            _orderIcons.paddingY = EditorGUILayout.FloatField("Padding Y", _orderIcons.paddingY);
            _orderIcons.iconWidth = EditorGUILayout.FloatField("Icon Width", _orderIcons.iconWidth);
            _orderIcons.iconHeight = EditorGUILayout.FloatField("Icon Height", _orderIcons.iconHeight);
            GUILayout.EndVertical();
        }

        showArrays = EditorGUILayout.Foldout(showArrays, "Memorama Configuration");
        if (showArrays)
        {
            GUILayout.BeginVertical("box");
            // Mostrar solo las variables seleccionadas
            SerializedProperty arrayImages = serializedObject.FindProperty("imgs");
            EditorGUILayout.PropertyField(arrayImages,true);

            SerializedProperty arrayIcons = serializedObject.FindProperty("iconosList");
            EditorGUILayout.PropertyField(arrayIcons, true);

            SerializedProperty arrayPositions = serializedObject.FindProperty("positions");
            EditorGUILayout.PropertyField(arrayPositions, true);
            
            _orderIcons.panel3D = EditorGUILayout.ObjectField("Panel 3D", _orderIcons.panel3D, typeof(GameObject), true) as GameObject;

            _orderIcons.panelPosition = EditorGUILayout.Vector3Field("Panel Position", _orderIcons.panelPosition);
            
            GUILayout.BeginHorizontal();
            _orderIcons.rows = EditorGUILayout.FloatField("Icons per Row", _orderIcons.rows);
            _orderIcons.columns = EditorGUILayout.FloatField("Icons per Column", _orderIcons.columns);
            GUILayout.EndHorizontal();

            // _orderIcons.panelWidth = EditorGUILayout.FloatField("Panel Width", _orderIcons.panelWidth);
            // _orderIcons.panelHeight = EditorGUILayout.FloatField("Panel Height", _orderIcons.panelHeight);

            GUILayout.EndVertical();
        }

        if (GUILayout.Button("Configuration Game"))
        {
            _orderIcons.StructureMemory();
        }

         serializedObject.ApplyModifiedProperties(); // Aplica los cambios realizados en el Inspector
    }
}
