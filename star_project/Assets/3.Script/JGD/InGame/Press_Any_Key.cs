using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Press_Any_Key : MonoBehaviour
{
    private Image image;
    private float num = 0f;
    private float num2 = 1f;

    public Coroutine PressNext;

    private float time;
    public float FadeTime = 2f;
    private void Awake()
    {
        image = GetComponent<Image>();
    }

    private void Start()
    {
        PressNext = StartCoroutine(Press());
    }
    private IEnumerator Press()
    {
        while (true)
        {
            num += Time.unscaledDeltaTime * num2 ;
            if (1f <= num || 0f >= num)
                num2 *= -1f;
            float blink = Mathf.Lerp(0f, 1f, num);
            image.color = new Color(1f, 1f, 1f, blink);
            yield return null;
        }
    }
}
