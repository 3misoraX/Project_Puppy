using System;
using Unity.VisualScripting;
using UnityEngine;

public class WaterCollision : MonoBehaviour
{
    public float completionRate;
    public WaterGameCompletionMeter completionMeter;
    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out TargetMovement target))
        {
            //Debug.Log("Choca con target");
            completionMeter.completion += completionRate * Time.deltaTime;
        }
    }
}
