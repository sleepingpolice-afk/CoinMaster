using UnityEngine;
using UnityEngine.EventSystems;

public class PanelCloseHandler : MonoBehaviour, IPointerClickHandler
{
    public UpgradePanelController panelController;

    public void OnPointerClick(PointerEventData eventData)
    {
        panelController.ClosePanel();
    }
}
