using System.Collections;
using UnityEngine;

public class AgentReaction : MonoBehaviour
{
    public Animator agentAnimator;
    public bool noticedShifter = false, objectNoticePending = false, agentAlive, targetHuman = false;
    public float topbound = 0.5f, bottombound = -0.5f, leftbound = -0.5f, rightbound = 0.5f;
    private GameObject player;
    public GameObject humanReported;
    public bool runtowardsShifter=false;
    private PlayerController playerController;
    public float shifterNoticeDelay, humanNoticeDelay, objectNoticeDelay;
    void Start()
    {
        topbound = GetComponent<Human_Reaction>().topbound;
        leftbound = GetComponent<Human_Reaction>().leftbound;
        bottombound = GetComponent<Human_Reaction>().bottombound;
        rightbound = GetComponent<Human_Reaction>().rightbound;
        shifterNoticeDelay = GetComponent<Human_Reaction>().shifterNoticeDelay;
        humanNoticeDelay = GetComponent<Human_Reaction>().humanNoticeDelay;
        objectNoticeDelay = GetComponent<Human_Reaction>().objectNoticeDelay;
        agentAlive = GetComponent<Human_Reaction>().alive;
        agentAnimator = GetComponent<Animator>();
        player = GameObject.Find("Player");
        agentAnimator.SetBool("noticed", false);
        playerController = player.GetComponent<PlayerController>();
        objectNoticePending = false;
    }

    // Update is called once per frame
    void Update()
    {

        //if (agentAlive)
        //{
        Vector3 playerPos = player.transform.position;
        if (!player.GetComponent<PlayerController>().safe&&GetComponent<Human_Reaction>().cannoticeshifter&&playerPos.x > gameObject.transform.position.x + leftbound && playerPos.x < gameObject.transform.position.x + rightbound && playerPos.y > gameObject.transform.position.y + bottombound && playerPos.y < gameObject.transform.position.y + topbound)
        {
            noticedShifter = true;
        }
        else
        {
            noticedShifter = false;
        }
        if (!player.GetComponent<PlayerController>().safe&&noticedShifter && player.GetComponent<PlayerController>().alive && agentAnimator.GetBool("noticed") == false && playerController.shiftedTag == "Shifter")
        {
            StartCoroutine(TimeUntilNoticed(shifterNoticeDelay));
        }
        if (!player.GetComponent<PlayerController>().safe&&!objectNoticePending &&noticedShifter && player.GetComponent<PlayerController>().alive && agentAnimator.GetBool("noticed") == false && playerController.shiftedTag == "Object"&&playerController.moveInput!=new Vector2(0,0))
        {
            
            StartCoroutine(TimeUntilNoticedObject(objectNoticeDelay));
        }
        if(!player.GetComponent<PlayerController>().safe&&noticedShifter&&player.GetComponent<PlayerController>().alive&&agentAnimator.GetBool("noticed")==false&&playerController.shiftedTag=="Human")
        {
            StartCoroutine(TimeUntilNoticed(humanNoticeDelay));
            
            
        }
        if(player.GetComponent<PlayerController>().alive != true)
        {
            StopCoroutine(TimeUntilNoticed(shifterNoticeDelay));
            StopCoroutine(TimeUntilNoticed(humanNoticeDelay));
            StopCoroutine(TimeUntilNoticedObject(objectNoticeDelay));
        }
        if (runtowardsShifter&&!GetComponent<Human_Reaction>().noticedShifter&&GetComponent<Human_Reaction>().suspectsShifter)
        {
            targetHuman = true;
            
        }
        else if (runtowardsShifter)
        {
            GetComponent<PathFindPlayer>().target = player;
        }
        if(targetHuman == true)
        {
            GetComponent<PathFindPlayer>().target = humanReported;
        }
        
        
        //}
        
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
            objectNoticePending = true;
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
                objectNoticePending = false;
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
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("human"))
        {
            targetHuman = false;
        }
    }

}
