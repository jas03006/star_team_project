using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;



public class Progress_JGD : MonoBehaviour
{
    [SerializeField] private Slider sliderProgress;
    [SerializeField] private TextMeshProUGUI textProgressDate;
    [SerializeField] private float progressTime;

    public void Play(UnityAction action = null)
    {
        StartCoroutine(OnProgress(action));
    }
    private IEnumerator OnProgress(UnityAction action)
    {
        float current = 0;
        float percent = 0;

        while (percent<1)
        {
            current += Time.deltaTime;
            percent = current / progressTime;

            //Text ���� ����
            textProgressDate.text = $"Now Loading... {sliderProgress.value * 100:F0}%";
            //Slider�� ����
            sliderProgress.value = Mathf.Lerp(0, 1, percent);

            yield return null;
        }
        //action�� null�� �ƴϸ� action �޼ҵ� ����
        action?.Invoke();
    }


}