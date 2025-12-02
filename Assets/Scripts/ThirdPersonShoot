using UnityEngine;

public class ThirdPersonShoot : MonoBehaviour
{
    public Camera cam;          // Cámara que sigue al personaje (la que ve el jugador)
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
        // Ray desde el centro de la pantalla (igual que en primera persona)
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        RaycastHit hit;

        // Solo para que veas la línea en Scene
        Debug.DrawRay(ray.origin, ray.direction * range, Color.cyan, 0.5f);

        if (Physics.Raycast(ray, out hit, range))
        {
            Debug.Log("TPS Golpeaste: " + hit.collider.name);

            if (hitEffect != null)
            {
                GameObject balaEffect = Instantiate(hitEffect, hit.point, Quaternion.identity);
                Destroy(balaEffect, 1f);
            }

            if (hit.collider.CompareTag("Enemy"))
            {
                Destroy(hit.collider.gameObject);
            }
        }
    }
}