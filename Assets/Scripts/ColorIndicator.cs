using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ColorIndicator : MonoBehaviour
{
    public List<string> colorNames;

    // Define the size of each color block
    public float blockSize = 50f;

    public void UpdateColorBlocks(List<string> colorNames)
    {
        // Clear previous color blocks
        ClearColorBlocks();

        // Calculate initial position for the first color block
        Vector2 position = new Vector2(-75, -30);

        // Iterate through the list of color names
        foreach (string colorName in colorNames)
        {
            // Create a new UI Image GameObject for each color block
            GameObject colorBlockGO = new GameObject("ColorBlock", typeof(Image));
            colorBlockGO.transform.SetParent(transform);

            // Set the color of the image based on the color name
            Color color = Color.white; // Default to white if color name is invalid
            if (ColorUtility.TryParseHtmlString(colorName, out color))
            {
                colorBlockGO.GetComponent<Image>().color = color;
            }

            // Set the size and position of the color block
            RectTransform rectTransform = colorBlockGO.GetComponent<RectTransform>();
            rectTransform.sizeDelta = new Vector2(blockSize, blockSize);
            rectTransform.anchoredPosition = position;

            // Move the position for the next color block
            position.x += 5;

            // Add a tag to the color block GameObject
            colorBlockGO.tag = "ColorBlock";
        }
    }

    private void ClearColorBlocks()
    {
        // Destroy existing color blocks with the "ColorBlock" tag
        GameObject[] colorBlocks = GameObject.FindGameObjectsWithTag("ColorBlock");
        foreach (GameObject block in colorBlocks)
        {
            Destroy(block);
        }
    }
}
