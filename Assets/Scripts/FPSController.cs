using UnityEngine;

public class FPSController : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;  

    public float speed = 6f;
    public float mouseSensitivity = 200f;
    public float gravity = -9.81f;

    float xRotation = 0f;
    float yVelocity;

    public Transform respawn;
    public float jumpHeight;
    
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // ------------------------
        // Rotación con mouse
        // ------------------------
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Rotación horizontal del jugador
        transform.Rotate(Vector3.up * mouseX);

        // Rotación vertical de la cámara (limitada)
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);

        cam.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // ------------------------
        // Movimiento WASD
        // ------------------------
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // Dirección respecto a la cámara del jugador
        Vector3 direction = transform.forward * z + transform.right * x;

        if (direction.magnitude > 1f)
            direction.Normalize();

        // ------------------------
        // Gravedad + Salto
        // ------------------------
        if (controller.isGrounded)
        {
            // Mantener al personaje pegado al piso
            yVelocity = -1f;

            // Saltar
            if (Input.GetButtonDown("Jump"))
            {
                // Fórmula física: v = sqrt(altura * -2 * gravedad)
                yVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }
        }
        else
        {
            // Aplicar gravedad mientras está en el aire
            yVelocity += gravity * Time.deltaTime;
        }
        
        Vector3 move = direction * speed + Vector3.up * yVelocity;

        controller.Move(move * Time.deltaTime);

        if(transform.position.y<-10f) transform.position=respawn.position;//Respawn a la coordenada 0.0.0
    }

    
}