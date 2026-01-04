using UnityEngine;

public class GameMannager : MonoBehaviour
{
    public Transform player, startPoint;

    private void Start()
    {
        player.position = startPoint.position;
        SoundMannager.Instance.Inicio();
    }
}
