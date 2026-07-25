using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Leaf : MonoBehaviour
{

    public Vector2 playerInput;
    public InputActionReference up;
    public InputActionReference down;
    public InputActionReference left;
    public InputActionReference right;
    public Direccion direccion;
    
    RectTransform rectTransform;
    public float speed = 100f;

    private void Awake()
    {        
        up.asset.Enable();
        down.asset.Enable();
        left.asset.Enable();
        right.asset.Enable();
        rectTransform = GetComponent<RectTransform>();
    }

    private void OnEnable()
    {

    }

    private void Start()
    {
        
    }
    LeafManager my_Manager;
    public void registrar_Manager(LeafManager leaf_Manager)
    {
        my_Manager = leaf_Manager;
    } 
    public bool turno = false;
    bool moviendo = false;
    private void Update()
    {
        if (!turno)
        {
            return;
        }
        if (moviendo)
        {
            return; 
        }
        if (right.action.WasPressedThisDynamicUpdate() && direccion == Direccion.Derecha)
            StartCoroutine(desaparacer(Vector3.right));
        //rectTransform.position += Vector3.right * speed * Time.deltaTime;
        if (left.action.WasPressedThisDynamicUpdate() && direccion == Direccion.Izquierda)
            StartCoroutine(desaparacer(Vector3.left));
        if (down.action.WasPressedThisDynamicUpdate() && direccion == Direccion.Abajo)
            StartCoroutine(desaparacer(Vector3.down));
        if (up.action.WasPressedThisDynamicUpdate()  && direccion == Direccion.Arriba)
            StartCoroutine(desaparacer(Vector3.up));
    }

    IEnumerator desaparacer(Vector3 direcion)
    {
        moviendo = true;
        turno = false;
        my_Manager.SiguienteHoja(this);
        float tiempo = 0;
        while (tiempo < 2)
        {
            yield return new WaitForEndOfFrame();
            tiempo += Time.deltaTime;
            rectTransform.position += direcion * speed * Time.deltaTime;
        }
        
        Destroy(gameObject);
    }
    public enum Direccion
    {
        Arriba,
        Abajo,
        Izquierda,
        Derecha
    }

}