using System.Collections;
using UnityEngine;

public class SelectionUI : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public float fadeSpeed = 0.1f;
    public float animationDuration = 4f;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(DestroyAfterTime(animationDuration));
    }
    private bool animationPlayed = false;
    // Update is called once per frame
    void Update()
    {
        
        if(animationPlayed)
        {
            StartCoroutine(FadeAfterAnimation());
        }
    }
    private IEnumerator DestroyAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        animationPlayed = true;
    }
    private IEnumerator FadeAfterAnimation()
    {
        while (spriteRenderer.color.a > 0)
        {
            Color newColor = spriteRenderer.color;
            newColor.a -= fadeSpeed;
            spriteRenderer.color = newColor;
            yield return new WaitForSeconds(0.01f);
        }
        Destroy(gameObject);
    }
}
