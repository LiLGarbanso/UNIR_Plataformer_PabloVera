using UnityEngine;
/*
 * Interfaz que define un elemento con el que se puede interactuar
 */
public abstract class IInteractable : MonoBehaviour
{
    public abstract void Interact(GameObject interactor);	//Reacción del objeto al interactuar
    public abstract string GetPrompt(); //Por si queremos mostrar algún tipo de mensaje antes de interactuar
    public int prioridad;

    private void OnDisable()
    {
        //EventBus.MostrarMensajeUI("");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            GetPrompt();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //if (collision.CompareTag("Player"))
        //    EventBus.MostrarMensajeUI("");
    }
}