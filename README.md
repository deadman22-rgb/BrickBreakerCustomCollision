# Brick Breaker 

This repository contains a Brick Breaker game built in Unity. The core focus is on manually handling collisions and movement without using Unity's built-in physics system.

Project Overview
The game consists of:

A custom ball movement and collision system (no Rigidbody2D or Unity physics)
A brick-breaking mechanic with dynamically generated levels
A LevelManager that loads level data from JSON and spawns bricks
Visual effects like camera shake when bricks are broken

========================================================================


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


========================================================================

# 2. Collision Handling
We manually define brick boundaries to detect collisions:


`` float brickLeft = brick.transform.position.x - 0.25f; ``
`` float brickRight = brick.transform.position.x + 0.25f; ``
`` float brickTop = brick.transform.position.y + 0.15f; `` 
`` float brickBottom = brick.transform.position.y - 0.15f; `` 

The offset values (0.25, 0.15) represent the brick's half-width and half-height.
This prevents collision detection from being too strict at the exact center.

========================================================================

Manual collision detection for ball movement
Randomized brick colors per game restart
Game effects like camera shake upon breaking bricks
A clean UI and stylized game feel

========================================================================

# Paddle Controller 

the Core Functions that is handling our paddle controller contains the :

Start(): Gets paddle width using SpriteRenderer.bounds.extents.x.

HandleKeyboardInput(): Moves the paddle when arrow keys or A/D keys are pressed.

HandleMouseInput(): Moves the paddle based on the mouse position while holding the left button.

HandleTouchInput(): Detects touch input, allows dragging when touched, and updates position.

MovePaddle(float targetX): Moves the paddle while ensuring it stays within screen limits.

# Ball Controller / Custom Collision Handling 

this is our main script that handles all the collisons i will explain it in three parts :- 


**1. Ball-Wall Collision**
We determine screen boundaries using Camera.main.ScreenToWorldPoint().
If the ball's X position goes beyond the left/right boundaries, we invert its X velocity to bounce it back.
If the ball’s Y position is greater than the top boundary, we invert its Y velocity to keep it in play.
If the ball falls below the bottom boundary, we call ResetBall() to reposition it at the center.

**2. Ball-Paddle Collision**
We check if the ball’s position overlaps with the paddle's area.
If the ball is within the paddle’s X range and Y range, a collision has occurred.
The ball’s bounce direction is adjusted based on where it hits the paddle:
Hitting the center results in a straight bounce.
Hitting closer to the edges adds more horizontal movement, making it behave dynamically.
The bounce effect is calculated using a relative hit point and adjusting the X velocity accordingly.


**3. Ball-Brick Collision**
We iterate over all bricks and check if the ball's position overlaps with any brick's bounds.
If a collision is detected:
The brick is destroyed (brick.TakeDamage()).
The ball’s Y velocity is reversed, making it bounce off the brick.
A camera shake effect is triggered for better feedback.


========================================================================
**Setup & Installation**
Clone this repository.
Open the project in Unity.
Ensure the levelData.json file is inside Resources/.
Run the game in Unity Editor.


