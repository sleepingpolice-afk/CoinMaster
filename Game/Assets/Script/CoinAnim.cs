using UnityEngine;
using DG.Tweening;
using UnityEngine.EventSystems;

public class CoinAnim : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
{
    private Vector3 originalScale;
    public float scaleFactor = 1.2f;
    public float duration = 0.15f;

    void Start()
    {
        originalScale = transform.localScale;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        transform.DOScale(originalScale * scaleFactor, duration).SetEase(Ease.OutBack);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        transform.DOScale(originalScale, duration).SetEase(Ease.InBack);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Return to normal if the pointer exits the button while pressed
        transform.DOScale(originalScale, duration).SetEase(Ease.InBack);
    }
}
