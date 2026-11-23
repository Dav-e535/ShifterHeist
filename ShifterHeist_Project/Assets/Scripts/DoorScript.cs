using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public Sprite openDoor, closedDoor;
    private SpriteRenderer sr;
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
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
        
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.tag != "Player" || GetComponent<BoxCollider2D>().isTrigger == true)
        {
            sr.sprite = closedDoor;
        }
    }

}
