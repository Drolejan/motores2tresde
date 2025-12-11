using UnityEngine;

public class ThirdPersonShoot2 : MonoBehaviour
{
    public Camera cam;
    public float range = 100f;
    public GameObject hitEffect;
    public RectTransform crosshair; // mira en el Canvas


void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        // Posición en pantalla del crosshair (Canvas en Screen Space - Overlay)
        Vector3 screenPos = crosshair.position;

        // Ray ya no desde el centro, sino desde la mira
        Ray ray = cam.ScreenPointToRay(screenPos);
        RaycastHit hit;

        Debug.DrawRay(ray.origin, ray.direction * range, Color.cyan, 0.5f);

        if (Physics.Raycast(ray, out hit, range))
        {
            Debug.Log("TPS con mira golpeaste: " + hit.collider.name);

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