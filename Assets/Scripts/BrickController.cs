using UnityEngine;

public class BrickController : MonoBehaviour
{
    public GameObject breakEffectPrefab; 
    public AudioClip breakSound; 
    private AudioSource audioSource;
    private int health = 1;
    private void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    public void TakeDamage()
    {
        health--;

        if (breakEffectPrefab != null)
        {
            Instantiate(breakEffectPrefab, transform.position, Quaternion.identity);
        }

        if (breakSound != null)
        {
            AudioSource.PlayClipAtPoint(breakSound, Camera.main.transform.position, 1.0f);
        }

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }


}
