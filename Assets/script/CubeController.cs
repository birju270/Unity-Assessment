using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class CubeController : MonoBehaviour
{
    public GameObject cubePrefab;
    public int numberOfCubes = 5;
    public Transform spawnArea;
    public float spawnRadius = 10.0f;
    public Text rangeWarningText;
    public Transform playerStartPosition;
    public GameObject player;
    public CubeUI cubeUI;
    public Text victoryText; // Add a reference to the victory text UI

    public Toggle ascendingToggle;
    public Toggle descendingToggle;

    private List<GameObject> cubes = new List<GameObject>();
    private List<int> cubeIDs = new List<int>();
    private int currentIndex = 0;
    private bool isGameFrozen = false;

    void Start()
    {
        victoryText.enabled = false; // Ensure the victory text is hidden at the start

        if (numberOfCubes < 3 || numberOfCubes > 10)
        {
            ShowRangeWarning();
            return;
        }

        ascendingToggle.onValueChanged.AddListener(delegate { OnToggleChanged(); });
        descendingToggle.onValueChanged.AddListener(delegate { OnToggleChanged(); });

        SpawnCubes();
        InitializeCubeOrder();
        cubeUI.Initialize(cubeIDs);
    }

    void ShowRangeWarning()
    {
        rangeWarningText.text = "Incorrect range";
        Invoke("QuitGame", 5f); // Quit the game after 5 seconds
    }

    void SpawnCubes()
    {
        for (int i = 0; i < numberOfCubes; i++)
        {
            Vector3 spawnPosition = spawnArea.position + Random.insideUnitSphere * spawnRadius;
            spawnPosition.y = spawnArea.position.y; // Ensure cubes spawn at the same height level
            GameObject cube = Instantiate(cubePrefab, spawnPosition, Quaternion.identity);
            int id = Random.Range(0, 101);
            while (cubeIDs.Contains(id))
            {
                id = Random.Range(0, 101);
            }
            cube.GetComponent<Cube>().Initialize(id);
            cubeIDs.Add(id);
            cubes.Add(cube);
        }
    }

    void InitializeCubeOrder()
    {
        cubeIDs.Sort();
        if (descendingToggle.isOn)
        {
            cubeIDs.Reverse();
        }
    }

    void OnToggleChanged()
    {
        InitializeCubeOrder();
        cubeUI.Initialize(cubeIDs);
        ResetGame();
    }

    public void CheckJumpedCube(int id)
    {
        if (isGameFrozen) return;

        if (cubeIDs[currentIndex] == id)
        {
            // Correct jump
            cubeUI.UpdateUI(currentIndex, true);
            currentIndex++;
            if (currentIndex >= cubeIDs.Count)
            {
                // Victory
                Debug.Log("Victory!");
                ShowVictory();
            }
        }
        else
        {
            // Incorrect jump
            cubeUI.UpdateUI(currentIndex, false);
            StartCoroutine(HandleIncorrectJump());
        }
    }

    IEnumerator HandleIncorrectJump()
    {
        isGameFrozen = true;
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(3);
        Time.timeScale = 1f;
        player.transform.position = playerStartPosition.position;
        player.transform.rotation = playerStartPosition.rotation;
        cubeUI.ResetUI();
        currentIndex = 0;
        isGameFrozen = false;
    }

    void ResetGame()
    {
        currentIndex = 0;
        cubeUI.ResetUI();
        victoryText.enabled = false; // Hide victory text when the game resets
    }

    void ShowVictory()
    {
        victoryText.enabled = true; // Show the victory text
    }

    void QuitGame()
    {
        Application.Quit();
    }
}

