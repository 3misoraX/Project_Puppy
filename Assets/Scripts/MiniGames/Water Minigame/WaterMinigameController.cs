using UnityEngine;
using UnityEngine.InputSystem;

public class WaterMinigameController : MonoBehaviour
{
    public float movementSpeed;
    public Transform leftLimit;
    public Transform rightLimit;

    [SerializeField] private float movementInput;

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.right * movementInput * movementSpeed * Time.deltaTime;

        float clampedX = Mathf.Clamp(transform.position.x, leftLimit.position.x, rightLimit.position.x);

        transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);
    }

    public void OnWaterMinigame(InputValue input)
    {
        movementInput = input.Get<float>();
    }
}
