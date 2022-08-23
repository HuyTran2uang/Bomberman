using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class LeaveRoom : MonoBehaviourSingleton<LeaveRoom>
{
    public void HandleLeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }
}
