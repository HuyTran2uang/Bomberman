using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

public class RoomListItem : MonoBehaviour
{
    [SerializeField] private TMP_Text _nameRoom;
    [SerializeField] private TMP_Text _countPlayer;
    [SerializeField] private Button _buttonJoinRoom;
    [SerializeField] private GameObject _listRoom;
    [SerializeField] private GameObject _room;
    private RoomInfo _info;

    private void OnEnable()
    {
        _listRoom = FindObjectOfType<Canvas>().transform.Find("ListRoom").gameObject;
        _room = FindObjectOfType<Canvas>().transform.Find("Room").gameObject;
        _buttonJoinRoom.onClick.AddListener(() => OnJoinedRoomButtonClick());
    }

    public void Setup(RoomInfo info)
    {
        _info = info;
        _nameRoom.text = _info.Name;
        _countPlayer.text = $"{_info.PlayerCount}/{_info.MaxPlayers}";
    }

    public void OnJoinedRoomButtonClick()
    {
        JoinRoom.Instance.HandleJoinRoom(_info);
        _listRoom.SetActive(false);
        _room.SetActive(true);
    }
}
