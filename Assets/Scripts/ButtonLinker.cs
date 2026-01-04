using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Class to link Menu Button To the Popup Button
public class ButtonLinker : MonoBehaviour
{
    public Button menuButton;

    void Start()
    {
        menuButton.onClick.RemoveAllListeners();
        menuButton.onClick.AddListener(() =>
        {
            PopUpMenu.Instance.ShowPopUpMenu();
        });
    }
    
}
