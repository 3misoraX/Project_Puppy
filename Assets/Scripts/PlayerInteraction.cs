using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

public class PlayerInteraction : MonoBehaviour
{
    private IInteractable currentInteractable;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IInteractable interactable))
        {
            currentInteractable = interactable;
            currentInteractable.ShowPrompt(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out IInteractable interactable))
        {
            interactable.ShowPrompt(false);
            currentInteractable = null;
        }
    }
    
    public void OnInteract()
    {
        currentInteractable?.Interact();
    }
}
