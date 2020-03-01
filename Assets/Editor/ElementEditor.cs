using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Element))]
public class ElementEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        Element element = (Element)target;

        if (GUILayout.Button("Activate"))
        {
            element.ActionPlayer();
        }
    }
}

[CustomEditor(typeof(DoorElement))]
public class DoorElementEditor : ElementEditor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        //DoorElement element = (DoorElement)target;
        //if (GUILayout.Button("Activate"))
        //{
        //    element.Action();
        //}

    }
}
