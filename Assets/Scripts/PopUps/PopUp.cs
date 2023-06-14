using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopUp : MonoBehaviour
{
    Dialog dialog = new Dialog();
    public static PopUp instence;
    public Text titleText;
    public TMP_Text messageText;
    public Button popUpButton;
    public GameObject popUpCanvas;

    private void Awake()
    {
        instence = this;
        popUpButton.onClick.RemoveAllListeners();
        popUpButton.onClick.AddListener(Hide);
    }
    public PopUp SetTitle(string title)
    {
        dialog.Title = title;
        return instence;
    }
    
    public PopUp SetMessage(string message)
    {
        dialog.Message = message;
        return instence;
    }
    public void Show()
    {
        titleText.text = dialog.Title;
        messageText.text = dialog.Message;
        popUpCanvas.SetActive(true);

    }
    public void Hide()
    {
        popUpCanvas.SetActive(false);
        dialog = new Dialog();
    }



}

public class Dialog
{
    public string Title = "Error";
    public string Message = "Username Already Taken";
    
}
