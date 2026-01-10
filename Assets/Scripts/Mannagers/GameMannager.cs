using UnityEngine;

public class GameMannager : MonoBehaviour
{
    public Transform player, lastChekPoint;
    public PlayerMovement playerMovement;
    public HasLives playerLives;
    public Level currentLevel;

    private void OnEnable()
    {
        EventBus.OnMuerteJugador += MuerteJugador;
        EventBus.OnSetCheckpoint += ActualizarCheckpoint;
        EventBus.OnResetLevel += ReiniciarNivel;
        EventBus.OnStartLevel += StartLevel;
    }

    private void OnDisable()
    {
        EventBus.OnMuerteJugador -= MuerteJugador;
        EventBus.OnSetCheckpoint -= ActualizarCheckpoint;
        EventBus.OnResetLevel -= ReiniciarNivel;
        EventBus.OnStartLevel -= StartLevel;
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
        ReiniciarNivel();
    }

    public void StartLevel(Level l)
    {
        currentLevel = l;
        playerLives.ResetLives();
    }

    public void ReiniciarNivel()
    {
        playerMovement.ResetPlayer();
        playerLives.ResetLives();
        player.gameObject.SetActive(true);

        if (!currentLevel.GetLevelStatus())
        {
            player.position = currentLevel.startPos.position;
            currentLevel.ResetLevel();
        }
        else
        {
            player.position = currentLevel.levelTarget.position;
        }
    }
}
