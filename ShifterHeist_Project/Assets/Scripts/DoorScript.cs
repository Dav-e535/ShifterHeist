using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public Sprite openDoor, closedDoor;
    public bool isQuestDoor;
    public GameObject popupMessage;
    private SpriteRenderer sr;
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = closedDoor;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag != "Player" || GetComponent<BoxCollider2D>().isTrigger == true)
        {
            sr.sprite = openDoor;
        }
        if (isQuestDoor&&transform.childCount==0&&collision.gameObject.tag == "Player")
        {
            Instantiate(popupMessage,popupMessage.transform.position,popupMessage.transform.rotation,transform);
        }
        
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.tag != "Player" || GetComponent<BoxCollider2D>().isTrigger == true)
        {
            sr.sprite = closedDoor;
        }
    }
    public void OpenDoor()
    {
        sr.sprite = openDoor;
    }
    public void CloseDoor()
    {
        sr.sprite = closedDoor;
    }

}
