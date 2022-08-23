using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] private string _menuName;
    [SerializeField] private bool _isOpen;

    public string GetMenuName
    {
        get { return _menuName; }
    }

    public bool GetCheckOpen
    {
        get { return _isOpen; }
    }

    public void Open()
    {
        _isOpen = true;
        gameObject.SetActive(true);
    }

    public void Close()
    {
        _isOpen = false;
        gameObject.SetActive(false);
    }
}
