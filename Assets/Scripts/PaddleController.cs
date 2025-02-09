using UnityEngine;

public class PaddleController : MonoBehaviour
{
    public float speed = 10f;
    private float paddleHalfWidth;

    private Vector2 touchStartOffset;
    private bool isDragging = false;
    public float paddleWidth => paddleHalfWidth * 2;

    private void Start()
    {
        // Get the half-width of the paddle dynamically
        paddleHalfWidth = GetComponent<SpriteRenderer>().bounds.extents.x;
    }

    private void Update()
    {
        HandleTouchInput();
        HandleKeyboardInput();
        HandleMouseInput();
    }

    private void HandleTouchInput()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 worldTouchPosition = Camera.main.ScreenToWorldPoint(touch.position);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    if (IsTouchOnPaddle(worldTouchPosition))
                    {
                        isDragging = true;
                        touchStartOffset = new Vector2(transform.position.x - worldTouchPosition.x, 0f);
                    }
                    break;

                case TouchPhase.Moved:
                    if (isDragging)
                    {
                        MovePaddle(worldTouchPosition.x + touchStartOffset.x);
                    }
                    break;

                case TouchPhase.Ended:
                case TouchPhase.Canceled:
                    isDragging = false;
                    break;
            }
        }
    }

    private bool IsTouchOnPaddle(Vector2 touchPosition)
    {
        return touchPosition.x >= transform.position.x - paddleHalfWidth &&
               touchPosition.x <= transform.position.x + paddleHalfWidth &&
               touchPosition.y >= transform.position.y - 0.5f &&
               touchPosition.y <= transform.position.y + 0.5f;
    }

    private void HandleKeyboardInput()
    {
        float moveInput = Input.GetAxis("Horizontal"); // A/D or Left/Right arrow keys
        if (moveInput != 0)
        {
            MovePaddle(transform.position.x + moveInput * speed * Time.deltaTime);
        }
    }

    private void HandleMouseInput()
    {
        if (Input.GetMouseButton(0)) // Left mouse button
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            MovePaddle(mousePosition.x);
        }
    }

    private void MovePaddle(float targetX)
    {
        // Get world boundaries
        float screenLimit = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x;

        // Clamp position within screen bounds
        float clampedX = Mathf.Clamp(targetX, -screenLimit + paddleHalfWidth, screenLimit - paddleHalfWidth);
        transform.position = new Vector2(clampedX, transform.position.y);
    }
}
