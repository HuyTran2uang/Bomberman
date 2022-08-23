using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PhotonView _photonView;
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private Transform _pointPutBomb;
    private bool _once;
    //data
    public int NumberBomb;
    public int ForceExplode;
    public float Speed;
    public GameObject Bomb;

    private void Awake()
    {
        _photonView = GetComponent<PhotonView>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _pointPutBomb = transform.Find("PointPutBomb");
        Bomb = Resources.Load<GameObject>("Prefabs/Bomb/BlackBomb");
    }

    private void Start()
    {
        NumberBomb = 1;
        ForceExplode = 1;
        Speed = 0.3f;
        Setup();
    }

    public Rigidbody2D Rigidbody2D
    {
        get { return _rigidbody2D; }
    }

    public Animator Animator
    {
        get { return transform.Find("Model").GetChild(0).GetComponent<Animator>(); }
    }

    public Transform PointPutBomb
    {
        get { return _pointPutBomb; }
    }

    private void Update()
    {
        if (!_photonView.IsMine) return;
        Move();
        if (Input.GetKeyDown(KeyCode.J))
        {
            if (NumberBomb <= 0) return;
            if (MapManager.Instance.CheckBombPlaced(PointPutBomb.position)) return;
            _photonView.RPC("RPC_PutBoom", RpcTarget.All);
            --NumberBomb;
        }
    }

    [PunRPC]
    private void RPC_PutBoom()
    {
        Vector3Int cell = MapManager.Instance.Tilemap.WorldToCell(PointPutBomb.position);
        Vector3 posCenter = MapManager.Instance.Tilemap.GetCellCenterWorld(cell);
        GameObject bombClone = Instantiate(Bomb, posCenter, Quaternion.identity);
        bombClone.GetComponent<BombController>().PV = _photonView;
        _once = true;
        MapManager.Instance.SetBombPlaced(posCenter, true);
    }

    #region Movement
    private Vector2 _direction;
    private Vector2 _lastDirection;
    private bool _isMoving;

    public void Move()
    {
        _direction.x = Input.GetAxisRaw("Horizontal");
        _direction.y = Input.GetAxisRaw("Vertical");
        RemoveDiagonalMovement();
        _lastDirection = _direction.magnitude == 1 ? _direction : _lastDirection;
        _isMoving = _direction != Vector2.zero ? true : false;

        _rigidbody2D.velocity = _direction * Speed;
        ChangeAnimationState();
    }

    private void RemoveDiagonalMovement()
    {
        _direction.x = _direction.y != 0 ? 0 : _direction.x;
        _direction.y = _direction.x != 0 ? 0 : _direction.y;
    }

    private void ChangeAnimationState()
    {
        Animator.SetFloat("moveX", _direction.x);
        Animator.SetFloat("moveY", _direction.y);
        Animator.SetFloat("lastMoveX", _lastDirection.x);
        Animator.SetFloat("lastMoveY", _lastDirection.y);
        Animator.SetBool("isMoving", _isMoving);
    }
    #endregion

    #region Sensor
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == 7)
        {
            if (other.gameObject.tag == "BombItem")
            {
                NumberBomb++;
                Destroy(other.gameObject);
            }

            if (other.gameObject.tag == "GunpowderItem")
            {
                ForceExplode++;
                Destroy(other.gameObject);
            }

            if (other.gameObject.tag == "SoftDrinkItem")
            {
                Speed += 0.1f;
                Destroy(other.gameObject);
            }
        }
        // if (other.gameObject.layer == 8)
        // {
        //     other.gameObject.GetComponent<Collider2D>().isTrigger = true;
        // }
        //collision explode
        if (other.gameObject.layer == 9)
        {
            Dead();
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.layer == 7)
        {
            if (other.gameObject.tag == "BombItem")
            {
                NumberBomb++;
                Destroy(other.gameObject);
            }

            if (other.gameObject.tag == "GunpowderItem")
            {
                ForceExplode++;
                Destroy(other.gameObject);
            }

            if (other.gameObject.tag == "SoftDrinkItem")
            {
                Speed += 0.1f;
                Destroy(other.gameObject);
            }
        }
        if (other.gameObject.layer == 8)
        {
            if (!_once) return;
            _once = false;
            other.gameObject.GetComponent<Collider2D>().isTrigger = true;
        }
        //collision explode
        if (other.gameObject.layer == 9)
        {
            Dead();
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.layer == 8)
        {
            other.gameObject.GetComponent<Collider2D>().isTrigger = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 7)
        {
            if (other.gameObject.tag == "BombItem")
            {
                NumberBomb++;
                Destroy(other.gameObject);
            }

            if (other.gameObject.tag == "GunpowderItem")
            {
                ForceExplode++;
                Destroy(other.gameObject);
            }

            if (other.gameObject.tag == "SoftDrinkItem")
            {
                Speed += 0.1f;
                Destroy(other.gameObject);
            }
        }
        // if (other.gameObject.layer == 8)
        // {
        //     other.gameObject.GetComponent<Collider2D>().isTrigger = true;
        // }
        //collision explode
        if (other.gameObject.layer == 9)
        {
            Dead();
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.layer == 7)
        {
            if (other.gameObject.tag == "BombItem")
            {
                NumberBomb++;
                Destroy(other.gameObject);
            }

            if (other.gameObject.tag == "GunpowderItem")
            {
                ForceExplode++;
                Destroy(other.gameObject);
            }

            if (other.gameObject.tag == "SoftDrinkItem")
            {
                Speed += 0.1f;
                Destroy(other.gameObject);
            }
        }
        // if (other.gameObject.layer == 8)
        // {
        //     other.gameObject.GetComponent<Collider2D>().isTrigger = true;
        // }
        //collision explode
        if (other.gameObject.layer == 9)
        {
            Dead();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == 8)
        {
            other.gameObject.GetComponent<Collider2D>().isTrigger = false;
        }
    }
    #endregion

    private void Dead()
    {
        gameObject.SetActive(false);
    }

    #region Player UI
    [SerializeField] private Text _nameText;

    public void Setup()
    {
        _nameText.text = _photonView.Owner.NickName;
    }
    #endregion
}
