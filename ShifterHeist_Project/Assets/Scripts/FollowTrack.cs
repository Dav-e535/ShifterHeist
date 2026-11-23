using System.Collections;
using UnityEngine;

public class FollowTrack : MonoBehaviour
{
    public Vector3[] direction;
    private Human_Reaction humanReaction;
    public float speed;
    public bool repeats = false, waitsEveryTurn = false;
    public float repeatWait;
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
        int way = 1;
        for(int i = 0; i != direction.Length+1; i+=way)
        {
            
            if (i == 0||waitsEveryTurn)
            {
                yield return new WaitForSeconds(repeatWait);
            }
            
            Vector3 initialPos = transform.position;
            if (direction[i] == null)
            {
                yield return null;
            }
            else if (direction[i].y > 0)
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
            if((i==direction.Length - 1||i+way==-1)&&repeats)
            {
                way=-way;
                for (int j = 0; j < direction.Length; j++)
                {
                    direction[j].x = -direction[j].x;
                    direction[j].y = -direction[j].y;
                }
            }
        }
    }

}
