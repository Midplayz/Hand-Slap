using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ColourTransition : MonoBehaviour
{
    public TextMeshProUGUI textMeshPro;
    public List<Color> colors;
    public float transitionDuration = 1f;

    private int currentIndex = 0;
    private Color startColor;
    private Color targetColor;
    private float transitionTimer = 0f;

    private void Start()
    {
        if (colors.Count > 0)
        {
            startColor = colors[0];
            targetColor = colors[0];
        }
    }

    private void Update()
    {
        if (colors.Count < 2)
            return;

        transitionTimer += Time.deltaTime;

        if (transitionTimer >= transitionDuration)
        {
            currentIndex = (currentIndex + 1) % colors.Count;
            startColor = targetColor;
            targetColor = colors[currentIndex];
            transitionTimer = 0f;
        }

        float t = transitionTimer / transitionDuration;
        Color currentColor = Color.Lerp(startColor, targetColor, t);

        textMeshPro.color = currentColor;
    }
}
