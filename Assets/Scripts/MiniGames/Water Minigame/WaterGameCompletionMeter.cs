using System;
using UnityEngine;
using UnityEngine.UI;

public class WaterGameCompletionMeter : MonoBehaviour
{
    public float depletionRate;
    public Image progressBar;
    [Range(0f,1f)]
    public float completion;

    private void Start()
    {
        completion = 0;
    }

    private void Update()
    {
        completion -= depletionRate * Time.deltaTime;
        progressBar.fillAmount = completion;
        if (completion >= 1f)
        {
            // Poner aqui la lógica de cuando termine el juego
            // regresar al jugador, etc.
            Debug.Log("complete");
        }
    }
}
