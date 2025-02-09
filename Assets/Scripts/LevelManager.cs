using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public GameObject brickPrefab;  
    private LevelData levelData;    // Reference to the level data 

    void Start()
    {
        Debug.Log("LevelManager Start() called!");
        levelData = LoadLevelData();  // Load the level data from the JSON file
        if (levelData != null)
        {
            Debug.Log("Calling GenerateLevel()");
            GenerateLevel();  // Generate the level when the game starts
        }
        else
        {
            Debug.LogError("Level data is null, not calling GenerateLevel()");
        }
    }

    private LevelData LoadLevelData()
    {
        // Load the level data from the Resources folder
        TextAsset jsonFile = Resources.Load<TextAsset>("levelData");
        if (jsonFile == null)
        {
            Debug.LogError("Level data JSON file not found in Resources folder!");
            return null;
        }

        Debug.Log($"Loaded JSON: {jsonFile.text}");

        // Deserialize the JSON into LevelData object
        LevelData data = JsonUtility.FromJson<LevelData>(jsonFile.text);
        if (data == null)
        {
            Debug.LogError("JSON deserialization failed! Data is null.");
            return null;
        }

        // Ensure brick layout is not null or empty
        if (data.brickLayout == null || data.brickLayout.Count == 0)
        {
            Debug.LogError("Brick layout is null or empty!");
            return null;
        }

        // Reformat the brick layout to handle potential issues with row data
        List<BrickRow> formattedLayout = new List<BrickRow>();
        foreach (var row in data.brickLayout)
        {
            if (row.row == null)
            {
                Debug.LogError("A row in brickLayout is NULL!");
                continue;
            }

            formattedLayout.Add(new BrickRow { row = new List<int>(row.row) });
        }

        data.brickLayout = formattedLayout;
        Debug.Log($"Level data parsed successfully! Rows: {data.rows}, Columns: {data.columns}, Brick Rows: {data.brickLayout.Count}");

        return data;
    }

    private void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.R))
        {   
            
            ReloadCurrentScene();      
        }
    }

    private void ModifyBrickLayout()
    {
        // Randomize the brick layout,basically change 0's and 1's in json UNLIMITED LEVELS !!! 
        for (int row = 0; row < levelData.brickLayout.Count; row++)
        {
            for (int col = 0; col < levelData.brickLayout[row].row.Count; col++)
            {
                levelData.brickLayout[row].row[col] = Random.Range(0, 2);  // Randomize between 0 and 1
            }
        }

        // Log the modified brick layout for debugging
        /* Debug.Log("Modified Brick Layout:");
        foreach (var row in levelData.brickLayout)
        {
            string rowData = string.Join(", ", row.row);
            Debug.Log(rowData);
        }
        */

        
    }

    public void GenerateLevel()
    {
        ModifyBrickLayout();
        // Clear any existing bricks in the scene before regenerating the level

        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);  // Destroy all existing bricks
        }

        if (levelData == null || levelData.brickLayout == null || levelData.brickLayout.Count == 0)
        {
            Debug.LogError("Level data or brick layout is null/empty, cannot generate level.");
            return;
        }

        Debug.Log($"Generating Level! Total Rows: {levelData.brickLayout.Count}");

        // Calculate positioning for the bricks
        int maxColumns = levelData.columns;
        float totalWidth = (maxColumns - 1) * levelData.brickSpacing.x;
        float startX = -totalWidth / 2;  // Center horizontally

        float totalHeight = (levelData.brickLayout.Count - 1) * levelData.brickSpacing.y;
        float startY = Camera.main.orthographicSize - levelData.brickSpacing.y;  // Top of the screen

        // Loop through the brick layout and instantiate the bricks in the correct positions
        for (int row = 0; row < levelData.brickLayout.Count; row++)
        {
            for (int col = 0; col < levelData.brickLayout[row].row.Count; col++)
            {
                int brickType = levelData.brickLayout[row].row[col];

                if (brickType == 1)  // Only place brick if its value is 1
                {
                    Vector3 position = new Vector3(
                        startX + (col * levelData.brickSpacing.x),  // X position
                        startY - (row * levelData.brickSpacing.y),  // Y position
                        0  // Z position (set to 0 as we're in a 2D game)
                    );

                    // Instantiate the brick prefab at the correct position
                    GameObject brick = Instantiate(brickPrefab, position, Quaternion.identity);
                    brick.transform.SetParent(transform);  // Parent the brick to this LevelManager for easy cleanup
                }
            }
        }

        Debug.Log("Level generation complete!");
    }

    public void ReloadCurrentScene()
    {
        // Get the current scene and reload it
        UnityEngine.SceneManagement.Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }
}
