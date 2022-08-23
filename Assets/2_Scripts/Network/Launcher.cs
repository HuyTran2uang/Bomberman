using UnityEngine;
using Photon.Pun;

public class Launcher : MonoBehaviourPunCallbacksSingleton<Launcher>
{
    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public void Connect()
    {
        PhotonNetwork.ConnectUsingSettings();
        Debug.Log("Connected To Master");
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
        Debug.Log("Joined Lobby");
    }
}
