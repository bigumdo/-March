//using BGD.Tests;
using UnityEditor;
using UnityEngine;

namespace BGD.CustomEditors
{
    //[CustomEditor(typeof(TestAbstract),true)]
    //public class MyScriptEditor : Editor
    //{
    //    public override void OnInspectorGUI()
    //    {
    //        serializedObject.Update();

    //        EditorGUILayout.LabelField("Base Character Editor", EditorStyles.boldLabel);
    //        //EditorGUILayout.PropertyField(serializedObject.FindProperty("showDetails"));
    //        //EditorGUILayout.PropertyField(serializedObject.FindProperty("basicInfo"));
    //        //EditorGUILayout.PropertyField(serializedObject.FindProperty("detailedInfo"));

    //        // 개별 클래스의 필드도 자동 적용됨
    //        SerializedProperty iterator = serializedObject.GetIterator();
    //        bool enterChildren = true;
            
    //        while (iterator.NextVisible(enterChildren))
    //        {
    //            EditorGUILayout.PropertyField(iterator, false);

    //            enterChildren = false;
    //        }

    //        serializedObject.ApplyModifiedProperties();
    //    }
    //}
}
