using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;
using static Unity.VisualScripting.Member;

public class CamMannager : MonoBehaviour
{
    private List<CinemachineCamera> cameras;
    public GameObject prefabCam;
    public List<BoxCollider2D> cameraZones;
    public Transform playerTransform, camZonesTransform;

    private void OnEnable()
    {
        EventBus.OnCambiarCamZone += CambiarCamZone;
    }

    private void OnDisable()
    {
        EventBus.OnCambiarCamZone -= CambiarCamZone;
    }

    private void Awake()
    {
        cameras = new List<CinemachineCamera>();

        foreach (BoxCollider2D point in cameraZones)
        {
            GameObject camGO = Instantiate(prefabCam, point.transform.position, Quaternion.identity, camZonesTransform);
            if (camGO.transform.GetChild(0).TryGetComponent<CinemachineCamera>(out var cam))
            {
                cam.Priority = 0;
                cam.Follow = playerTransform;
                cameras.Add(cam);

                if (camGO.transform.GetChild(0).TryGetComponent<CinemachineConfiner2D>(out var confiner))
                {
                    confiner.BoundingShape2D = point;
                    confiner.InvalidateBoundingShapeCache();
                }
                if (camGO.transform.GetChild(1).TryGetComponent<BoxCollider2D>(out var trigger))
                {
                    Vector2 worldSize = point.bounds.size;
                    //trigger.offset = point.offset;
                    trigger.size = new Vector2(worldSize.x / trigger.transform.lossyScale.x, worldSize.y / trigger.transform.lossyScale.y);
                    Vector2 worldCenter = point.bounds.center;
                    trigger.offset = trigger.transform.InverseTransformPoint(worldCenter);

                    if (point.gameObject.TryGetComponent<SpriteRenderer>(out var spriteRenderer)) spriteRenderer.enabled = false;
                }
            }
        }

        cameras[0].Priority = 10;
    }

    private void CambiarCamZone(CinemachineCamera cam)
    {
        foreach (var camera in cameras)
        {
            camera.Priority = 0;
        }

        cam.Priority = 10;
    }
}
