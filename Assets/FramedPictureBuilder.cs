using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[ExecuteInEditMode]
public class HintPictureBuilder : MonoBehaviour
{
    public Texture2D imageTexture;

    [ContextMenu("Build Hint Picture")]
    void Build()
    {
        // Clear only previously built picture, don't destroy this object!
        Transform existing = transform.Find("FramedPicture");
        if (existing != null)
        {
            DestroyImmediate(existing.gameObject);
        }

        // Create root inside this object
        GameObject root = new GameObject("FramedPicture");
        root.transform.SetParent(this.transform, false); // Don't change this.transform position
        root.transform.localPosition = Vector3.zero;
        root.transform.localRotation = Quaternion.identity;
        root.transform.localScale = Vector3.one;

        // Create frame
        GameObject frame = GameObject.CreatePrimitive(PrimitiveType.Cube);
        frame.name = "Frame";
        frame.transform.SetParent(root.transform, false);
        frame.transform.localPosition = Vector3.zero;
        frame.transform.localRotation = Quaternion.identity;
        frame.transform.localScale = new Vector3(1.1f, 1.6f, 0.01f);
        frame.GetComponent<Renderer>().sharedMaterial.color = new Color(0.2f, 0.1f, 0f); // dark brown

        // Create picture
        GameObject pictureQuad = GameObject.CreatePrimitive(PrimitiveType.Quad);
        pictureQuad.name = "PictureQuad";
        pictureQuad.transform.SetParent(root.transform, false);
        pictureQuad.transform.localPosition = new Vector3(0, 0, -0.01f);
        pictureQuad.transform.localRotation = Quaternion.identity;
        pictureQuad.transform.localScale = new Vector3(1f, 1.3f, 1f);

        // Apply texture
        if (imageTexture != null)
        {
            Material mat = new Material(Shader.Find("Unlit/Texture"));
            mat.mainTexture = imageTexture;
            pictureQuad.GetComponent<Renderer>().sharedMaterial = mat;
        }
        else
        {
            Debug.LogWarning("No image texture assigned to HintPictureBuilder.");
        }
    }
}
