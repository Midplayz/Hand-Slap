using UnityEngine;
using UnityEngine.UI;

public class ComicStyleAnimation : MonoBehaviour
{
    public Image image;
    public float animationSpeed = 2f;
    public float scaleAmount = 1.2f;
    private bool animateNow = false;

    private Vector3 originalScale;

    private void Start()
    {
        image = GetComponent<Image>();
        originalScale = image.transform.localScale;
    }
    private void OnEnable()
    {
        animateNow = true;
    }
    private void OnDisable()
    {
        animateNow = false;
    }
    private void Update()
    {
        if (animateNow)
        {
            float scale = Mathf.PingPong(Time.time * animationSpeed, 1f) * (scaleAmount - 1f) + 1f;
            image.transform.localScale = originalScale * scale;

            image.transform.Rotate(0f, 0f, animationSpeed * Time.deltaTime);
        }
    }
}
