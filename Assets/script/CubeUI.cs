using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class CubeUI : MonoBehaviour
{
    public GameObject uiPanel; // The panel where cube images will be displayed
    public GameObject cubeImagePrefab; // The prefab for each cube image

    private List<Image> cubeImages = new List<Image>();

    // Initialize the UI with cube IDs
    public void Initialize(List<int> cubeIDs)
    {
        // Clear existing images
        foreach (Transform child in uiPanel.transform)
        {
            Destroy(child.gameObject);
        }
        cubeImages.Clear();

        // Create a new image for each cube ID
        foreach (int id in cubeIDs)
        {
            GameObject cubeImage = Instantiate(cubeImagePrefab, uiPanel.transform);
            Text cubeText = cubeImage.GetComponentInChildren<Text>();
            if (cubeText != null)
            {
                cubeText.text = id.ToString(); // Set the text to the cube ID
            }
            cubeImages.Add(cubeImage.GetComponent<Image>());
        }
    }

    // Update the UI based on the player's actions
    public void UpdateUI(int currentIndex, bool correct)
    {
        if (currentIndex < cubeImages.Count)
        {
            if (correct)
            {
                StartCoroutine(ShowColorForSeconds(cubeImages[currentIndex], Color.green, 2f));
            }
            else
            {
                StartCoroutine(ShowColorForSeconds(cubeImages[currentIndex], Color.red, 2f));
            }
        }
    }

    // Coroutine to show the color for a specified number of seconds
    IEnumerator ShowColorForSeconds(Image image, Color color, float seconds)
    {
        image.color = color;
        yield return new WaitForSeconds(seconds);
        image.color = Color.white;
    }

    // Reset the UI to its default state
    public void ResetUI()
    {
        StopAllCoroutines(); // Stop any ongoing color changes
        foreach (var img in cubeImages)
        {
            img.color = Color.white; // Set all images to white color
        }
    }
}
