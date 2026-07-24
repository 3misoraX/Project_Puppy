using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Video;

public class SpawnRescue : MonoBehaviour
{

    //Visual debug
    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(Vector3.zero,minRadius);
        Gizmos.DrawWireSphere(Vector3.zero,maxRadius);
    }

    void Update()
    {
        if (Mouse.current.rightButton.wasPressedThisFrame)
        {
            GenerateSpawns();
        }
    }

    [Header("Spawn Settings")]
    public float minRadius;
    public float maxRadius;
    public int totalSpawns;
    public GameObject spawnPrefab;
    List<GameObject> spawned = new List<GameObject>();


    void GenerateSpawns()
    {
        int i = 0;
        float beforeLastAngle = 0;
        float lastAngle = 0;
        float currentAngle = 0;
        float angleStep = 360.0f/ totalSpawns;
        float radiusStep = (maxRadius - minRadius)/ totalSpawns;
        Vector3 collitionPoint;
        ISpawner spawner;
        
        while(0 < spawned.Count)
        {
            Destroy(spawned[0]);
            spawned.RemoveAt(0);
        }

        do
        {
            beforeLastAngle = Random.Range(0,360);  
        }while(!TryFindSpawnPoint(GeneratePointInRadius(minRadius, minRadius + radiusStep,beforeLastAngle), out collitionPoint, out spawner));
        spawned.Add(spawner.Spawn(collitionPoint));
        i++;
        
        do
        {
            do
            {
                lastAngle = Random.Range(0,360);
            }while(Mathf.Abs(beforeLastAngle - lastAngle) < angleStep);
        }while(!TryFindSpawnPoint(GeneratePointInRadius(minRadius + radiusStep, minRadius + (2*radiusStep),lastAngle), out collitionPoint, out spawner));
        spawned.Add(spawner.Spawn(collitionPoint));
        i++;

        while (i < totalSpawns)
        {
            currentAngle = GetAngle(beforeLastAngle, lastAngle, angleStep);
            while(!TryFindSpawnPoint(GeneratePointInRadius(minRadius + (radiusStep * i), minRadius + (radiusStep * (i+1)),currentAngle), out collitionPoint, out spawner)){}
            spawned.Add(spawner.Spawn(collitionPoint));
            beforeLastAngle = lastAngle;
            lastAngle = currentAngle;
            i++;
        }
    }

    public Vector2 GeneratePointInRadius(float minRadius, float maxRadius, float angle)
    {
        //Check valid values
        if(minRadius<=0){return default;}
        if(maxRadius<minRadius){return default;}
        float r = Random.Range(minRadius,maxRadius);
        Debug.Log("Radio " + r);
        float x = r * Mathf.Cos(angle);
        float z = r * Mathf.Sin(angle);
        
        return new Vector3(x, z);
    }

    public float GetAngle(float x1, float x2, float step)
    {
        List<float> x = new List<float>();
        if(x1 < x2)
        {
            x.Add(x1 - step);
            x.Add(x1 + step);
            x.Add(x2 - step);
            x.Add(x2 + step);
        }
        else
        {
            x.Add(x2 - step);
            x.Add(x2 + step);
            x.Add(x1 - step);
            x.Add(x1 + step);
        }

        int random = Random.Range(0,1);
        float angle;

        if(random == 0)
        {
            angle = Random.Range(x[1],x[2]);
        }
        else
        {
            angle = Random.Range(x[3],x[0] + 360);
        }

        return angle % 360;
    }


    [Header("Spawn Check")]
    public float checkStartY = 0;
    public float checkMaxDistance = 1.0f;
    public bool TryFindSpawnPoint(Vector2 startPoint, out Vector3 collitionPoint, out ISpawner spawn)
    {
        collitionPoint = Vector3.zero;
        spawn = null;
        Vector3 startPosition = new Vector3(startPoint.x, checkStartY, startPoint.y);
        Ray ray = new Ray(startPosition,Vector3.down);
        Physics.SphereCast(ray, 1, out RaycastHit hitInfo, checkMaxDistance);
        
            //No collition
            if(hitInfo.collider == null){return false;}

            //Did collide in a spawn zone
            if(hitInfo.collider.gameObject.TryGetComponent<ISpawner>(out spawn))
            {
                Debug.DrawLine(startPosition,hitInfo.point,Color.green,5f);
                collitionPoint = hitInfo.point;
                return true;
            }
            //Did NOT collide in a spawn zone
            else
            {
                Vector3 endPosition = startPosition;
                endPosition.y -= checkMaxDistance;
                Debug.DrawLine(startPosition,endPosition,Color.red,1.5f);
                Debug.Log("No hit point");
                return false;
            }
    }
}
