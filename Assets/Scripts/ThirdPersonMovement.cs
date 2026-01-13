using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    public Animator playerAnim; // Declaramos el animator para conectarlo con los inputs
    public CharacterController controller;
    public Transform cam; // Referencia a la cámara del player
    public float speed = 5f;
    public float turnSpeed = 10f;
    public float gravity = -9.81f;
    public float jumpHeight = 2f;

    float yVelocity;

    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 inputDirection = new Vector3(x, 0, z).normalized;

        if (inputDirection.magnitude >= 0.1f)
        {
            playerAnim.SetBool("isWalk",true);//Entramos al estado isWalk

            // Ángulo relativo a la cámara
            float targetAngle = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg
                                + cam.eulerAngles.y;

            // Rotación suave
            float angle = Mathf.LerpAngle(transform.eulerAngles.y, targetAngle, Time.deltaTime * turnSpeed);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            // Dirección de movimiento
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            // Gravedad básica
            if (controller.isGrounded)
            {
                yVelocity = -1f;
                // Saltar
            if (Input.GetButtonDown("Jump"))
                {
                // Fórmula física: v = sqrt(altura * -2 * gravedad)
                yVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
                playerAnim.SetTrigger("jump");
                }
            }
            else
            {
                yVelocity += gravity * Time.deltaTime;
            } 

            controller.Move((moveDir * speed + Vector3.up * yVelocity) * Time.deltaTime);
        }
        else
        {
            playerAnim.SetBool("isWalk",false);//Salimos del estado isWalk
            // Aplicar gravedad aunque no haya input
            // Gravedad básica
            if (controller.isGrounded)
            {
                yVelocity = -1f;
                // Saltar
            if (Input.GetButtonDown("Jump"))
                {
                // Fórmula física: v = sqrt(altura * -2 * gravedad)
                yVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
                playerAnim.SetTrigger("jump");
                }
            }
            else
            {
                yVelocity += gravity * Time.deltaTime;
            } 

            controller.Move(Vector3.up * yVelocity * Time.deltaTime);
        }
    }
}