using System.Collections;
using UnityEngine;

public class AgentReaction : MonoBehaviour
{
    public Animator agentAnimator;
    public bool noticedShifter = false, objectNoticePending = false;
    public float topbound = 0.5f, bottombound = -0.5f, leftbound = -0.5f, rightbound = 0.5f;
    private GameObject player;
    private PlayerController playerController;
    public float shifterNoticeDelay, humanNoticeDelay, objectNoticeDelay;
    void Start()
    {
        shifterNoticeDelay = GetComponent<Human_Reaction>().shifterNoticeDelay;
        humanNoticeDelay = GetComponent<Human_Reaction>().humanNoticeDelay;
        objectNoticeDelay = GetComponent<Human_Reaction>().objectNoticeDelay;
        agentAnimator = GetComponent<Animator>();
        player = GameObject.Find("Player");
        agentAnimator.SetBool("noticed", false);
        playerController = player.GetComponent<PlayerController>();
        objectNoticePending = false;
    }

    // Update is called once per frame
    void Update()
    {
        
        
        Vector3 playerPos = player.transform.position;
        if (GetComponent<Human_Reaction>().cannoticeshifter&&playerPos.x > gameObject.transform.position.x + leftbound && playerPos.x < gameObject.transform.position.x + rightbound && playerPos.y > gameObject.transform.position.y + bottombound && playerPos.y < gameObject.transform.position.y + topbound)
        {
            noticedShifter = true;
        }
        else
        {
            noticedShifter = false;
        }
        if (noticedShifter && player.GetComponent<PlayerController>().alive && agentAnimator.GetBool("noticed") == false && playerController.shiftedTag == "Shifter")
        {
            StartCoroutine(TimeUntilNoticed(shifterNoticeDelay));
        }
        if (!objectNoticePending &&noticedShifter && player.GetComponent<PlayerController>().alive && agentAnimator.GetBool("noticed") == false && playerController.shiftedTag == "Object"&&playerController.moveInput!=new Vector2(0,0))
        {
            objectNoticePending = true;
            StartCoroutine(TimeUntilNoticedObject(objectNoticeDelay));
        }
        if(noticedShifter&&player.GetComponent<PlayerController>().alive&&agentAnimator.GetBool("noticed")==false&&playerController.shiftedTag=="Human")
        {
            StartCoroutine(TimeUntilNoticed(humanNoticeDelay));
            
            
        }
        if(player.GetComponent<PlayerController>().alive != true)
        {
            StopCoroutine(TimeUntilNoticed(shifterNoticeDelay));
            StopCoroutine(TimeUntilNoticed(humanNoticeDelay));
            StopCoroutine(TimeUntilNoticedObject(objectNoticeDelay));
        }
        
        
        
    }
    public IEnumerator TimeUntilNoticed(float seconds)
        {
            yield return new WaitForSeconds(seconds);
            Vector3 playerPos = player.transform.position;
            if (playerPos.x > gameObject.transform.position.x + leftbound && playerPos.x < gameObject.transform.position.x + rightbound && playerPos.y > gameObject.transform.position.y + bottombound && playerPos.y < gameObject.transform.position.y + topbound)
            {
                agentAnimator.SetBool("noticed", true);
                
                StartCoroutine(WhileNoticed(3f));
            }
            else
            {
                agentAnimator.SetBool("noticed", false);
            }
            
        }
public IEnumerator TimeUntilNoticedObject(float seconds)
        {
            yield return new WaitForSeconds(seconds);
            Vector3 playerPos = player.transform.position;
            if (playerPos.x > gameObject.transform.position.x + leftbound && playerPos.x < gameObject.transform.position.x + rightbound && playerPos.y > gameObject.transform.position.y + bottombound && playerPos.y < gameObject.transform.position.y + topbound&&playerController.moveInput!=new Vector2(0,0))
            {
                agentAnimator.SetBool("noticed", true);
                
                StartCoroutine(WhileNoticed(3f));
            }
            else
            {
                agentAnimator.SetBool("noticed", false);
            }
            
        }
        public IEnumerator WhileNoticed(float seconds)
        {
            yield return new WaitForSeconds(0.05f);
            player.GetComponent<PlayerController>().alive = false;
            yield return new WaitForSeconds(seconds);
            agentAnimator.SetBool("noticed", false);
            if(playerController.shiftedTag == "Object")
        {
            objectNoticePending = false;
        }
        }
    
}
