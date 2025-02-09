# Brick Breaker 

This repository contains a Brick Breaker game built in Unity. The core focus is on manually handling collisions and movement without using Unity's built-in physics system.

Project Overview
The game consists of:

A custom ball movement and collision system (no Rigidbody2D or Unity physics)
A brick-breaking mechanic with dynamically generated levels
A LevelManager that loads level data from JSON and spawns bricks
Visual effects like camera shake when bricks are broken

==========================================================


## CODE BREAKDOWN 

# 1. LevelManager.cs
This script handles level generation by loading a predefined layout from a JSON file and spawning bricks accordingly.

Key Functions:
Start()

Loads level data and calls GenerateLevel() if data is valid.
LoadLevelData()

Reads levelData.json from the Resources folder.
Deserializes JSON into a LevelData object.
Validates data before passing it to GenerateLevel().
GenerateLevel()

Places bricks in the scene based on JSON-defined layout.
Aligns bricks to fit the screen using brickSpacing values.
Ensures rows and columns are centered properly.


==============================================================

# 2. Collision Handling
We manually define brick boundaries to detect collisions:


`` float brickLeft = brick.transform.position.x - 0.25f;
float brickRight = brick.transform.position.x + 0.25f;
float brickTop = brick.transform.position.y + 0.15f;
float brickBottom = brick.transform.position.y - 0.15f; `` 
The offset values (0.25, 0.15) represent the brick's half-width and half-height.
This prevents collision detection from being too strict at the exact center.

================================================================

Manual collision detection for ball movement
Randomized brick colors per game restart
Game effects like camera shake upon breaking bricks
A clean UI and stylized game feel

===============================================================

**Setup & Installation**
Clone this repository.
Open the project in Unity.
Ensure the levelData.json file is inside Resources/.
Run the game in Unity Editor.


