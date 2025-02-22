using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    public Transform cameraTransform;   
    public float shakeDuration = 0f;    
    private float shakeMagnitude = 0.05f; 

    private Vector3 originalPosition;   
    private float currentShakeDuration;

    private void Start()
    {
            originalPosition = cameraTransform.localPosition;
    }

    private void Update()
    {
        if (currentShakeDuration > 0)
        {
            cameraTransform.localPosition = originalPosition + Random.insideUnitSphere * shakeMagnitude;

            currentShakeDuration -= Time.deltaTime;
        }
        else
        {
            cameraTransform.localPosition = originalPosition;
        }
    }

    public void Shake()
    {
        currentShakeDuration = shakeDuration;
    }
}
