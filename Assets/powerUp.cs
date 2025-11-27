using UnityEngine;

public class powerUp : MonoBehaviour
{
    public float power=20f;
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<RaycastShoot>())
        {
            Debug.Log("POWERUP");
            other.gameObject.GetComponent<RaycastShoot>().shootDmg+=power;
            Destroy(gameObject,0.1f);
        }
    }

}
