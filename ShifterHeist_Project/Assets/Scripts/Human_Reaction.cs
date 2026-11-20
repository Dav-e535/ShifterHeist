using System.Collections;
using UnityEngine;

public class Human_Reaction : MonoBehaviour
{
    public GameObject suspiciousIcon;
    public GameObject noticedIcon;
    public GameObject player;
    public PlayerController playerController;
    public bool suspectsShifter = false, noticedShifter=false, stopchecking=false, cannoticeshifter = false;
    public float topbound = 0.5f, bottombound = -0.5f, leftbound = -0.5f, rightbound = 0.5f, speed = 0.5f;
    public bool alive=true;
    public float startNoticeDelay, shifterNoticeDelay, humanNoticeDelay, objectNoticeDelay;
    void Start()
    {
        cannoticeshifter = false;
        alive = true;
        player = GameObject.Find("Player");
        playerController = player.GetComponent<PlayerController>();
        StartCoroutine(canNoticeWait());
    }
    void Update()
    {
        Vector3 playerPos = player.transform.position;
        if(playerPos.x > gameObject.transform.position.x + -0.5 && playerPos.x < gameObject.transform.position.x + 0.5 && playerPos.y > gameObject.transform.position.y + -0.5 && playerPos.y < gameObject.transform.position.y + 0.5)
        {
            if (playerController.attack.triggered)
            {
                alive = false;
                transform.rotation = new Quaternion(0,0,90,0);
            }
        }
        if (alive == false)
        {
            if (transform.childCount > 0)
            {
                if (transform.GetChild(0).gameObject.CompareTag("SelectionUI")&&transform.childCount > 1)
            {
                Destroy(transform.GetChild(1).gameObject);
            }
            else if(transform.GetChild(0).gameObject.CompareTag("SelectionUI")==false)
            {
                Destroy(transform.GetChild(0).gameObject);
            }
            }
            
        }
        if (!suspectsShifter && !noticedShifter)
        {
            if (transform.childCount > 0)
            {
                if (transform.GetChild(0).gameObject.CompareTag("SelectionUI")&&transform.childCount > 1)
            {
                Destroy(transform.GetChild(1).gameObject);
            }
            else if(transform.GetChild(0).gameObject.CompareTag("SelectionUI")==false)
            {
                Destroy(transform.GetChild(0).gameObject);
            }
            }
        }
        if (suspectsShifter && !noticedShifter&&alive)
        {
            if (transform.childCount > 0)
            {
                if (transform.GetChild(0).gameObject.CompareTag("SelectionUI")&&transform.childCount > 1)
            {
                Destroy(transform.GetChild(1).gameObject);
            }
            else if(transform.GetChild(0).gameObject.CompareTag("SelectionUI")==false)
            {
                Destroy(transform.GetChild(0).gameObject);
            }
            }
            Instantiate(suspiciousIcon,transform.position+new Vector3(0,0.2f,0),suspiciousIcon.transform.rotation,transform);
        }
        if (noticedShifter&&!stopchecking&&alive)
        {
            if (transform.childCount > 0)
            {
                if (transform.GetChild(0).gameObject.CompareTag("SelectionUI")&&transform.childCount > 1)
            {
                Destroy(transform.GetChild(1).gameObject);
            }
            else if(transform.GetChild(0).gameObject.CompareTag("SelectionUI")==false)
            {
                Destroy(transform.GetChild(0).gameObject);
            }
            }
            Instantiate(noticedIcon, transform.position+new Vector3(0,0.2f,0), noticedIcon.transform.rotation, transform);
            stopchecking = true;
        }
        if (noticedShifter&&alive)
        {
            transform.Translate((transform.position-player.transform.position).normalized*Time.deltaTime*speed);
        }
        
        if (alive&&playerPos.x > gameObject.transform.position.x + leftbound && playerPos.x < gameObject.transform.position.x + rightbound && playerPos.y > gameObject.transform.position.y + bottombound && playerPos.y < gameObject.transform.position.y + topbound)
        {
           suspectsShifter = true;
            if (playerController.shiftedTag == "Human")
            {
                
                StartCoroutine(NoticingHuman(humanNoticeDelay,playerPos));
            }
            if (playerController.shiftedTag == "Object" && playerController.moveInput != new Vector2(0, 0))
            {
                StartCoroutine(NoticingObject(objectNoticeDelay, playerPos));
            }
            if (playerController.shiftedTag == "Shifter")
            {
                noticedShifter = true;
            }
        }
        else if(alive)
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
        else
        {
            if (transform.childCount > 0)
            {
                if (transform.GetChild(0).gameObject.CompareTag("SelectionUI")&&transform.childCount > 1)
            {
                Destroy(transform.GetChild(1).gameObject);
            }
            else if(transform.GetChild(0).gameObject.CompareTag("SelectionUI")==false)
            {
                Destroy(transform.GetChild(0).gameObject);
            }
            }
            suspectsShifter = false;
        }
        
    }
    public IEnumerator NoticingObject(float seconds, Vector3 playerPos)
    {
        yield return new WaitForSeconds(seconds);
        if(playerController.moveInput != new Vector2(0, 0)&&playerPos.x > gameObject.transform.position.x + leftbound && playerPos.x < gameObject.transform.position.x + rightbound && playerPos.y > gameObject.transform.position.y + bottombound && playerPos.y < gameObject.transform.position.y + topbound)
        {
            noticedShifter = true;
        }
        else
        {
            if (transform.childCount > 0)
            {
                if (transform.GetChild(0).gameObject.CompareTag("SelectionUI")&&transform.childCount > 1)
            {
                Destroy(transform.GetChild(1).gameObject);
            }
            else if(transform.GetChild(0).gameObject.CompareTag("SelectionUI")==false)
            {
                Destroy(transform.GetChild(0).gameObject);
            }
            }
            suspectsShifter = false;
        }
    }
    IEnumerator canNoticeWait()
    {
        yield return new WaitForSeconds(startNoticeDelay);
        cannoticeshifter = true;
    }
}
