using UnityEngine;
using UnityEngine.InputSystem;

/*
 *	Clase que implementa el sistema para interactuar con objetos interactuables.
 *	El objeto que contenga el script debe tener un collider como trigger para detectar
 *	los objetos interactuables. Los objetos interactuables deben implementar la interfaz IInteractable
 *	y deben estar en la layerMask definida para poder detectarse. El comportamiento de cada interactuable
 *	se define en la propia implementación de la interfaz. Idealmente este script irá en el GameObject del
 *	personaje jugable si lo hay.
 */

public class InteractionHandler : MonoBehaviour
{
    [SerializeField] private LayerMask interactableLayer;   //Capa donde deberán encontarse los elementos interactuables
    private IInteractable currentInteractable;              //Objeto con el que se quiere interactuar (puede ser nulo, es decir, no hay)
    //private IPickable carriedItem;


    //Detección de interactuables
    private void OnTriggerEnter2D(Collider2D other)
    {
        //Si el objeto colisionado no está en la capa de interacción se omite
        if (((1 << other.gameObject.layer) & interactableLayer) == 0) return;

        //Si el objeto colisionado está en la capa pero no tiene el componente IInteractable, se omite
        if (!other.TryGetComponent<IInteractable>(out var interactable)) return;

        // Si llevo algo → solo permito interactuar con ese mismo
        //if (carriedItem != null)
        //{
        //    if (interactable == carriedItem)
        //    {
        //        currentInteractable = carriedItem;
        //        currentInteractable.GetPrompt();
        //    }
        //    return;
        //}

        // Si no hay ningún interactuable registrado o el nuevo tiene prioridad igual o mayor
        if (currentInteractable == null || interactable.prioridad >= currentInteractable.prioridad)
        {
            currentInteractable = interactable;
            currentInteractable.GetPrompt();
        }
    }

    //Limpiar interactuables fuera de alcance
    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.TryGetComponent<IInteractable>(out var interactable)) return;

        if (interactable == currentInteractable)
        {
            currentInteractable = null; // Si quieres, aquí podrías buscar otro en la zona
        }
    }

    //Interacción con el objeto
    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            currentInteractable?.Interact(gameObject);
            //if (carriedItem != null)
            //{
            //    carriedItem.Interact(gameObject); // Decide si hace Drop
            //    if (!carriedItem.IsBeingCarried)
            //    {
            //        carriedItem = null; // Se soltó
            //        currentInteractable = null;
            //    }
            //}
            //else
            //{
            //    currentInteractable?.Interact(gameObject);
            //    if (currentInteractable is IPickable pickable && pickable.IsBeingCarried)
            //        carriedItem = pickable; // Ahora llevas algo
            //}
        }
    }

    public void ForzarInteraccion()
    {
        currentInteractable?.Interact(gameObject);
        //if (carriedItem != null)
        //{
        //    carriedItem.Interact(gameObject); // Decide si hace Drop
        //    if (!carriedItem.IsBeingCarried)
        //    {
        //        carriedItem = null; // Se soltó
        //        currentInteractable = null;
        //    }
        //}
        //else
        //{
        //    currentInteractable?.Interact(gameObject);
        //    if (currentInteractable is IPickable pickable && pickable.IsBeingCarried)
        //        carriedItem = pickable; // Ahora llevas algo
        //}
    }
}
