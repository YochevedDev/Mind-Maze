using UnityEngine;
using UnityEditor;
using System.IO;

public class PrefabSpawner : MonoBehaviour
{
    [Header("Set path inside Assets/, for example: Assets/Scenes/ZNS3D/Vintage Living Room Game Pack/Prefabs")]
    public string folderPath = "Assets/YourPrefabFolderHere";
    public Vector3 startPosition = Vector3.zero;
    public Vector3 offset = new Vector3(3, 0, 0);

    void Start()
    {
#if UNITY_EDITOR
        string[] prefabFiles = Directory.GetFiles(folderPath, "*.prefab", SearchOption.AllDirectories);
        Vector3 currentPosition = startPosition;

        foreach (string prefabPath in prefabFiles)
        {
            GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(prefabPath);
            if (prefab != null)
            {
                GameObject instance = PrefabUtility.InstantiatePrefab(prefab) as GameObject;
                instance.transform.position = currentPosition;
                currentPosition += offset;
            }
        }
#endif
    }
}
