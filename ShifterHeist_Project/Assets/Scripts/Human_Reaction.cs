using System.Collections;
using UnityEngine;

public class Human_Reaction : MonoBehaviour
{
    public GameObject suspiciousIcon;
    public GameObject noticedIcon;
    public GameObject player;
    public PlayerController playerController;
    public bool suspectsShifter = false, noticedShifter=false, stopchecking=false;
    public float topbound = 0.5f, bottombound = -0.5f, leftbound = -0.5f, rightbound = 0.5f, speed = 0.5f;
    void Start()
    {
        player = GameObject.Find("Player");
        playerController = player.GetComponent<PlayerController>();
    }
    void Update()
    {
        if (suspectsShifter && !noticedShifter)
        {
            if (transform.childCount > 0)
            {
                Destroy(transform.GetChild(0).gameObject);
            }
            Instantiate(suspiciousIcon,transform.position+new Vector3(0,0.2f,0),suspiciousIcon.transform.rotation,transform);
        }
        if (noticedShifter&&!stopchecking)
        {
            if (transform.childCount > 0)
            {
                Destroy(transform.GetChild(0).gameObject);
            }
            Instantiate(noticedIcon, transform.position+new Vector3(0,0.2f,0), noticedIcon.transform.rotation, transform);
            stopchecking = true;
        }
        if (noticedShifter)
        {
            transform.Translate((transform.position-player.transform.position).normalized*Time.deltaTime*speed);
        }
        Vector3 playerPos = player.transform.position;
        if (playerPos.x > gameObject.transform.position.x + leftbound && playerPos.x < gameObject.transform.position.x + rightbound && playerPos.y > gameObject.transform.position.y + bottombound && playerPos.y < gameObject.transform.position.y + topbound)
        {
           suspectsShifter = true;
            if (playerController.shiftedTag == "Human")
            {
                
                StartCoroutine(NoticingHuman(3f,playerPos));
            }
            if (playerController.shiftedTag == "Object" && playerController.moveInput != new Vector2(0, 0))
            {
                StartCoroutine(NoticingObject(0.2f, playerPos));
            }
            if (playerController.shiftedTag == "Shifter")
            {
                noticedShifter = true;
            }
        }
        else
        {
            suspectsShifter = false;
        }
        
        
    }
    public IEnumerator NoticingHuman(float seconds, Vector3 playerPos)
    {
        yield return new WaitForSeconds(seconds);
        if (playerPos.x > gameObject.transform.position.x + leftbound && playerPos.x < gameObject.transform.position.x + rightbound && playerPos.y > gameObject.transform.position.y + bottombound && playerPos.y < gameObject.transform.position.y + topbound)
        {
            noticedShifter = true;
        }
        
    }
    public IEnumerator NoticingObject(float seconds, Vector3 playerPos)
    {
        yield return new WaitForSeconds(seconds);
        if(playerController.moveInput != new Vector2(0, 0)&&playerPos.x > gameObject.transform.position.x + leftbound && playerPos.x < gameObject.transform.position.x + rightbound && playerPos.y > gameObject.transform.position.y + bottombound && playerPos.y < gameObject.transform.position.y + topbound)
        {
            noticedShifter = true;
        }
    }
}
