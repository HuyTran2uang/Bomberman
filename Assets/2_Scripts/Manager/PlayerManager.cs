using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;

public class PlayerManager : MonoBehaviourSingletonPersistent<PlayerManager>
{
    private PhotonView _photonView;
    [SerializeField] private GameObject _playerController;

    protected override void Awake()
    {
        base.Awake();
        _photonView = GetComponent<PhotonView>();
    }

    void Start()
    {
        if (_photonView.IsMine)
        {
            CreateController();
        }
    }

    void CreateController()
    {
        PhotonNetwork.Instantiate(Path.Combine("Prefabs", "PhotonPrefabs", "PlayerController"), SpawnPlayer.Instance.GetSpawnPoint(), Quaternion.identity);
    }
}
