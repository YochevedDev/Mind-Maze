using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomBuilder : MonoBehaviour
{
    private Transform roomRoot;
    private string roomName; // Will be set dynamically from the room container name

    [ContextMenu("Build Room")]
    void BuildRoom()
    {
        Initiate();

        // Floor
        GameObject floor = GameObject.CreatePrimitive(PrimitiveType.Cube);
        floor.name = roomName + "_Floor";
        floor.transform.position = new Vector3(0, 0, 0);
        floor.transform.localScale = new Vector3(10, 0.1f, 10);
        floor.GetComponent<Renderer>().material.color = Color.gray;
        floor.transform.parent = roomRoot;

        // Ceiling
        GameObject ceiling = GameObject.CreatePrimitive(PrimitiveType.Cube);
        ceiling.name = roomName + "_Ceiling";
        ceiling.transform.position = new Vector3(0, 3f, 0);
        ceiling.transform.localScale = new Vector3(10, 0.1f, 10);
        ceiling.GetComponent<Renderer>().material.color = Color.gray;
        ceiling.transform.parent = roomRoot;

        // Walls
        CreateWall("BackWall", new Vector3(0, 1.5f, -5f), new Vector3(10, 3, 0.1f));
        CreateWall("FrontWall", new Vector3(0, 1.5f, 5f), new Vector3(10, 3, 0.1f));
        CreateWall("LeftWall", new Vector3(-5f, 1.5f, 0), new Vector3(0.1f, 3, 10));
        CreateWall("RightWall", new Vector3(5f, 1.5f, 0), new Vector3(0.1f, 3, 10));
    }

    void CreateWall(string name, Vector3 pos, Vector3 scale)
    {
        GameObject wall = GameObject.CreatePrimitive(PrimitiveType.Cube);
        wall.name = roomName + "_" + name;
        wall.transform.position = pos;
        wall.transform.localScale = scale;
        wall.GetComponent<Renderer>().material.color = Color.white;
        wall.transform.parent = roomRoot;
    }

    void Initiate()
    {
        // Initialize roomRoot and roomName manually (Start() won't run in editor)
        roomRoot = this.transform;
        roomName = roomRoot.name;

        // Clear previous children
        foreach (Transform child in roomRoot)
        {
            DestroyImmediate(child.gameObject);
        }
    }
}
