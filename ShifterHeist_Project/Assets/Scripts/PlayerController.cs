using System.Collections;
using Unity.VisualScripting;
using UnityEditor.PackageManager.Requests;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public InputAction moveAction;
    public float moveSpeed = 5f;
    public Vector2 moveInput;
    public Rigidbody2D playerRb;
    public SpriteRenderer playerSprite;
    public GameObject[] shiftList;
    public InputAction[] shapeshiftActions;
    public GameObject shiftOptionUI;
    public string shiftedTag = "Shifter";
    public float shiftNumber = 0;
    public bool alive = true, canUse = false;
    public bool turnsBack = true;
    public Animator animator;
    public InputAction attack, restart, use;
    public float atkcooldown = 1f;
    private bool canEnterVent = false;
    public Vector3 secondVentPosition;
    void Start()
    {
        moveAction.Enable();
        playerRb = GetComponent<Rigidbody2D>();
        playerSprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        foreach (InputAction shapeshiftAction in shapeshiftActions)
        {
            shapeshiftAction.Enable();
        }
        attack.Enable();
        restart.Disable();
        use.Enable();

    }

    // Update is called once per frame
    void Update()
    {
        if (restart.triggered)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        animator.SetFloat("WalkDirX", moveInput.x);
        moveInput = moveAction.ReadValue<Vector2>();
        transform.Translate(moveInput * moveSpeed * Time.deltaTime);
        if(moveInput != new Vector2(0, 0))
        {
            animator.SetFloat("Speed",1);
        }
        else
        {
            animator.SetFloat("Speed",0);
        }
        if (attack.triggered)
        {
            ShapeShift(0);
            moveAction.Disable();
            animator.SetTrigger("attack");
            StartCoroutine(Attackperiod(0.9f));
            attack.Disable();
            StartCoroutine(Attackcooldown(atkcooldown));
            if(canEnterVent == true)
            {
                GameObject vent = GameObject.FindGameObjectWithTag("Vent");
                
                VentEnter(vent);

                
            }
        }
        if (shiftNumber != 0)
        {
            MaxShiftTime(60f);
        }
        foreach (InputAction shapeshiftAction in shapeshiftActions)
        {
            if (shapeshiftAction.triggered)
            {
                int index = System.Array.IndexOf(shapeshiftActions, shapeshiftAction);
                ShapeShift(index);
            }
        }
        if (alive != true)
        {
            DeathSequence();
        }
        if (canUse && use.triggered&&usableObject!=null)
        {
            usableObject.GetComponent<Usable>().UseTriggered();
        }

    }
    public GameObject usableObject;
    public void ShiftOptionUI(GameObject shiftOption)
    {
        Instantiate(shiftOptionUI, shiftOption.transform.position, shiftOptionUI.transform.rotation,shiftOption.transform);
    }
    public IEnumerator MaxShiftTime(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        ShapeShift(0);
    }
    public IEnumerator Attackperiod(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        moveAction.Enable();
    }
    public IEnumerator Attackcooldown(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        attack.Enable();
    }
    public void ShapeShift(int index)
    {
        if(index != 0)
        {
            animator.enabled = false;
            StartCoroutine(MaxShiftTime(30f));
        }
        else
        {
            animator.enabled = true;
            StopCoroutine(MaxShiftTime(60f));
        }
        playerSprite.sprite = shiftList[index].GetComponent<SpriteRenderer>().sprite;
        shiftedTag = shiftList[index].tag;
        shiftNumber = index;
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Vent"))
        {
            canEnterVent = true;
        }
        if (collision.CompareTag("Item"))
        {
            canUse = true;
            usableObject = collision.gameObject;
        }
        if (collision.CompareTag("Door"))
        {
            collision.GetComponent<DoorScript>().OpenDoor();
        }

    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Vent"))
        {
            canEnterVent = false;
            
        }
        if (collision.CompareTag("Item"))
        {
            canUse = false;
            usableObject = null;
        }

    }
    public void VentEnter(GameObject vent)
    {
        Vector3 ventPosition = vent.transform.position;
        Destroy(vent);
        StartCoroutine(VentAnimation(0.9f, ventPosition));
        
        
    }
    public IEnumerator VentAnimation(float seconds, Vector3 ventPosition)
    {
        yield return new WaitForSeconds(seconds);
        animator.SetTrigger("attack");
        StartCoroutine(VentAnimation2(0.5f, ventPosition));
    }
    public IEnumerator VentAnimation2(float seconds, Vector3 ventPosition)
    {
        moveAction.Disable();
        yield return new WaitForSeconds(seconds);
        while(transform.position != ventPosition+new Vector3(0, -0.1f, 0))
        {
            yield return new WaitForSeconds(0.01f);
            transform.position = Vector3.MoveTowards(transform.position, ventPosition+new Vector3(0, -0.1f, 0), moveSpeed * Time.deltaTime);
            transform.localScale = transform.localScale - new Vector3(0.01f, 0.01f, 0);
        }
        transform.position = secondVentPosition;
        transform.localScale = new Vector3(1, 1, 1);
        ShapeShift(0);
        System.Array.Resize(ref shiftList, 1);
        moveAction.Enable();
    }
    public void DeathSequence()
    {
        moveAction.Disable();
        foreach (InputAction shapeshiftAction in shapeshiftActions)
        {
            shapeshiftAction.Disable();
        }
        attack.Disable();
        restart.Enable();
    }
}
