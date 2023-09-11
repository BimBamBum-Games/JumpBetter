using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetterJumper : MonoBehaviour
{
    private Rigidbody _rb;
    [SerializeField] float jumpSpeed;
    [SerializeField] float _arbGravity = 10f;
    [SerializeField] float _fallDownMultiplier = 2.5f;
    [SerializeField] float _riseUpMultiplier = 2f;
    [SerializeField] ExternalGravityChanger _gravityChanger;
    private void Start()
    {
        //Baslangicta gravity ek olarak varsayilan etkiyi yapmaktadir. Kapali degildir.
        _rb = GetComponent<Rigidbody>();

        //Physics.autoSyncTransforms = true;

        //X, Y ve Z rotasyonlarini kisitlanir.
        //_rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
    }

    [SerializeField] Vector3 _upVector = Vector3.zero;
    private void Update()
    {

        _upVector = _gravityChanger.DistUnitVec;

        //Mouse ile tiklandiginda rigidbody velocity property belirlenen hiza isaretlenir. Ziplama icin izin verilir.
        if (IsGrounded == true)
        {
            _rb.velocity = _upVector * jumpSpeed;
            IsGrounded = false;
        }

        Vector3 ngtVel = _upVector * _arbGravity * Time.deltaTime;

        //Eger tepeye cikilmissa y eksenindeki hizi sifirdan kucuk olur.
        if (_rb.velocity.magnitude <= 0)
        {
            _rb.velocity -= _upVector * _fallDownMultiplier * Time.deltaTime + ngtVel;/* * _arbGravity * Time.deltaTime;*/
        }
        //Eger y eksenindeki hizi sifirdan buyukse ek olarak yapay gravity eklenilebilir.
        else if (_rb.velocity.magnitude > 0)
        {
            _rb.velocity -= _upVector * _riseUpMultiplier * Time.deltaTime + ngtVel;
        }

        //_rb.velocity -= _upVector * _arbGravity * Time.deltaTime;

        //Zemine varildiysa artik hizi sifirlar ve vektorel olarak velocity property uzerinde isaretlenir.
        if (IsGrounded == true)
        {
            _rb.velocity = Vector3.zero;
        }


    }

    [field: SerializeField] bool IsGrounded { get; set; } = false;

    private void OnCollisionEnter(Collision collision)
    {   //Other gameobject must be Ground tagged.
        if (collision.gameObject.CompareTag("Ground"))
        {
            IsGrounded = true;
            Debug.LogWarning("Satellite Has Grounded!");
        }
    }
}