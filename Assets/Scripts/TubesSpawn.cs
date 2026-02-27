    using System;
    using UnityEngine;
    using Random = UnityEngine.Random;

    public class TubesSpawn : MonoBehaviour
{
    public static Action<GameObject> OnTubeSpawn;
    [SerializeField] private GameObject prefab;
    [SerializeField] private Transform pointOfSpawn, maxPosition, minPosition;
    [SerializeField] private float tubeSpeed = 1;
    [SerializeField] private float timePerTube = 2f;
    private GameObject clone;

    private float randomNumber;

    private Vector2 tubesposition;
    private float timer = 0;

    private void Start()
    {
        clone = Instantiate(prefab, pointOfSpawn.transform.position, transform.rotation);
        clone.GetComponent<Rigidbody2D>().AddForce(Vector2.left * tubeSpeed * 5f);
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= timePerTube)
        {
            randomNumber = Random.Range(0.0f, 1.0f);
            tubesposition = new Vector2(pointOfSpawn.transform.position.x, Mathf.Lerp(maxPosition.position.y, minPosition.position.y, randomNumber));
            clone = Instantiate(prefab, tubesposition, transform.rotation);
            clone.GetComponent<Rigidbody2D>().AddForce(Vector2.left * tubeSpeed * 5f);
            OnTubeSpawn?.Invoke(clone);
            timer = 0;
        }
    }
}
