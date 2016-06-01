using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;

public class InsulinEffect : MonoBehaviour
{
    public int insulinEffect;
    private ParticleSystem _particlePickup;

    private Tweener _shakeIt;
    private GameObject _camera;
    private SoundManager _soundManager;
    public static bool damaged;

    void Start()
    {
        _particlePickup = Resources.Load<ParticleSystem>("Prefabs/PickUpParticle");
        if(GameObject.FindGameObjectWithTag("MainCamera") != null)
        _camera = GameObject.FindGameObjectWithTag("MainCamera");
        if(GameObject.FindGameObjectWithTag("Sound") != null)
        _soundManager = GameObject.FindGameObjectWithTag("Sound").GetComponent<SoundManager>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            damaged = true;
            _soundManager.audioInsulinHit();
            StatManager.ToxinReduce(insulinEffect);    
            _shakeIt = _camera.transform.DOShakePosition(1f, 6f, 10).SetAutoKill(true);
            StartCoroutine("DestroySequence");
        }
    }

    IEnumerator DestroySequence()
    {
        Instantiate(_particlePickup, transform.position, Quaternion.identity);
        //Destroy(this.transform.parent.gameObject);
        Destroy(this.gameObject);
        yield return null;
    }
}
