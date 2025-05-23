using System;

using UnityEngine;
using UnityEngine.EventSystems;

public class BackdropController : MonoBehaviour, IPointerClickHandler
{
    public event Action OnOverlayClicked;

    public void OnPointerClick(PointerEventData eventData)
    {
        OnOverlayClicked?.Invoke();
    }
}
