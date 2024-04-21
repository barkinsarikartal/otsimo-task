using UnityEngine;

public class BucketTool : MonoBehaviour
{
    // Reference to the canvas image component
    public UnityEngine.UI.Image canvasImage;

    // Fill color
    public Color fillColor;

    public void Update()
    {
        if (Input.GetMouseButton(0))
        {
            FillCanvas();
        }
    }

    // Method to perform the bucket fill operation
    public void FillCanvas()
    {
        // Get the texture of the canvas image
        Texture2D texture = (Texture2D)canvasImage.mainTexture;

        // Create a color array to represent each pixel of the texture
        Color[] pixels = texture.GetPixels();

        // Loop through all pixels and set them to the fill color
        for (int i = 0; i < pixels.Length; i++)
        {
            pixels[i] = fillColor;
        }

        // Apply the modified pixels array to the texture
        texture.SetPixels(pixels);

        // Apply the changes to the texture
        texture.Apply();
    }
}
