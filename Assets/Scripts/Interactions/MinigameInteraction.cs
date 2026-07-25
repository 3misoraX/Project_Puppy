
using UnityEngine;
using UnityEngine.InputSystem;

public class MinigameInteraction : MonoBehaviour, IInteractable
{
    public GameObject prompt;
    public GameObject minigame;
    public InputActionReference moveInput;
    public InputActionReference minigameInput;

    void Awake()
    {
        minigame.SetActive(false);
    }
    public void Interact()
    {
        moveInput.asset.Disable();
        minigameInput.asset.Enable();
        minigame.SetActive(true);
    }

    public void EndIteraction()
    {
        moveInput.asset.Enable();
        minigameInput.asset.Disable();
        minigame.SetActive(false);
    }

    public void ShowPrompt(bool show)
    {
        prompt.SetActive(show);
    }
}