using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class RollingLineDoTween : MonoBehaviour
{
    private RectTransform _rectTransform;
    private Vector2 startPos;
    // Start is called before the first frame update
    void Start()
    {
        _rectTransform = gameObject.GetComponent<RectTransform>();
        startPos = _rectTransform.anchoredPosition;
        
        MoveBanner();
    }

    private void MoveBanner()
    {
        _rectTransform.anchoredPosition = startPos;
        _rectTransform.DOAnchorPos(new Vector2(-403f, 0f), 10).SetEase(Ease.Linear).OnComplete(() => { MoveBanner();});
    }

}
