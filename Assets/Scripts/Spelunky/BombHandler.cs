using System.Collections.Generic;
using UnityEngine;

public class BombHandler : MonoBehaviour
{
    private List<GameObject> bombasActivas = new List<GameObject>();
    public GameObject prefabBomba;
    public Transform escenario, dropPoint;
    public AudioClip clocarBomba;

    private void OnEnable()
    {
        EventBus.OnMuerteJugador += LimpiarBombas;
        EventBus.OnStartLevel -= LimpiarBombas;
    }

    private void OnDisable()
    {
        EventBus.OnMuerteJugador -= LimpiarBombas;
        EventBus.OnStartLevel -= LimpiarBombas;
    }

    public void ColocarBomba()
    {
        SoundMannager.Instance.PlaySFX(clocarBomba, 0.5f);
        bombasActivas.Add(Instantiate(prefabBomba, dropPoint.position, Quaternion.identity, escenario));
    }

    public void LimpiarBombas()
    {
        foreach (GameObject go in bombasActivas)
        {
            Destroy(go);
        }
        bombasActivas.Clear();
    }
    public void LimpiarBombas(Level l)
    {
        foreach (GameObject go in bombasActivas)
        {
            Destroy(go);
        }
        bombasActivas.Clear();
    }
}
