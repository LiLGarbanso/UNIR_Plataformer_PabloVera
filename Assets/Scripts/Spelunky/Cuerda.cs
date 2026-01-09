using System.Collections.Generic;
using UnityEngine;

public class Cuerda : MonoBehaviour
{
    public float ropeRange;
    public LayerMask sueloMask;
    public GameObject prefabCuerda;
    public Transform escenario, player;
    private List<GameObject> cuerdasActivas = new List<GameObject>();

    private void OnEnable()
    {
        EventBus.OnMuerteJugador += LimpiarCuedas;
    }

    private void OnDisable()
    {
        EventBus.OnMuerteJugador -= LimpiarCuedas;
    }

    public bool LanzarCuerda()
    {
        RaycastHit2D hit = Physics2D.Raycast(player.position, Vector2.up, ropeRange, sueloMask);
        Debug.DrawRay(transform.position, Vector2.up, Color.brown, 3f);
        if (hit.rigidbody != null)
        {
            cuerdasActivas.Add(Instantiate(prefabCuerda, hit.point, Quaternion.identity, escenario));
            return true;
        }
        else
            return false;
    }

    public void LimpiarCuedas()
    {
        foreach (GameObject go in cuerdasActivas)
        {
            Destroy(go);
        }
        cuerdasActivas.Clear();
    }
}
