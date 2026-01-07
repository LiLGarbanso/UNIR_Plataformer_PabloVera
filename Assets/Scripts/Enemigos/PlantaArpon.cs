using UnityEngine;

public class PlantaArpon : MonoBehaviour
{
    public GameObject pincho;
    public Transform centroPlanta;
    public LineRenderer lineRend;
    public float hookedTime, returnTime, hookSpeed, hookRange;
    private bool isAttacking, targetReached;
    private Vector3 hookDir;
    public int dmg;

    private void Start()
    {
        isAttacking = false;
        lineRend.SetPosition(0, centroPlanta.position);
        lineRend.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isAttacking)
        {
            pincho.transform.position += Time.deltaTime * hookDir;
            lineRend.SetPosition(1, pincho.transform.position);
            if (pincho.transform.position.sqrMagnitude > hookRange)
                isAttacking = false;
        }
    }

    public void PresaDetectada(Vector3 pos)
    {
        if (isAttacking) return;
        lineRend.enabled = true;
        hookDir = Vector2.zero;
        hookDir = pos - centroPlanta.position;
        hookDir.Normalize();
        //hookDir *= hookRange;
        isAttacking = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isAttacking)
        {
            if (collision != null)
            {
                isAttacking = false;
                if(collision.gameObject.TryGetComponent<HasLives>(out var player))
                {
                    player.TakeDamage(dmg);
                }
            }
        }
    }
}
