using UnityEngine;

public class Cuerda : MonoBehaviour
{
    public float ropeRange;
    public LayerMask sueloMask;
    public GameObject prefabCuerda;
    public Transform escenario;
    public bool LanzarCuerda()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.up, ropeRange, sueloMask);
        Debug.DrawRay(transform.position, Vector2.up, Color.brown, 3f);
        if (hit.rigidbody != null)
        {
            Instantiate(prefabCuerda, hit.point, Quaternion.identity, escenario);
            return true;
        }
        else
            return false;
    }
}
