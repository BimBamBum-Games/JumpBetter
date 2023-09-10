using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetterJumper : MonoBehaviour
{
    public float jumpSpeed;
    private Rigidbody _rb;
    [SerializeField] private Transform _core;
    [SerializeField] float _fallDownMultiplier = 2.5f;
    [SerializeField] float _riseUpMultiplier = 2f;
    private void Start()
    {
        //Baslangicta gravity ek olarak varsayilan etkiyi yapmaktadir. Kapali degildir.
        _rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        //Mouse ile tiklandiginda rigidbody velocity property belirlenen hiza isaretlenir.
        if (Input.GetMouseButtonDown(0))
        {
            _rb.velocity = Vector3.up * jumpSpeed;
        }

        //Eger tepeye cikilmissa y eksenindeki hizi sifirdan kucuk olur.
        if(_rb.velocity.y < 0)
        {
            _rb.velocity += Vector3.up * Physics.gravity.y * _fallDownMultiplier * Time.deltaTime;
        }
        //Eger y eksenindeki hizi sifirdan buyukse ek olarak yapay gravity eklenilebilir.
        else if(_rb.velocity.y > 0)
        {
            _rb.velocity += Vector3.up * Physics.gravity.y * _riseUpMultiplier * Time.deltaTime;
        }

    }
}
