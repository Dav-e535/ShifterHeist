using UnityEngine;

public class ServerScript : MonoBehaviour
{
    public bool isOn= false, canTurnOn;
    public GameManager gm;
    public int servernumber;
    private PlayerController player;
    public Sprite offsprite, onsprite;
    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        player = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canTurnOn&&player.canTurnOnServer&&player.use.triggered&&!isOn)
        {
            isOn = true;
            gm.ServerActivated(gameObject);
        }
        if (isOn)
        {
            GetComponent<SpriteRenderer>().sprite = onsprite;
        }
        else
        {
            GetComponent<SpriteRenderer>().sprite = offsprite;
        }
    }
    public void TurnOn()
    {
        
    }
}
