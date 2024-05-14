using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

[CustomEditor(typeof(OrderIcons))]
public class OrderIcons_Inspector : Editor
{
    OrderIcons _orderIcons;
    bool showSounds;
    bool showVariablesIcons;
    bool showArrays;
    bool score;

    public override void OnInspectorGUI()
    {
        _orderIcons = (OrderIcons)target;

        _orderIcons.timeToPlay = EditorGUILayout.IntField("Time to Play", _orderIcons.timeToPlay);

        showSounds = EditorGUILayout.Foldout(showSounds, "Sounds");
        if (showSounds)
        {
            GUILayout.BeginVertical("box");
            _orderIcons.sounds = EditorGUILayout.ObjectField("Sounds Manager", _orderIcons.sounds, typeof(SoundsManager), true) as SoundsManager;
            GUILayout.EndVertical();
        }

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
            
            _orderIcons.panel3D = EditorGUILayout.ObjectField("Panel 3D", _orderIcons.panel3D, typeof(GameObject), true) as GameObject;
            _orderIcons.panelPosition = EditorGUILayout.Vector3Field("Panel Position", _orderIcons.panelPosition);
            
            GUILayout.BeginHorizontal();
            _orderIcons.rows = EditorGUILayout.FloatField("Icons per Row", _orderIcons.rows);
            _orderIcons.columns = EditorGUILayout.FloatField("Icons per Column", _orderIcons.columns);
            GUILayout.EndHorizontal();

            GUILayout.EndVertical();
        }

        if (GUILayout.Button("Configuration Game"))
        {
            _orderIcons.StructureMemory();
        }

        score = EditorGUILayout.Foldout(score, "Score");
        if (score)
        {
            GUILayout.BeginVertical("box");
            _orderIcons.timeText = EditorGUILayout.ObjectField("Time Text", _orderIcons.timeText, typeof(TextMeshProUGUI), true) as TextMeshProUGUI;
            _orderIcons.scoreTxt = EditorGUILayout.ObjectField("Score Text", _orderIcons.scoreTxt, typeof(TextMeshProUGUI), true) as TextMeshProUGUI;
            _orderIcons.Finalscore = EditorGUILayout.ObjectField("Final Score", _orderIcons.Finalscore, typeof(TextMeshProUGUI), true) as TextMeshProUGUI;
            GUILayout.EndVertical();
        }
        

        serializedObject.ApplyModifiedProperties(); // Aplica los cambios realizados en el Inspector
    }
}
