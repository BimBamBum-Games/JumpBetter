using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetterJumper : MonoBehaviour
{
    private Rigidbody _rb;
    [SerializeField] float jumpSpeed;
    [SerializeField] Transform _core;
    [SerializeField] float _fallDownMultiplier = 2.5f;
    [SerializeField] float _riseUpMultiplier = 2f;
    private void Start()
    {
        //Baslangicta gravity ek olarak varsayilan etkiyi yapmaktadir. Kapali degildir.
        _rb = GetComponent<Rigidbody>();

        //X, Y ve Z rotasyonlarini kisitlanir.
        _rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
    }
    private void Update()
    {
        //Mouse ile tiklandiginda rigidbody velocity property belirlenen hiza isaretlenir. Ziplama icin izin verilir.
        if (Input.GetMouseButtonDown(0))
        {
            _rb.velocity = Vector3.up * jumpSpeed;
            IsGrounded = false;
        }

        //Eger tepeye cikilmissa y eksenindeki hizi sifirdan kucuk olur.
        if (_rb.velocity.y < 0)
        {
            _rb.velocity += Vector3.up * Physics.gravity.y * _fallDownMultiplier * Time.deltaTime;
        }
        //Eger y eksenindeki hizi sifirdan buyukse ek olarak yapay gravity eklenilebilir.
        else if (_rb.velocity.y > 0)
        {
            _rb.velocity += Vector3.up * Physics.gravity.y * _riseUpMultiplier * Time.deltaTime;
        }

        //Zemine varildiysa artik hizi sifirlar ve vektorel olarak velocity property uzerinde isaretlenir.
        if(IsGrounded == true)
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