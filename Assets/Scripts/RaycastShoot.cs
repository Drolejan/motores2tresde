using UnityEngine;

public class RaycastShoot : MonoBehaviour
{
    public Camera cam;     // Cámara desde donde saldrá el raycast
    public float range = 100f;

    void Update()
    {
        // Fire1 normalmente es el click izquierdo del mouse
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        // Ray desde el centro de la pantalla
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, range))
        {
            // Por ahora solo mostramos qué se golpeó
            Debug.Log("Golpeaste: " + hit.collider.name);

            // Ejemplo para práctica:
            // si el objeto tiene tag "Enemy", lo destruimos
            if (hit.collider.CompareTag("Enemy"))
            {
                Destroy(hit.collider.gameObject);
            }
        }
    }
}