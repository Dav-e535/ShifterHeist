using System.Collections;
using UnityEngine;

public class FollowTrack : MonoBehaviour
{
    public Vector3[] direction;
    private Human_Reaction humanReaction;
    public float speed;
    void Start()
    {
        humanReaction = gameObject.GetComponent<Human_Reaction>();
        
        StartCoroutine(GoTowards(direction));
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator GoTowards(Vector3[] direction)
    {
        for(int i = 0; i != direction.Length; i++)
        {
        Vector3 initialPos = transform.position;
            if (direction[i].y > 0)
            {
                if(direction[i].x > 0)
                {
                    while (transform.position.y < (initialPos + direction[i]).y&&transform.position.x < (initialPos + direction[i]).x&&humanReaction.noticedShifter==false||GetComponent<Human_Reaction>().cannoticeshifter)
                    {   
                        yield return null;
                        transform.Translate(direction[i].normalized*speed*Time.deltaTime);
                    }
                }
                else if(direction[i].x < 0)
                {
                    while (transform.position.y < (initialPos + direction[i]).y&&transform.position.x > (initialPos + direction[i]).x&&humanReaction.noticedShifter==false||!GetComponent<Human_Reaction>().cannoticeshifter)
                    {   
                        yield return null;
                        transform.Translate(direction[i].normalized*speed*Time.deltaTime);
                    }
                }
                else
                {
                    while (transform.position.y < (initialPos + direction[i]).y&&humanReaction.noticedShifter==false||!GetComponent<Human_Reaction>().cannoticeshifter)
                    {   
                        yield return null;
                        transform.Translate(direction[i].normalized*speed*Time.deltaTime);
                    }
                }
                
            }
            else if(direction[i].y < 0)
            {
                if(direction[i].x > 0)
                {
                    while (transform.position.y > (initialPos + direction[i]).y&&transform.position.x < (initialPos + direction[i]).x&&humanReaction.noticedShifter==false||!GetComponent<Human_Reaction>().cannoticeshifter)
                    {   
                        yield return null;
                        transform.Translate(direction[i].normalized*speed*Time.deltaTime);
                    }
                }
                else if(direction[i].x < 0)
                {
                    while (transform.position.y > (initialPos + direction[i]).y&&transform.position.x > (initialPos + direction[i]).x&&humanReaction.noticedShifter==false||!GetComponent<Human_Reaction>().cannoticeshifter)
                    {   
                        yield return null;
                        transform.Translate(direction[i].normalized*speed*Time.deltaTime);
                    }
                }
                else
                {
                    while (transform.position.y > (initialPos + direction[i]).y&&humanReaction.noticedShifter==false||!GetComponent<Human_Reaction>().cannoticeshifter)
                    {   
                        yield return null;
                        transform.Translate(direction[i].normalized*speed*Time.deltaTime);
                    }
                }
            }
            else
            {
                if(direction[i].x > 0)
                {
                    while (transform.position.x < (initialPos + direction[i]).x&&humanReaction.noticedShifter==false||!GetComponent<Human_Reaction>().cannoticeshifter)
                    {   
                        yield return null;
                        transform.Translate(direction[i].normalized*speed*Time.deltaTime);
                    }
                }
                else if(direction[i].x < 0)
                {
                    while (transform.position.x > (initialPos + direction[i]).x&&humanReaction.noticedShifter==false||!GetComponent<Human_Reaction>().cannoticeshifter)
                    {   
                        yield return null;
                        transform.Translate(direction[i].normalized*speed*Time.deltaTime);
                    }
                }
                else
                {
                    yield return null;
                    
                }
            }
        
        }
    }

}
