using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float salud=100;
    
    public void TakeDamage(float damage)
    {
        salud-=damage;

        if (salud < 1)
        {
            Destroy(gameObject);
        }
    }
}
