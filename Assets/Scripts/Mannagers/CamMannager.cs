using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;

public class CamMannager : MonoBehaviour
{
    public List<CinemachineCamera> cameras;

    private void OnEnable()
    {
        EventBus.OnCambiarCamZone += CambiarCamZone;
    }

    private void OnDisable()
    {
        EventBus.OnCambiarCamZone -= CambiarCamZone;
    }

    private void CambiarCamZone(CinemachineCamera cam)
    {
        foreach (var camera in cameras)
        {
            camera.Priority = 0;
        }

        cam.Priority = 1;
    }
}
