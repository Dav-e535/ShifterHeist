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
        while (transform.position != initialPos + direction[i]&&humanReaction.noticedShifter==false)
        {
            yield return null;
            transform.Translate(direction[i].normalized*speed*Time.deltaTime);
        }
        }
    }

}
