using System;
using UnityEditor;
using UnityEngine;
using TMPro;

[CustomEditor(typeof(Register))]
public class Register_Inspector : Editor
{
    Register _register;
    bool showRegister;
    bool showScripts;
    public override void OnInspectorGUI()
    {
        _register = (Register)target;
        // base.OnInspectorGUI(); Para tener el inspector por defecto

        // if (GUILayout.Button("Check Register"))
        // {
        //     _register.CheckRegister();
        // }

        showScripts = EditorGUILayout.Foldout(showScripts, "Scripts");
        if (showScripts)
        {
            GUILayout.BeginVertical("box");
            _register.uiManager = EditorGUILayout.ObjectField(new GUIContent("UI Manager", "Link to the UI Manager Script"), _register.uiManager, typeof(UIManager), true) as UIManager;
            _register.main = EditorGUILayout.ObjectField(new GUIContent("Main", "Link to the Main Script"), _register.main, typeof(Main), true) as Main;
            GUILayout.EndVertical();
        }


        showRegister = EditorGUILayout.Foldout(showRegister, "Register");
        if (showRegister)
        {
            GUILayout.BeginVertical("box");
            _register.registerExist = EditorGUILayout.Toggle("Register Exist", _register.registerExist);

            _register.nameInputField = EditorGUILayout.ObjectField(new GUIContent("Name Input Field", "Link to take the user name"), _register.nameInputField, typeof(TMP_InputField), true) as TMP_InputField;
            _register.emailInputField = EditorGUILayout.ObjectField("Email Input Field", _register.emailInputField, typeof(TMP_InputField), true) as TMP_InputField;
            _register.phoneInputField = EditorGUILayout.ObjectField("Phone Input Field", _register.phoneInputField, typeof(TMP_InputField), true) as TMP_InputField;
            GUILayout.EndVertical();
        }
    }
}
