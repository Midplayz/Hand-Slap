using UnityEngine;
using UnityEngine.Rendering.Universal;

public class StopWatch : MonoBehaviour
{
    public Transform secondsHand;
    private float rotationSpeed;
    private Vector2 startingRotation;

    private float targetRotation;
    private float currentRotation;
    private float secondsRemaining;
    private bool startTimer = false;

    private void Start()
    {
        startingRotation = secondsHand.localRotation.eulerAngles;
    }

    private void Update()
    {
        if (startTimer)
        {
            secondsRemaining -= Time.deltaTime;
            if (secondsRemaining >= 0f)
            {
                float rotationAmount = rotationSpeed * Time.deltaTime;
                currentRotation += rotationAmount;
                targetRotation = currentRotation;

                RotateSecondsHand(targetRotation);
            }
            else 
            {
                secondsHand.localRotation = Quaternion.Euler(startingRotation);
                startTimer = false;
            }
        }
    }

    public void StartStopwatch(float seconds)
    {
        currentRotation = startingRotation.x;
        targetRotation = 360f;
        rotationSpeed = 360 / seconds;
        secondsRemaining = seconds;
        startTimer = true;
    }

    private void RotateSecondsHand(float rotation)
    {
        secondsHand.localRotation = Quaternion.Euler(0f, 0f, rotation);
    }
}
