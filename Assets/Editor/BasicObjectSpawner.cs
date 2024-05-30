 using UnityEngine;
using UnityEditor;

namespace Editor
{
    public class BasicObjectSpawner : EditorWindow
    {
        private string objectBaseName = "";
        private int objectId = 0;
        private GameObject gameObjectToSpawn;
        private float objectScale = 1f;
        private float spawnRadius = 5f;

        
        [MenuItem("Tool/Basic Object Spawner")]
        public static void ShowWindow()
        {
            GetWindow<BasicObjectSpawner>("Object Spawner");
        }

        private void OnGUI()
        {
            GUILayout.Label("Spawn New Object", EditorStyles.boldLabel);

            objectBaseName = EditorGUILayout.TextField("Base Name", objectBaseName);
            objectId = EditorGUILayout.IntField("Object Id", objectId);
            objectScale = EditorGUILayout.Slider("Object Scale", objectScale,0.5f, 3f);
            spawnRadius = EditorGUILayout.FloatField("Spawn Radius", spawnRadius);
            gameObjectToSpawn = EditorGUILayout.ObjectField("Spawn Object Name", gameObjectToSpawn, typeof(GameObject), false)as GameObject;

            if (GUILayout.Button("Spawn Object"))
            {
                SpawnObject();
            }

        }

        private void SpawnObject()
        {
            Debug.Log("Its working");
        }
    }
}
