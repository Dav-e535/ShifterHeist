using UnityEngine;
using UnityEngine.AI;

public class PathFindPlayer : MonoBehaviour
{
    private NavMeshAgent navAgent;
    public GameObject target;
    private AgentReaction agRe;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
        navAgent.updateRotation = false;
        navAgent.updateUpAxis = false;
        agRe = GetComponent<AgentReaction>();
        target = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {

        if (agRe.runtowardsShifter&&!GetComponent<Human_Reaction>().noticedShifter&&GameObject.Find("Player").GetComponent<PlayerController>().alive&&!GameObject.Find("Player").GetComponent<PlayerController>().safe)
        {
            navAgent.SetDestination(target.transform.position);
        }
        else if(agRe.runtowardsShifter)
        {
            navAgent.SetDestination(transform.position);
        }
    }
}
