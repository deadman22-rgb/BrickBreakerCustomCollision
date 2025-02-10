using UnityEngine;

public class BrickColorAssigner : MonoBehaviour
{
    public Color[] colorPalette;  

    void Start()
    {
        // Check if colorPalette is populated
        if (colorPalette.Length == 0)
        {
            Debug.LogError("No colors assigned in the colorPalette!");
            return;
        }

        // Get the SpriteRenderer component attached to the brick
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer != null)
        {
            // Pick a random color from the colorPalette
            // can change it in a way where we can just as easily load up the sprites 
            Color randomColor = colorPalette[Random.Range(0, colorPalette.Length)];

            // Ensure the alphas is not zero 
            randomColor.a = Mathf.Max(randomColor.a, 1f);

            spriteRenderer.color = randomColor;
           
        }
        else
        {
            Debug.LogError("Brick does not have a SpriteRenderer component!");
        }
    }

}
