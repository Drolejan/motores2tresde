using UnityEngine;

public class RaycastShoot : MonoBehaviour
{
    public Camera cam;     
    public float range = 100f;
    public GameObject hitEffect;

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        RaycastHit hit;

        // Visualizar el rayo en escena
        if (Physics.Raycast(ray, out hit, range))
        {
            // Línea solo hasta el impacto
            Debug.DrawLine(ray.origin, hit.point, Color.red, 1f);

            Debug.Log("Golpeaste: " + hit.collider.name);

            GameObject balaeffect = Instantiate(hitEffect, hit.point, Quaternion.identity);
            Destroy(balaeffect, 1f);

            if (hit.collider.CompareTag("Enemy"))
            {
                Destroy(hit.collider.gameObject);
            }
        }
        else
        {
            // Si no golpea, dibuja ray completo
            Debug.DrawRay(ray.origin, ray.direction * range, Color.red, 1f);
        }
    }
}