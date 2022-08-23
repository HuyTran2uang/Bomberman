using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using Photon.Pun;

public class RoomManager : MonoBehaviourPunCallbacksSingletonPersistent<RoomManager>
{
    public override void OnEnable()
    {
        base.OnEnable();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public override void OnDisable()
    {
        base.OnDisable();
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        if (scene.buildIndex == 0) return;
        PhotonNetwork.Instantiate(Path.Combine("Prefabs", "PhotonPrefabs", "PlayerManager"), Vector3.zero, Quaternion.identity);
    }
}
