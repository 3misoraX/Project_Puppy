using UnityEngine;

public class TargetMovement : MonoBehaviour
{
    public float movementSpeed = 3f;
    public Transform leftLimit;
    public Transform rightLimit;
    
    private int direction = 1;

    void Update()
    {
        transform.position += Vector3.right * direction * movementSpeed * Time.deltaTime;
        
        if (transform.position.x >= rightLimit.position.x)
        {
            direction = -1;
        }
        
        if (transform.position.x <= leftLimit.position.x)
        {
            direction = 1;
        }
    }
}