using UnityEngine;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

public class CreateRoom : MonoBehaviourSingleton<CreateRoom>
{
    [SerializeField] private TMP_InputField _inputName;
    [SerializeField] private byte _maxPlayersPerRoom = 4;

    public virtual void HandleCreateRoom()
    {
        if (!string.IsNullOrEmpty(_inputName.text))
        {
            PhotonNetwork.CreateRoom(_inputName.text, new RoomOptions { MaxPlayers = _maxPlayersPerRoom });
            Debug.Log("Created room: " + _inputName.text);
        }
        else
        {
            throw new System.Exception("Room name is invalid!");
        }
    }
}