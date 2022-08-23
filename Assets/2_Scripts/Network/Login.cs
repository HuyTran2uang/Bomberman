using UnityEngine;
using TMPro;
using Photon.Pun;

public class Login : MonoBehaviourSingleton<Login>
{
    [SerializeField] private TMP_InputField _inputName;

    public virtual void HandleLogin()
    {
        if (!string.IsNullOrEmpty(_inputName.text))
        {
            PhotonNetwork.LocalPlayer.NickName = _inputName.text;
            Debug.Log("user name: " + PhotonNetwork.LocalPlayer.NickName);
        }
        else
        {
            throw new System.Exception("Player name is invalid!");
        }
    }
}
