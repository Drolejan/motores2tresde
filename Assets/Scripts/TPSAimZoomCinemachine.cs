using UnityEngine;
using Unity.Cinemachine;

public class TPSAimZoomCinemachine : MonoBehaviour
{
    public CinemachineCamera cinemachineCam;   // Nuevo tipo
    public float normalFOV = 60f;
    public float zoomFOV = 30f;
    public float aimSpeed = 10f;

    bool isAiming;

    void Update()
    {
        isAiming = Input.GetMouseButton(1);

        float targetFOV = isAiming ? zoomFOV : normalFOV;

        // Acceso a la lente en Cinemachine 3.x
        var lens = cinemachineCam.Lens;
        lens.FieldOfView = Mathf.Lerp(lens.FieldOfView, targetFOV, Time.deltaTime * aimSpeed);
        cinemachineCam.Lens = lens;
    }
}