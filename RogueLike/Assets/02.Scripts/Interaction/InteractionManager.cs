using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public interface IInteractable
{
    void SetText(bool active);

    void OnInteract(bool active);
}

public class InteractionManager : MonoBehaviour
{
    public float maxCheckDistance;
    private IInteractable curInteractable;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Interactable"))
        {
            curInteractable = collision.GetComponent<IInteractable>();
            curInteractable.SetText(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Interactable"))
        {
            curInteractable.SetText(false);
            curInteractable.OnInteract(false);
            curInteractable = null;
        }
    }

    public void OnInteractInput(InputAction.CallbackContext callbackContext)
    {
        if(callbackContext.phase == InputActionPhase.Started && curInteractable != null)
        {
            curInteractable.OnInteract(true);
        }
    }

}
