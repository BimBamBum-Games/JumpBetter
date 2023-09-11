using DG.Tweening;
using ES3Types;
using UnityEngine;

public class ExternalGravityChanger : MonoBehaviour
{
    [SerializeField] Transform _planet;
    [SerializeField] Transform _satellite;
    
    [field: SerializeField] public Vector3 DistVec { get; set; } = Vector3.zero;
    [field: SerializeField] public Vector3 DistUnitVec { get; set; } = Vector3.zero;
    [field: SerializeField] public float DistMag { get; set; } = float.MinValue;

    private Sequence _seq0;
    private Tween _twn0, _twn1;
    private int _seqOnn = 0;
    private void Start()
    {

        _seq0 = DOTween.Sequence(); 

        _twn0 = transform.DOMoveZ(20f, 5f).SetLoops(int.MaxValue, LoopType.Yoyo);
        //_twn1 = transform.DORotate(new Vector3(100, 0, 0), 2f).SetLoops(int.MaxValue, LoopType.Yoyo);

        _seq0.Append(_twn0);
    }

    private void Update()
    {
        DistVec = _satellite.position - _planet.position;
        DistMag = DistVec.magnitude;
        DistUnitVec = DistVec.normalized;
        if (Input.GetMouseButtonDown(1))
        {
            _seqOnn++;
            if(_seqOnn % 2 == 0)
            {
                _seq0.Pause();
            }
            else
            {
                _seq0.Play();
            }          
        }
    }
}
