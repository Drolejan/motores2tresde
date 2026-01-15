using UnityEngine;

public class powerUpSound : MonoBehaviour
{
    public AudioClip miClip;
    void OnTriggerEnter(Collider other)
    {
        AudioSource.PlayClipAtPoint(miClip,transform.position);
        Destroy(gameObject);
    }
}
