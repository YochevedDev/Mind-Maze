using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[ExecuteInEditMode]
public class HintPictureGalleryBuilder : MonoBehaviour
{
    [Tooltip("Assign exactly 7 textures: 3 xsmall, 1 large, 1 small, 1 medium, 1 xlarge")]
    public Texture2D[] imageTextures;

    // Scales for small, medium and large pictures
    private Vector3 xsmallScale = new Vector3(0.1f, 0.1f, 0.01f);
    private Vector3 smallScale = new Vector3(0.1f, 0.2f, 0.01f);
    private Vector3 mediumScale = new Vector3(0.2f, 0.2f, 0.01f);

    private Vector3 largeScale = new Vector3(0.31f, 0.25f, 0.01f);
    private Vector3 xlargeScale = new Vector3(0.5f, 0.5f, 0.01f);


    // Positions on the wall (local positions relative to this GameObject)
    private Vector3[] positions = new Vector3[7]
    {
        new Vector3(-0.43f, 0.4f, 0f),   // xSmall #1 - top left
        new Vector3(-0.315f, 0.4f, 0f),   // xSmall #2 - slightly right
        new Vector3(-0.2f, 0.4f, 0f),   // xSmall #3 - slightly right

        new Vector3(-0.315f, 0.1f, 0f),      // Large #1 - center

        new Vector3(-0.43f, -0.28f, 0f), // Small #2 - bottom left
        new Vector3(-0.25f, -0.28f, 0f),   // Medium #1 - bottom right

        new Vector3(0.2f, 0f, 0f),     // xLarge #2 - right
    };

    [ContextMenu("Build Picture Gallery")]
    public void BuildGallery()
    {
        // Validate textures array length
        if (imageTextures == null || imageTextures.Length < 7)
        {
            Debug.LogWarning("Please assign at least 7 textures in the Inspector.");
            return;
        }

        // Remove previous picture objects
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(transform.GetChild(i).gameObject);
        }

        // Create 7 pictures with appropriate size and position
        for (int i = 0; i < 7; i++)
        {
            GameObject pictureObj = new GameObject("Picture_" + i);
            pictureObj.transform.SetParent(this.transform, false);

            // Slightly offset the Z position to prevent z-fighting
            Vector3 pos = positions[i];
            pos.z += 0.001f * i;
            pictureObj.transform.localPosition = pos;

            pictureObj.transform.localPosition = positions[i];
            pictureObj.transform.localRotation = Quaternion.identity;

            // Set scale depending on picture index
            switch (i)
            {
                case 0:
                case 1:
                case 2:
                    pictureObj.transform.localScale = xsmallScale;
                    break;
                case 3:
                    pictureObj.transform.localScale = largeScale;
                    break;
                case 4:
                    pictureObj.transform.localScale = smallScale;
                    break;
                case 5:
                    pictureObj.transform.localScale = mediumScale;
                    break;
                case 6:
                    pictureObj.transform.localScale = xlargeScale;
                    break;
                default:
                    pictureObj.transform.localScale = smallScale; // או ברירת מחדל אחרת
                    break;
            }

            // Add HintPictureBuilder component and assign texture
            HintPictureBuilder builder = pictureObj.AddComponent<HintPictureBuilder>();
            builder.imageTexture = imageTextures[i];
            builder.Build();
        }
    }
}
