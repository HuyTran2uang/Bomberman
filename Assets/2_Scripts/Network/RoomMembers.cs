using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

public class RoomMembers : MonoBehaviourPunCallbacks
{
    [SerializeField] private Transform _room;
    [SerializeField] private Transform _parent;
    [SerializeField] private GameObject _buttonStartGame;
    [SerializeField] private GameObject _playerListItemPrefabs;

    public override void OnJoinedRoom()
    {
        Debug.Log("Joined room");
        ShowPlayerInRoom();
        if (PhotonNetwork.IsMasterClient)
        {
            _buttonStartGame.SetActive(true);
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log("Player enter room");
        Instantiate(_playerListItemPrefabs, Vector3.zero, Quaternion.identity, _parent).GetComponent<PlayerListItem>().Setup(newPlayer);
    }

    private void ShowPlayerInRoom()
    {
        Player[] players = PhotonNetwork.PlayerList;

        foreach (Transform child in _room)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < players.Length; i++)
        {
            Instantiate(_playerListItemPrefabs, Vector3.zero, Quaternion.identity, _parent).GetComponent<PlayerListItem>().Setup(players[i]);
        }
    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        _buttonStartGame.SetActive(PhotonNetwork.IsMasterClient);
    }
}
