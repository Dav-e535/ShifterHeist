using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool keycardGet = false;
    public GameObject firstDoor;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(keycardGet == true)
        {
            firstDoor.GetComponent<BoxCollider2D>().isTrigger = true;
        }
    }
    public void ActivateProgressPoint(GameObject gObject)
    {
        if(gObject.GetComponent<Usable>().inCodeTag == "KeyCard")
        {
            keycardGet = true;
        }
    }
}
