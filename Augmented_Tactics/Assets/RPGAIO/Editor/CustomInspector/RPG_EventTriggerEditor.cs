
using System;
using System.Linq;
using Assets.Scripts.Testing;
using LogicSpawn.RPGMaker.Core;
using UnityEditor;
using UnityEngine;

namespace LogicSpawn.RPGMaker.Editor
{
    [CustomEditor(typeof(RPG_EventTrigger))]
    public class RPG_EventTriggerEditor : UnityEditor.Editor
    {
        SerializedProperty eventID;
        SerializedProperty interactType;
        SerializedProperty distance;
        SerializedProperty allowRetrigger;


        private int selectedItem = 0;

        void OnEnable()
        {
            eventID = serializedObject.FindProperty("EventID");
            interactType = serializedObject.FindProperty("InteractType");
            distance = serializedObject.FindProperty("Distance");
            allowRetrigger = serializedObject.FindProperty("AllowRetrigger");
        }   

        public override void OnInspectorGUI()
        {
            if (Rm_RPGHandler.Instance == null) return;

            serializedObject.Update();
            var s = eventID;

            string[] foundItems = Rm_RPGHandler.Instance.Nodes.EventNodeTrees.Select(i => i.Name).ToArray();

            if(!string.IsNullOrEmpty(s.stringValue))
            {
                NodeTree foundItem = Rm_RPGHandler.Instance.Nodes.EventNodeTrees.FirstOrDefault(i => i.ID == s.stringValue);

                if(foundItem != null)
                    selectedItem = Array.IndexOf(foundItems, foundItem.Name);
            }

            if(foundItems.Length > 0)
            {
                selectedItem = EditorGUILayout.Popup("Event To Run:", selectedItem, foundItems);    
            }
            else
            {
                EditorGUILayout.LabelField("Event To Run:", "None Found");
            }

            EditorGUILayout.PropertyField(interactType, new GUIContent("Condition:"));


            NodeTree item = Rm_RPGHandler.Instance.Nodes.EventNodeTrees.FirstOrDefault(i => i.Name == foundItems[selectedItem]);
            s.stringValue = item != null ? item.ID : "";

            if (interactType.enumValueIndex == (int)InteractType.NearTo)
            {
                EditorGUILayout.PropertyField(distance, new GUIContent("Trigger Distance:"));
            }

            EditorGUILayout.PropertyField(allowRetrigger, new GUIContent("Allow Retrigger"));

            serializedObject.ApplyModifiedProperties();
        }
    }
}
