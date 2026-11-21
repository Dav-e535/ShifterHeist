using System.Collections;
using UnityEngine;

public class ElevatorScript : MonoBehaviour
{
    public GameObject[] humanPrefabs;
    private Animator elevatorAnimatior;
    public bool spawnedAlready;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spawnedAlready = false;
        elevatorAnimatior = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EventsTrigger")&&spawnedAlready==false)
        {
            StartCoroutine(OpeningTime());
            spawnedAlready = true;
        }
    }
    public float elevatorSpawnTime=0.6f;
    IEnumerator OpeningTime()
    {
        elevatorAnimatior.SetTrigger("OpenElevator");
        yield return new WaitForSeconds(elevatorSpawnTime);
        SpawnHumans();
    }
    public void SpawnHumans()
    {
        foreach(GameObject prefab in humanPrefabs)
        {
            Instantiate(prefab);
        }
    }
}
