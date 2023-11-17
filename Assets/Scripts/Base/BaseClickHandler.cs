using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BaseClickHandler : MonoBehaviour, IPointerClickHandler
{
    private bool _isSelected;

    public bool  IsSelected
    {
        get { return _isSelected; }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        BaseClickHandler[] baseClickHandlers = FindObjectsByType<BaseClickHandler>(FindObjectsSortMode.None);

        foreach (BaseClickHandler baseClickHandler in baseClickHandlers)
            baseClickHandler.CancelSelection();

        _isSelected = true;
    }

    public void CancelSelection()
    {
        _isSelected = false;
    }
}
