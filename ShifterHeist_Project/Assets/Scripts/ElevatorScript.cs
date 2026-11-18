using UnityEngine;

public class ElevatorScript : MonoBehaviour
{
    public GameObject[] humanPrefabs;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerTrigger"))
        {
            SpawnHumans();
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
