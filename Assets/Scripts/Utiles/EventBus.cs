using System;
using System.Collections.Generic;
using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class EventBus : MonoBehaviour
{
    /*Para suscribirse y dessuscribirse de los eventos:
     * 
     * private void OnEnable()
        {
            EventBus.NombreEvento += FuncionCallback;
        }

        private void OnDisable()
        {
            EventBus.Evento -= FuncionCallback;
        }
    
        En el cuerpo del objeto a suscribir
     */

    //--------Declaración de eventos--------//

    //Con parámetros (puede ser cualquier otra cosa que GameObject)
    public static event Action<string> OnCambiarActionMap;
    public static event Action OnMuerteJugador;
    public static event Action<CinemachineCamera> OnCambiarCamZone;

    //----------Funciones para invocar los eventos----------//
    //Se llama a estas funciones, no a los eventos directamente,
    //pero las subscripciones si que se hacen al propio evento 

    //Llamadas a eventos con parámetros (puede ser cualquier otra cosa que GameObject)
    public static void CambiarActionMap(string newMap) => OnCambiarActionMap?.Invoke(newMap);
    public static void MuerteJugador() => OnMuerteJugador?.Invoke();
    public static void ActivarCamZone(CinemachineCamera cam) => OnCambiarCamZone?.Invoke(cam);
}
