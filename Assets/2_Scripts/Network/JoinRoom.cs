using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class JoinRoom : MonoBehaviourPunCallbacksSingleton<JoinRoom>
{
    public virtual void HandleJoinRoom(RoomInfo info)
    {
        PhotonNetwork.JoinRoom(info.Name);
        Debug.Log("Join room: " + info.Name);
    }
}
