using System.Collections;
using UnityEngine;

public class AutoColorChange : MonoBehaviour
{
    public Color targetColor = Color.red;
    public float transitionDuration = 1.0f;
    public float delayBetweenTransitions = 1.0f;

    private void Start()
    {
        StartCoroutine(AutoChangeColor());
    }

    private IEnumerator AutoChangeColor()
    {
        Renderer renderer = GetComponent<Renderer>();

        while (true)
        {
            Color initialColor = renderer.material.color;
            float elapsedTime = 0f;

            while (elapsedTime < transitionDuration)
            {
                renderer.material.color = Color.Lerp(initialColor, targetColor, elapsedTime / transitionDuration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            renderer.material.color = targetColor;
            yield return new WaitForSeconds(delayBetweenTransitions);
            Color tempColor = targetColor;
            targetColor = initialColor;
            initialColor = tempColor;
        }
    }
}
