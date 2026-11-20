using UnityEngine;

public class ElevatorScript : MonoBehaviour
{
    public GameObject[] humanPrefabs;
    public bool spawnedAlready;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spawnedAlready = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EventsTrigger")&&spawnedAlready==false)
        {
            SpawnHumans();
            spawnedAlready = true;
        }
    }
    public void SpawnHumans()
    {
        foreach(GameObject prefab in humanPrefabs)
        {
            Instantiate(prefab);
        }
    }
}
