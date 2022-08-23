using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class StartGame : MonoBehaviourSingleton<StartGame>
{
    public void HandleStartGame()
    {
        PhotonNetwork.LoadLevel(1);
    }
}
