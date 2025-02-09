using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private Vector3 originalPosition;
    public float shakeIntensity = 0.1f;  
    public float shakeDuration = 0.2f;   
    private float shakeTimer = 0f;

    private void Start()
    {
        originalPosition = transform.position;
    }

    private void Update()
    {
        // If the shake timer is greater than 0, we be tripping 
        if (shakeTimer > 0)
        {
            transform.position = originalPosition + (Vector3)Random.insideUnitCircle * shakeIntensity;

            shakeTimer -= Time.deltaTime;  // Decrease the shake time
        }
        else
        {
            // Reset the position after the shake ends
            transform.position = originalPosition;
        }
    }

    public void Shake()
    {
        shakeTimer = shakeDuration;
    }


}
