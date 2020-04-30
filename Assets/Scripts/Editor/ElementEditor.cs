using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Element))]
public class ElementEditor : Editor
{
    string[] choices = new[] { "StandIdle,SitDown,SitIdle,SitUp" };
    int choiceIndex = 0;
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        Element element = (Element)target;

        choiceIndex = EditorGUILayout.Popup(choiceIndex, choices);
        element.
    }
}

[CustomEditor(typeof(InteractableElement))]
public class InteractableElementEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        InteractableElement element = (InteractableElement)target;

        if (GUILayout.Button("Activate"))
        {
            element.ActionPlayer();
        }
    }
}

    [CustomEditor(typeof(DoorElement))]
public class DoorElementEditor : InteractableElementEditor
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
