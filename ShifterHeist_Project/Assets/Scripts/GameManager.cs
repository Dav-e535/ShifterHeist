using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool keycardGet = false, serverPuzzleSolved = false;
    public GameObject firstDoor, seconddoor, screen;
    public GameObject[] servers; 
    public int serverorder = 0;
    public int[] theOrder;
    public bool everythingCorrectServer;
    public Sprite screenEmpty, screenLocked, screenUnlocked;
    void Start()
    {
        everythingCorrectServer = true;
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
    public void ServerActivated(GameObject serv)
    {
        serverorder++;
        System.Array.Resize(ref servers, servers.Length + 1);
        servers[serverorder - 1] = serv;
        if (servers[serverorder - 1].GetComponent<ServerScript>().servernumber != theOrder[serverorder - 1])
        {
            everythingCorrectServer = false;
        }
        if(serverorder == 6 && everythingCorrectServer)
        {
            serverPuzzleSolved = true;
            seconddoor.GetComponent<BoxCollider2D>().isTrigger = true;
            screen.GetComponent<SpriteRenderer>().sprite = screenUnlocked;
        }
        else if(serverorder == 6)
        {
            foreach(GameObject server in servers)
            {
                server.GetComponent<ServerScript>().isOn = false;
            }
            System.Array.Resize(ref servers, 0);
            serverorder = 0;
            everythingCorrectServer = true;
        }
    }
}
