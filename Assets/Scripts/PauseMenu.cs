using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    public InputActionReference pauseAction;
    public GameObject pausePanel;

    void OnEnable()
    {
        pauseAction.action.started += TogglePause;
        pauseAction.asset.Enable();
        pausePanel.SetActive(false);
    }
    void OnDisable()
    {
        pauseAction.action.started -= TogglePause;
    }
    private void TogglePause(InputAction.CallbackContext context)
    {
        TogglePause();
    }
    public void TogglePause()
    {
        pausePanel.SetActive(!pausePanel.activeSelf);
        Time.timeScale = pausePanel.activeSelf? 0 : 1;
        Cursor.lockState = pausePanel.activeSelf? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = pausePanel.activeSelf;
    }
}
