using System;
using UnityEngine;

public class Interactable : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject prompt;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        prompt.SetActive(false);
    }

    public void Interact()
    {
        //Debug.Log("Interact");
        GetComponent<Renderer>().material.color = Color.green;
    }

    public void ShowPrompt(bool show)
    {
        //Debug.Log("Can Interact");
        prompt.SetActive(show);
        
    }
}
