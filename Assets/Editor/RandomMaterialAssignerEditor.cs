using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(RandomMaterialAssigner))]
public class RandomMaterialAssignerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        RandomMaterialAssigner script = (RandomMaterialAssigner)target;

        if (GUILayout.Button("Assign Random Materials"))
        {
            script.AssignRandomMaterials();
        }
    }
}
