using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class BombController : MonoBehaviour
{
    private float _timeExplodeBomb;
    [SerializeField] private Collider2D _collider;
    [SerializeField] private PhotonView _pv;
    public PhotonView PV
    {
        get { return _pv; }
        set { _pv = value; }
    }

    public int ForceExplode
    {
        get { return PV.GetComponent<PlayerController>().ForceExplode; }
        set { PV.GetComponent<PlayerController>().ForceExplode = value; }
    }

    public int NumberBomb
    {
        get { return PV.GetComponent<PlayerController>().NumberBomb; }
        set { PV.GetComponent<PlayerController>().NumberBomb = value; }
    }

    private void OnEnable()
    {
        _timeExplodeBomb = 2f;
        _collider = GetComponent<Collider2D>();
    }

    private void FixedUpdate()
    {
        if (_timeExplodeBomb > 0) _timeExplodeBomb -= Time.deltaTime;
        if (_timeExplodeBomb <= 0)
        {
            Invoke("CompletedExplodeBomb", 0f);
        }
    }

    private void CompletedExplodeBomb()
    {
        Destroy(gameObject);
        MapManager.Instance.SetBombPlaced(gameObject.transform.position, false);
        MapDestroyer.Instance.ExplodeBomb(transform.position, ForceExplode);
        ++NumberBomb;
    }

    #region Sensor
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == 9)
        {
            Invoke("CompletedExplodeBomb", 0f);
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.layer == 9)
        {
            Invoke("CompletedExplodeBomb", 0f);
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.layer == 6)
        {
            _collider.isTrigger = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 9)
        {
            Invoke("CompletedExplodeBomb", 0f);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.layer == 9)
        {
            Invoke("CompletedExplodeBomb", 0f);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == 6)
        {
            _collider.isTrigger = false;
        }
    }
    #endregion
}
