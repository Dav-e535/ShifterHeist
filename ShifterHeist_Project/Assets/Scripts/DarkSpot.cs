using System.Collections;
using UnityEngine;

public class DarkSpot : MonoBehaviour
{
    public float fadeSpeed;
    private SpriteRenderer spriteRenderer;
    public bool fadingin = false, fadingout = false, safeSpot = false;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public IEnumerator FadeOut()
    {
        fadingout = true;
        yield return new WaitForSeconds(0.01f);
        while (spriteRenderer.color.a !=0&&!fadingin)
        {
            Color newColor = spriteRenderer.color;
            newColor.a -= fadeSpeed;
            spriteRenderer.color = newColor;
            yield return new WaitForSeconds(0.01f);
        }
        fadingout = false;
    }
    public IEnumerator FadeIn()
    {
        fadingin = true;
        yield return new WaitForSeconds(0.01f);
        while (spriteRenderer.color.a !=1&&!fadingout)
        {
            Color newColor = spriteRenderer.color;
            newColor.a += fadeSpeed;
            spriteRenderer.color = newColor;
            yield return new WaitForSeconds(0.01f);
        }
        fadingin = false;
    }
}
