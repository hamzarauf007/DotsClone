using UnityEditor;

using UnityEditor.UIElements;
using UnityEngine.UIElements;

[CustomEditor(typeof(Car))]
public class Car_Inspector : UnityEditor.Editor
{
    public VisualTreeAsset m_InspectorXML;
    
    public override VisualElement CreateInspectorGUI()
    {
        // Create a new VisualElement to be the root of our Inspector UI.
        VisualElement myInspector = new VisualElement();

        // Add a simple label.
        // myInspector.Add(new Label("This is a custom Inspector"));

        // Load from default reference.
        m_InspectorXML.CloneTree(myInspector);
        
        
        // Get a reference to the default Inspector Foldout control.
        VisualElement InspectorFoldout = myInspector.Q("Default_Inspector");
    
        // Attach a default Inspector to the Foldout.
        InspectorElement.FillDefaultInspector(InspectorFoldout, serializedObject, this);

        

        // Return the finished Inspector UI.
        return myInspector;

    }
    
    
}
