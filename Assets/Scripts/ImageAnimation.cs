using UnityEngine;
using UnityEngine.UI;

public class ImageAnimation : MonoBehaviour
{
    [field: Header("------- Win & Lose Image Animation -------")]
    [field: SerializeField] private Image baseImage;
    [field: SerializeField] private Image deFlexImage;
    [field: SerializeField] private Image flexImage;
    [field: SerializeField] private float interval = 1f;

    private bool startAnimation = false;
    private Image imageToUse;
    private bool isImage1Active = true;
    private float timer;
    private void Start()
    {
        timer = interval;
    }

    private void Update()
    {
        if (startAnimation)
        {
            timer -= Time.deltaTime;

            if (timer <= 0f)
            {
                baseImage.gameObject.SetActive(isImage1Active);
                imageToUse.gameObject.SetActive(!isImage1Active);

                timer = interval;
                isImage1Active = !isImage1Active;
            }
        }
    }

    public void SetImage(bool hasWon)
    {
        if (hasWon)
        {
            imageToUse = flexImage;
        }
        else
        {
            imageToUse = deFlexImage;
        }
        timer = interval;
        startAnimation = true;
    }
    public void ResetEverything()
    {
        startAnimation = false;
        baseImage.gameObject.SetActive(false);
        flexImage.gameObject.SetActive(false);
        deFlexImage.gameObject.SetActive(false);
    }
}
