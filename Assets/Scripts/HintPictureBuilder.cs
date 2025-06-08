using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[ExecuteInEditMode]
public class HintPictureBuilder : MonoBehaviour
{
    // Texture to show inside the frame
    public Texture2D imageTexture;

    // Builds a framed picture with the assigned texture
    [ContextMenu("Build Hint Picture")]
    public void Build()
    {
        // Remove previous framed picture if exists
        Transform existing = transform.Find("FramedPicture");
        if (existing != null)
        {
            DestroyImmediate(existing.gameObject);
        }

        // Create root object for framed picture
        GameObject root = new GameObject("FramedPicture");
        root.transform.SetParent(transform, false);
        root.transform.localPosition = Vector3.zero;
        root.transform.localRotation = Quaternion.identity;
        root.transform.localScale = Vector3.one;

        // Create frame (cube primitive)
        GameObject frame = GameObject.CreatePrimitive(PrimitiveType.Cube);
        frame.name = "Frame";
        frame.transform.SetParent(root.transform, false);
        frame.transform.localPosition = Vector3.zero;
        frame.transform.localRotation = Quaternion.identity;
        frame.transform.localScale = new Vector3(1.1f, 1.6f, 0.01f);

        var frameRenderer = frame.GetComponent<Renderer>();
        frameRenderer.sharedMaterial.color = new Color(0.2f, 0.1f, 0f, 1f); // dark brown, fully opaque
        frameRenderer.sortingOrder = 0; // מסגרת תצא ראשונה

        // Create the picture quad to display texture
        GameObject pictureQuad = GameObject.CreatePrimitive(PrimitiveType.Quad);
        pictureQuad.name = "PictureQuad";
        pictureQuad.transform.SetParent(root.transform, false);
        pictureQuad.transform.localPosition = new Vector3(0, 0, -0.02f); // הקטן הזה מפחית z-fighting
        pictureQuad.transform.localRotation = Quaternion.identity;
        pictureQuad.transform.localScale = new Vector3(1f, 1.3f, 1f);

        var quadRenderer = pictureQuad.GetComponent<Renderer>();

        // Assign texture to the quad if available
        if (imageTexture != null)
        {
            Material mat = new Material(Shader.Find("Unlit/Texture"));
            mat.mainTexture = imageTexture;
            mat.renderQueue = 3000; // מבטיח שהתמונה תצויר אחרי המסגרת
            quadRenderer.sharedMaterial = mat;
            Debug.LogWarning("new HintPictureBuilder.");

        }
        else
        {
            Debug.LogWarning("No image texture assigned to HintPictureBuilder.");
        }
        quadRenderer.sortingOrder = 1; // התמונה תצויר אחרי המסגרת
    }
}
