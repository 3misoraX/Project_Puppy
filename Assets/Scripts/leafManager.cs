using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;


public class LeafManager : MonoBehaviour
{
    [Header("Referencias")]
    public Leaf hojaPrefab;
    public Transform leafSpawn;
    public TextMeshProUGUI indicacionPantalla;
    public MinigameInteraction minigameInteraction;

    [Header("Configuraci�n")]
    public int cantidadHojas = 10;
    public float separacion = 10f;

    public GameObject[] hojas;
    public int count;
    public List<Leaf> hojas_Activas;
    public Transform panel;

    public float speed = 10f;
    RectTransform rectTransform;

    public Vector2 playerInput;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }
    public void OnSticksMinigames(InputValue input)
    {
        playerInput = input.Get<Vector2>();
          
    }
    void Start()
    {
        CrearHojas();
       
    }

    void CrearHojas()
    {
        for (int i = 0; i < count; i++)
        {
            GameObject HojaActual = Instantiate(hojas[Random.Range(0, hojas.Length)], panel);
            Leaf leaf = HojaActual.GetComponent<Leaf>();
            leaf.registrar_Manager(this);
            hojas_Activas.Add(leaf);
        }
        hojas_Activas[0].turno = true;
    }

    public void SiguienteHoja(Leaf current)
    {
        hojas_Activas.Remove(current);
        Debug.Log(hojas_Activas.Count);
        if(hojas_Activas.Count == 0)
        {
            gameObject.SetActive(false);
            minigameInteraction.EndIteraction();
        }
        else
        {
            hojas_Activas[0].turno = true;
        }
    }
}

