using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnPlayer : MonoBehaviourSingletonPersistent<SpawnPlayer>
{
    [SerializeField] private PhotonView _photonView;
    [SerializeField] private Transform[] _spawnPoints;

    protected override void Awake()
    {
        base.Awake();
        _photonView = GetComponent<PhotonView>();
    }

    public Vector3 GetSpawnPoint()
    {
        int rand = Random.Range(0, _spawnPoints.Length);
        return _spawnPoints[rand].position;
    }
}
