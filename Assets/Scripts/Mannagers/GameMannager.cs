using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameMannager : MonoBehaviour
{
    public Transform player, lastChekPoint;
    public PlayerMovement playerMovement;
    public HasLives playerLives;
    public Level currentLevel;
    public List<AudioClip> musica, finishers;
    public PlayerInput playerInput;
    public GameObject playerUI, playerGO;

    private void OnEnable()
    {
        EventBus.OnMuerteJugador += MuerteJugador;
        EventBus.OnResetLevel += ReiniciarNivel;
        EventBus.OnStartLevel += StartLevel;
        EventBus.OnMusicFinished += NextSong;
    }

    private void OnDisable()
    {
        EventBus.OnMuerteJugador -= MuerteJugador;
        EventBus.OnResetLevel -= ReiniciarNivel;
        EventBus.OnStartLevel -= StartLevel;
        EventBus.OnMusicFinished -= NextSong;
    }

    private void Start()
    {
        playerInput.actions.FindActionMap("UI").Enable();
        SoundMannager.Instance.Inicio();
        currentSong = -1;
    }

    public void IniciarJuego()
    {
        playerUI.SetActive(true);
        playerGO.SetActive(true);
        Camera.main.orthographicSize = 18.45f;
        playerInput.actions.FindActionMap("UI").Disable();
        playerInput.actions.FindActionMap("Player").Enable();
        player.position = lastChekPoint.position;
        
        NextSong();
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

        if (!currentLevel.GetLevelStatus())
        {
            player.position = currentLevel.startPos.position;
            currentLevel.ResetLevel();
        }
        else
        {
            player.position = currentLevel.levelTarget.position;
        }

        playerMovement.ResetPlayer();
        playerLives.ResetLives();
        player.gameObject.SetActive(true);
    }

    private int currentSong;
    public void NextSong()
    {
        currentSong++;
        if (currentSong >= musica.Count)
            currentSong = 0;
        SoundMannager.Instance.ReproducirSiguienteCancion(musica[currentSong]);
    }
}
