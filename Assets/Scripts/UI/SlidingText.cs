using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class SlidingText : MonoBehaviour
{
    public float moveDuration;
    public float toastDuration;
    public float shownYPos;
    public float hidenYPos;

    private RectTransform _rectTransform;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    public void Show()
    {
        StopAllCoroutines();
        _rectTransform.DOAnchorPosY(shownYPos, moveDuration).SetEase(Ease.OutCubic).OnComplete(()=> 
        {
            StartCoroutine(StartCountdownCoroutine());
        });
    }

    public void Hide()
    {
        _rectTransform.DOAnchorPosY(hidenYPos, moveDuration).SetEase(Ease.OutCubic);
    }

    private IEnumerator StartCountdownCoroutine()
    {
        yield return new WaitForSecondsRealtime(toastDuration);
        Hide();
    }

}
