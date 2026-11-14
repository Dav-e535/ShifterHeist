using UnityEngine;

public class ShiftOption : MonoBehaviour
{
    public PlayerController playerController;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            bool isAlreadyInList = false;
            foreach (GameObject shiftObject in playerController.shiftList)
            {
                if (shiftObject== gameObject)
                {
                    isAlreadyInList = true;
                    break;
                }
            }
            if(!isAlreadyInList)
            {
                System.Array.Resize(ref playerController.shiftList, playerController.shiftList.Length + 1);
                playerController.shiftList[playerController.shiftList.Length-1] = gameObject;
                playerController.ShiftOptionUI(gameObject);
            }
            
            
            
            
        }
    }
    
}
