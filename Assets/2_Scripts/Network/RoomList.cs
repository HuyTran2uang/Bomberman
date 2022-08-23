using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class RoomList : MonoBehaviourPunCallbacks
{
    [SerializeField] private Transform _roomListContent;
    [SerializeField] private GameObject _roomListItemPrefab;

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        Debug.Log("update room list item");
        foreach (Transform trans in _roomListContent)
        {
            Destroy(trans.gameObject);
        }

        for (int i = 0; i < roomList.Count; i++)
        {
            if (roomList[i].RemovedFromList)
                continue;
            Instantiate(_roomListItemPrefab, _roomListContent).GetComponent<RoomListItem>().Setup(roomList[i]);
        }
    }
}
