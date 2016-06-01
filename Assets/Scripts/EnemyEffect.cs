using UnityEngine;
using System.Collections;
using DG.Tweening;

public class EnemyEffect : MonoBehaviour
{
    public static bool damaged;
    public int toxinIncreaseValue;
    public int HungerIncreaseValue;
    private ParticleSystem _particlePickup;
    private Tweener _shakeIt;
    private GameObject _camera;
    private bool _isCorountineRunning;
    private SoundManager _soundManager;
    private StatManager _statManager;


    void Start()
    {
        _particlePickup = Resources.Load<ParticleSystem>("Prefabs/PickUpParticle");
        if( GameObject.FindGameObjectWithTag("Sound") != null)
        _soundManager = GameObject.FindGameObjectWithTag("Sound").GetComponent<SoundManager>();
        if( GameObject.FindGameObjectWithTag("MainCamera") != null)
        _camera = GameObject.FindGameObjectWithTag("MainCamera");
        if( GameObject.Find("StatManager") != null)
        _statManager = GameObject.Find("StatManager").GetComponent<StatManager>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            damaged = true;
            _soundManager.audioEnemyHit();
            _statManager.ToxinIncrease(toxinIncreaseValue);    
            _statManager.HungerIncrease(HungerIncreaseValue); 
            _shakeIt = _camera.transform.DOShakePosition(1f, 6f, 10).SetAutoKill(true);
            StartCoroutine("DestroySequence");
        }
    }

    IEnumerator DestroySequence()
    {
        Instantiate(_particlePickup, transform.position, Quaternion.identity);
        gameObject.SetActive(false);
        yield return null;
    }
        
}
