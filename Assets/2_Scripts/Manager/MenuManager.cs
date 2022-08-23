using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviourSingleton<MenuManager>
{
    [SerializeField] private Menu[] _menus;

    public void OpenMenu(string menuName)
    {
        for (int i = 0; i < _menus.Length; i++)
        {
            if (_menus[i].GetMenuName == menuName)
            {
                _menus[i].Open();
            }
            else if (_menus[i].GetCheckOpen)
            {
                CloseMenu(_menus[i]);
            }
        }
    }

    public void OpenMenu(Menu menu)
    {
        for (int i = 0; i < _menus.Length; i++)
        {
            if (_menus[i].GetCheckOpen)
            {
                CloseMenu(_menus[i]);
            }
        }
        menu.Open();
    }

    public void CloseMenu(Menu menu)
    {
        menu.Close();
    }
}
