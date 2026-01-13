using UnityEngine;

public class audioPlaySound : MonoBehaviour
{
    public AudioSource fuente;

    void OnTriggerEnter(Collider other)
    {
        if (!fuente.isPlaying)
        {
           fuente.Play(); 
        }
        else
        {
            fuente.Pause();
        }
        
    }
}
