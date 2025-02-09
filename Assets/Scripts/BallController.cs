/*
 using UnityEngine;
using System.Collections.Generic;

public class BallController : MonoBehaviour
{
    public Vector2 velocity = new Vector2(1f, 1f); // Initial direction
    public float speed = 5f;
    private PaddleController paddle;
    private List<BrickController> bricks;

    // Reference to CameraShake component
    public CameraShake cameraShake;

    private void Start()
    {
        paddle = FindObjectOfType<PaddleController>(); // Get paddle reference
        bricks = new List<BrickController>(FindObjectsOfType<BrickController>()); // Get all bricks
    }

    private void Update()
    {
        // Move the ball based on velocity
        transform.position += (Vector3)(velocity.normalized * speed * Time.deltaTime);

        // Handle custom collision detection
        HandleWallCollisions();
        HandlePaddleCollision();
        HandleBrickCollisions();
    }

    private void HandleWallCollisions()
    {
        Vector2 screenBounds = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));

        // Bounce off left/right walls
        if (transform.position.x < -screenBounds.x || transform.position.x > screenBounds.x)
        {
            velocity.x *= -1;
            transform.position = new Vector2(Mathf.Clamp(transform.position.x, -screenBounds.x, screenBounds.x), transform.position.y);
        }

        // Bounce off the top wall
        if (transform.position.y > screenBounds.y)
        {
            velocity.y *= -1;
            transform.position = new Vector2(transform.position.x, screenBounds.y);
        }

        // Reset ball if it falls below the screen
        if (transform.position.y < -screenBounds.y)
        {
            ResetBall();
        }
    }

    private void HandlePaddleCollision()
    {
        if (paddle == null) return;

        // Define the paddle's bounds
        float paddleLeft = paddle.transform.position.x - (paddle.paddleWidth / 2);
        float paddleRight = paddle.transform.position.x + (paddle.paddleWidth / 2);
        float paddleTop = paddle.transform.position.y + 0.5f; // Adjust based on paddle height

        // Check if the ball is touching the paddle
        if (transform.position.y <= paddleTop &&
            transform.position.x >= paddleLeft &&
            transform.position.x <= paddleRight)
        {
            // Ball hit the paddle, calculate new bounce direction
            float relativeHitPoint = (transform.position.x - paddle.transform.position.x) / (paddle.paddleWidth / 2);
            velocity.x = Mathf.Clamp(relativeHitPoint * 5f, -4f, 4f);
            velocity.y = Mathf.Abs(velocity.y) + 0.5f; // Ensure it bounces upwards
            velocity = velocity.normalized; // Maintain consistent speed

            Debug.Log("Ball hit paddle! Adjusting bounce angle.");
        }
    }

    private void HandleBrickCollisions()
    {
        for (int i = bricks.Count - 1; i >= 0; i--)
        {
            BrickController brick = bricks[i];
            if (brick == null) continue;

            // Define brick bounds
            float brickLeft = brick.transform.position.x - 0.25f; // Assuming 0.5 width
            float brickRight = brick.transform.position.x + 0.25f;
            float brickTop = brick.transform.position.y + 0.15f; // Assuming 0.3 height
            float brickBottom = brick.transform.position.y - 0.15f;

            // Check if ball is inside brick bounds
            if (transform.position.x >= brickLeft && transform.position.x <= brickRight &&
                transform.position.y >= brickBottom && transform.position.y <= brickTop)
            {
                // Ball collided with a brick
                brick.TakeDamage(); // Call TakeDamage() instead of destroying directly
                velocity.y *= -1; // Reverse direction

                Debug.Log("Ball hit a brick! Reversing Y direction.");

                // Trigger camera shake
                if (cameraShake != null)
                {
                    cameraShake.Shake(); // Trigger the camera shake effect
                }

                bricks.RemoveAt(i); // Remove brick from the list
                break; // Exit the loop after the first brick collision
            }
        }
    }

    private void ResetBall()
    {
        transform.position = Vector2.zero;
        velocity = new Vector2(1f, 1f).normalized; // Reset direction
    }
}




*/

using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;

public class BallController : MonoBehaviour
{
    public Vector2 velocity = new Vector2(1f, 1f); // Ball's initial direction
    public float speed = 5f; // Ball speed
    private PaddleController paddle; // Reference to the paddle script
    private List<BrickController> bricks; // List of all brick objects
    [SerializeField] private GameObject pauseGameObject;
    private bool isGamePaused; 

    // Reference to CameraShake for visual feedback on collisions
    public CameraShake cameraShake;

