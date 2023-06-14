using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonClick : MonoBehaviour, IPointerClickHandler
{
    public GameObject text;
    public void OnPointerClick(PointerEventData eventData)
    {
        text.SetActive(false);
    }
}