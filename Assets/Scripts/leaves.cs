using UnityEngine;
using UnityEngine.InputSystem;


public class leaves : MonoBehaviour
{
    public float speed = 10f;
    public Vector2 input; 
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (input==new Vector2(0, 1))
        {
            transform.position = Vector2.MoveTowards(transform.position, input*100, speed* Time.deltaTime);
        }
    } 
    void OnMove(InputValue value)
    {
        input = value.Get<Vector2>();
    }
}
