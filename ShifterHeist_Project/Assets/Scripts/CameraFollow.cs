
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject player;
    public float smoothSpeed = 0.125f;
    private Vector2 relativePosition;
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        relativePosition = player.transform.position - transform.position;
/*
        if (player.transform.position.x >= 13.5)
        {
            //relativePosition = new Vector2(13-transform.position.x, player.transform.position.y-transform.position.y);
            relativePosition.x = 13.5f-transform.position.x;
        }
        if (player.transform.position.x <= -13.5f)
        {
            //relativePosition = new Vector2(-13-transform.position.x, player.transform.position.y-transform.position.y);
            relativePosition.x = -13.5f-transform.position.x;
        }
        if (player.transform.position.y <= -0.4f)
        {
            //relativePosition = new Vector2(player.transform.position.x-transform.position.x, -0.5f-transform.position.y);
            relativePosition.y = -0.4f-transform.position.y;
        }
        if (player.transform.position.y >= 31.5f)
        {
            //relativePosition = new Vector2(player.transform.position.x-transform.position.x, 31.5f-transform.position.y);
            relativePosition.y = 31.5f-transform.position.y;
        }
        */
        
        if(relativePosition.x>0.02f||relativePosition.y>0.02f||relativePosition.x<-0.02f||relativePosition.y<-0.02f)
        {
            transform.Translate(new Vector2(relativePosition.x/smoothSpeed, relativePosition.y/smoothSpeed) * Time.deltaTime);
        }
        
        
        
        /*if (player.transform.position.x <= 13&&player.transform.position.x >= -13)
        { 
            transform.Translate(new Vector2(relativePosition.x/smoothSpeed, 0) * Time.deltaTime);
        }
        if (player.transform.position.y <= 31.5f&&player.transform.position.y >= -0.5f)
        {
            transform.Translate(new Vector2(0,relativePosition.y/smoothSpeed) * Time.deltaTime);
        }*/
        
        /*if(relativePosition.x>0.1f||relativePosition.y>0.1f||relativePosition.x<-0.1f||relativePosition.y<-0.1f)
        {
            transform.Translate(new Vector2(relativePosition.x/smoothSpeed, relativePosition.y/smoothSpeed) * Time.deltaTime);
        }*/


        /*if (player.transform.position.x <= 13&&player.transform.position.x >= -13)
        {
            transform.position = new Vector3(player.transform.position.x, transform.position.y, transform.position.z);
        }
        if (player.transform.position.y <= 31.5f&&player.transform.position.y >= -0.5f)
        {
            transform.position = new Vector3(transform.position.x, player.transform.position.y, transform.position.z);
        }*/
        
    }
}
