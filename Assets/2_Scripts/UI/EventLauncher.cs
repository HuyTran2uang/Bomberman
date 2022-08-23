using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EventLauncher : MonoBehaviour
{
    public void OnLoginButtonClicked()
    {
        Login.Instance.HandleLogin();
        Launcher.Instance.Connect();
    }

    public void OnCreateRoomButtonClicked()
    {
        CreateRoom.Instance.HandleCreateRoom();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void OnLeaveRoomButtonClicked()
    {
        LeaveRoom.Instance.HandleLeaveRoom();
    }

    public void OnStartGameButtonClicked()
    {
        StartGame.Instance.HandleStartGame();
    }
}
