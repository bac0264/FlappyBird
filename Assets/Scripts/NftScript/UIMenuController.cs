using System;
using UnityEngine;
using UnityEngine.UI;

public class UIMenuController : MonoBehaviour
{
    public Text address;

    private void Awake()
    {
        address.text = "My Address: "+ PlayerPrefs.GetString("Account");
    }
}
