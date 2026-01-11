using System.Collections.Generic;
using UnityEngine;

public class GameMannager : MonoBehaviour
{
    public Transform player, lastChekPoint;
    public PlayerMovement playerMovement;
    public HasLives playerLives;
    public Level currentLevel;
    public List<AudioClip> musica, finishers;

    private void OnEnable()
    {
        EventBus.OnMuerteJugador += MuerteJugador;
        EventBus.OnSetCheckpoint += ActualizarCheckpoint;
        EventBus.OnResetLevel += ReiniciarNivel;
        EventBus.OnStartLevel += StartLevel;
        EventBus.OnMusicFinished += NextSong;
    }

    private void OnDisable()
    {
        EventBus.OnMuerteJugador -= MuerteJugador;
        EventBus.OnSetCheckpoint -= ActualizarCheckpoint;
        EventBus.OnResetLevel -= ReiniciarNivel;
        EventBus.OnStartLevel -= StartLevel;
        EventBus.OnMusicFinished -= NextSong;
    }

    private void Start()
    {
        player.position = lastChekPoint.position;
        SoundMannager.Instance.Inicio();
        currentSong = -1;
        NextSong();
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
        SoundMannager.Instance.PlaySFX_Pitch(finishers[Random.Range(0, finishers.Count)], 0.2f);
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

    private int currentSong;
    public void NextSong()
    {
        currentSong++;
        if (currentSong == musica.Count -1) currentSong = 0;
        SoundMannager.Instance.ReproducirSiguienteCancion(musica[currentSong]);
    }
}
