using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;
using UnityEngine.InputSystem;

//NombreDeLaAccion.accion.triggered = true (para botones)
//NombreDeLaAccion.accion.ReadValue<TipoDeDato>
public class RCP : MonoBehaviour
{
    public float timer = 5f;
    public float Max_time = 30f;
    public float beat_duration = 0.5f;
    public float epsilon = 0.1f;
    public TMP_Text texto;
    public Image heart;
    public Image heart_ritmo;
    private bool good;
    public int Max_good = 10;
    public int good_count = 0;
    public Image feel_bar;
    public GameObject game_Panel;
    void OnEnable()
    {
        good_count = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        texto.text = Convert.ToString(Convert.ToInt32(timer));
        float passed = Max_time - timer;
        float beat = passed % beat_duration;
        if (beat >= beat_duration - epsilon && beat <= beat_duration)
        {
            heart.color = Color.red;
            good = true;
        }
        else if (beat >= 0 && beat <= epsilon)
        {
            heart_ritmo.enabled = true;
            heart.enabled = false;
        }
        else
        {
            heart.enabled = true;
            heart_ritmo.enabled = false;
            good = false;
        }



        if (timer <= 0)
            {
                Debug.Log("y tu tiempo se acabo");
                timer = 30f;
            }
    }

    public MinigameInteraction minigameInteraction;
    void OnJump(InputValue value)
    {
        if (good == true)
        {
            Debug.Log("good");
            good_count++;
            feel_bar.fillAmount = (float)good_count / (float)Max_good;
            if (good_count == Max_good)
            {
                game_Panel.SetActive(false);
                minigameInteraction.EndIteraction();
            }
        }
        else
        {
            Debug.Log("hmm estas mal");
        }
    }
}
