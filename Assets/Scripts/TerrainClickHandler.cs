using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class TerrainClickHandler : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        BaseClickHandler selectedBase = FindObjectsByType<BaseClickHandler>(FindObjectsSortMode.None).
            Where(handler => handler.IsSelected == true).
            First();

        selectedBase.GetComponent<NewBaseCreatingState>().SetBuildingPosition(eventData.pointerCurrentRaycast.worldPosition);
    }
}
