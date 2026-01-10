using UnityEngine;

public class Caja : MonoBehaviour
{
    private Vector3 initPos;

    private void Awake()
    {
        initPos = transform.position;
    }

    public void ResetCaja()
    {
        if (initPos != null)
            transform.position = initPos;
    }
}