    private void Start()
    {   
        isGamePaused = false;
        pauseGameObject.SetActive(false);
        // Get references to the paddle and all bricks
        paddle = FindObjectOfType<PaddleController>();
        bricks = new List<BrickController>(FindObjectsOfType<BrickController>());
    }

    private void FixedUpdate()
    {
        // Move the ball by applying velocity and speed
        transform.position += (Vector3)(velocity.normalized * speed * Time.deltaTime);

        // Check for collisions with walls, paddle, and bricks
        HandleWallCollisions();
        HandlePaddleCollision();
        HandleBrickCollisions();
        
    }
    private void Update()
    {
        PauseGameCheck();
    }

    // Handle ball's collisions with walls
    private void HandleWallCollisions()
    {
        // Get screen bounds in world space
        Vector2 screenBounds = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));

        // Reverse direction if ball hits left/right walls
        if (transform.position.x < -screenBounds.x || transform.position.x > screenBounds.x)
        {
            velocity.x *= -1;
            transform.position = new Vector2(Mathf.Clamp(transform.position.x, -screenBounds.x, screenBounds.x), transform.position.y);
        }

        // Reverse direction if ball hits top wall
        if (transform.position.y > screenBounds.y)
        {
            velocity.y *= -1;
            transform.position = new Vector2(transform.position.x, screenBounds.y);
        }

        // Reset ball if it falls below the screen
        if (transform.position.y < -screenBounds.y)
        {
            ResetBall();
        }
    }

    // Handle ball's collision with the paddle
    private void HandlePaddleCollision()
    {
        if (paddle == null) return;

        // Get paddle's bounds
        float paddleLeft = paddle.transform.position.x - (paddle.paddleWidth / 2);
        float paddleRight = paddle.transform.position.x + (paddle.paddleWidth / 2);
        float paddleTop = paddle.transform.position.y + 0.5f; // Adjust based on paddle height

        // Check if ball hits the paddle
        if (transform.position.y <= paddleTop &&
            transform.position.x >= paddleLeft &&
            transform.position.x <= paddleRight)
        {
            // Calculate new bounce direction based on paddle hit point
            float relativeHitPoint = (transform.position.x - paddle.transform.position.x) / (paddle.paddleWidth / 2);
            velocity.x = Mathf.Clamp(relativeHitPoint * 5f, -4f, 4f);
            velocity.y = Mathf.Abs(velocity.y) + 0.5f; // Ensure upward bounce
            velocity = velocity.normalized; // Maintain constant speed

            // Debug log for paddle collision
            Debug.Log("Ball hit paddle! Adjusting bounce angle.");
        }
    }

    // Handle ball's collision with bricks
    private void HandleBrickCollisions()
    {
        // Iterate through bricks in reverse to avoid modifying the list during iteration
        for (int i = bricks.Count - 1; i >= 0; i--)
        {
            BrickController brick = bricks[i];
            if (brick == null) continue; // Skip if brick is destroyed

            // Get brick's bounds dynamically
            Bounds brickBounds = brick.GetComponent<Renderer>().bounds;

            float brickLeft = brickBounds.min.x - 0.3f;
            float brickRight = brickBounds.max.x + 0.3f;
            float brickTop = brickBounds.max.y + 0.12f;
            float brickBottom = brickBounds.min.y - 0.12f;


            //// Get brick's bounds
            //float brickLeft = brick.transform.position.x - 0.25f;
            //float brickRight = brick.transform.position.x + 0.25f;
            //float brickTop = brick.transform.position.y + 0.15f;
            //float brickBottom = brick.transform.position.y - 0.15f;

            // Check if ball is within brick bounds
            if (transform.position.x >= brickLeft && transform.position.x <= brickRight &&
                transform.position.y >= brickBottom && transform.position.y <= brickTop)
            {
                // Ball hit a brick: trigger damage, reverse direction, and shake camera
                brick.TakeDamage();
                velocity.y *= -1;

                // Trigger camera shake on brick hit
                if (cameraShake != null)
                {
                    cameraShake.Shake();
                }

                bricks.RemoveAt(i); // Remove brick from the list
                break; // Exit loop after handling one collision
            }
        }
    }

    // Reset ball position and velocity
    private void ResetBall()
    {
        transform.position = Vector2.zero; // Center position
        velocity = new Vector2(1f, 1f).normalized; // Reset direction
    }

    private void PauseGameCheck()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGameMethod();
        }
    }

    public void PauseGameMethod()
    {
        if (isGamePaused)
        {
            pauseGameObject.SetActive(false);
            Time.timeScale = 1f;  // Resume the game
        }
        else
        {
            pauseGameObject.SetActive(true);
            Time.timeScale = 0f;  // Pause the game
        }
        isGamePaused = !isGamePaused;  // Toggle the game state
    }
}

