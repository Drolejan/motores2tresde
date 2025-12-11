using UnityEngine;
using UnityEngine.UI;

public class CrosshairController : MonoBehaviour
{
    public RectTransform crosshair;
    public float sensitivity = 500f;

    Vector2 screenBounds;

    void Start()
    {
        // Tamaño de la pantalla
        screenBounds = new Vector2(Screen.width, Screen.height);
    }

    void Update()
    {
        // Movimiento con mouse
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        // Nueva posición de la mira
        Vector3 newPos = crosshair.position + new Vector3(mouseX, mouseY, 0);

        // Mantener la mira dentro de la pantalla
        newPos.x = Mathf.Clamp(newPos.x, 0, screenBounds.x);
        newPos.y = Mathf.Clamp(newPos.y, 0, screenBounds.y);

        crosshair.position = newPos;
    }
}