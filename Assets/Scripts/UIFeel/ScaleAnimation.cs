using UnityEngine;

public class ScaleAnimation : MonoBehaviour
{
    public Vector3 targetScale;
    private float animationDuration = 0.15f;

    private Vector3 initialScale;

    private void Start()
    {
        initialScale = Vector3.zero;
    }

    private void OnEnable()
    {
        StartCoroutine(AnimateScale());
    }

    private System.Collections.IEnumerator AnimateScale()
    {
        float timeElapsed = 0f;

        while (timeElapsed < animationDuration)
        {
            timeElapsed += Time.unscaledDeltaTime;
            float t = Mathf.Clamp01(timeElapsed / animationDuration);

            // Apply smooth scale interpolation
            transform.localScale = Vector3.Lerp(initialScale, targetScale, t);

            yield return null;
        }

        transform.localScale = targetScale;
    }
}
