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
    private IInteractable curInteractable;
    private float _lastTime = 0f;
    float _maxDelayTime = 0.5f;

    private void Update()
    {
        _lastTime += Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Interactable"))
        {
            if (curInteractable != null) return;
            curInteractable = collision.GetComponent<IInteractable>();
            curInteractable.SetText(true);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Interactable"))
        {
            if (curInteractable != null) return;
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
        if (_lastTime < _maxDelayTime) return;
        if (callbackContext.phase == InputActionPhase.Started && curInteractable != null)
        {
            curInteractable.OnInteract(true);
            _lastTime = 0f;
        }
    }

}
