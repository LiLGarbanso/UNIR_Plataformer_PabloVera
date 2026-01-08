using UnityEngine;

public class GameMannager : MonoBehaviour
{
    public Transform player, lastChekPoint;
    public PlayerMovement playerMovement;
    public HasLives playerLives;

    private void OnEnable()
    {
        EventBus.OnMuerteJugador += MuerteJugador;
        EventBus.OnSetCheckpoint += ActualizarCheckpoint;
    }

    private void OnDisable()
    {
        EventBus.OnMuerteJugador -= MuerteJugador;
        EventBus.OnSetCheckpoint -= ActualizarCheckpoint;
    }

    private void Start()
    {
        player.position = lastChekPoint.position;
        SoundMannager.Instance.Inicio();
    }

    public void ActualizarCheckpoint(Transform point)
    {
        lastChekPoint = point;
    }

    public void MuerteJugador()
    {
        playerMovement.ResetPlayer();
        playerLives.ResetLives();
        player.position = lastChekPoint.position;
        player.gameObject.SetActive(true);
    }
}
