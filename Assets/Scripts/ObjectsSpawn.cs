using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class ObjectsSpawn : MonoBehaviour
{
    [Header("Spawn Settings")]
    [SerializeField] private GameObject[] ObjectsList;
    [Space(10)]
    [SerializeField] private Transform MaxPosition;
    [SerializeField] private Transform MinPosition;
    private Transform cloneTransform;
    //This will store which medal was selected to spawn
    GameObject objectToSpawn;
    [SerializeField, Range(0f, -5f), Tooltip("Esto determina la distancia que tendran los objetos de los tubos")] private float distance;
    private Vector2 distanceBetweenPrefabs;
    //This will reference the current coin spawn to change its properties
    private GameObject objectClone;

    private void OnEnable()
    {
        TubesSpawn.OnTubeSpawn += SpawnTube;
    }

    private void OnDisable()
    {
        TubesSpawn.OnTubeSpawn -= SpawnTube;
    }

    private void SpawnTube(GameObject tube)
    {
        distanceBetweenPrefabs = new Vector2(distance + tube.transform.position.x, Mathf.Lerp(MinPosition.position.x + 1f, MaxPosition.position.x - 1f, Random.Range(0.0f, 1.0f)));
        int probabiltyToSpawn = Random.Range(0, 3);;
        int RandomObject = Random.Range(0, ObjectsList.Length);
        objectToSpawn = ObjectsList[RandomObject];
        if (probabiltyToSpawn == 2)
        {
            objectClone = Instantiate(objectToSpawn,distanceBetweenPrefabs, Quaternion.identity);
            objectClone.GetComponent<Rigidbody2D>().linearVelocityX = -2f;
        }
    }
    
}
