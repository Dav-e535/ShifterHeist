using UnityEngine;

public class Usable : MonoBehaviour
{
    public string inCodeTag;
    private GameManager gm;
    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void UseTriggered()
    {
        if (gameObject.CompareTag("Item"))
        {
            gm.ActivateProgressPoint(gameObject);
            Destroy(gameObject);
        }
    }
}
