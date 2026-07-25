using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseClick : MonoBehaviour
{
    public static MouseClick Instance;
    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this);
        }
    }
    public InputActionReference click;
    public Texture2D clickedCursor;

    void OnEnable()
    {
        click.action.started += StartClick;
        click.action.canceled += EndClick;
        click.asset.Enable();
    }

    void OnDisable()
    {
        click.action.started -= StartClick;
        click.action.canceled -= EndClick;
        click.asset.Enable();
    }
    private void StartClick(InputAction.CallbackContext context)
    {
        Cursor.SetCursor(clickedCursor,Vector2.zero,CursorMode.Auto);
    }
    private void EndClick(InputAction.CallbackContext context)
    {
        Cursor.SetCursor(null,Vector2.zero,CursorMode.Auto);
    }
}
