using UnityEngine;

public class AppearInFront : MonoBehaviour
{
    public SpriteRenderer Sprite;
    public GameObject player;
    public float offset = 0.33f;
    void Start()
    {
        player = GameObject.Find("Player");
        Sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

        if((player.transform.position.y + offset) < gameObject.transform.position.y)
        {
            Sprite.sortingOrder = 2;
        }
        else
        {
            Sprite.sortingOrder = 4;
        }
    }
}
