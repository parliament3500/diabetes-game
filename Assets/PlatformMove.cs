using UnityEngine;
using System.Collections;
using DG.Tweening;

public class PlatformMove : MonoBehaviour 
{
    public Transform endPosition;
    public enum AnimationType {MoveToPos, MoveToPosAndStop};
    public AnimationType aniType = AnimationType.MoveToPos;
    public float duration;
    private Tweener _platformMover;
    private Transform _current;
    private bool _isOnTop;

	void Start () 
	{
        _current = GetComponent<Transform>();
        if(aniType == AnimationType.MoveToPos)
            MoveToPos();
    }
        
    void MoveToPos()
    {
        _current.DOMove(endPosition.position, duration, false).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
    }

    void OnTriggerStay2D (Collider2D col)
    {
        if(col.tag == "Player" || col.tag == "Zombie" || col.tag == "Insulin")
        col.transform.parent = this.transform;
    }
    void OnTriggerExit2D (Collider2D col)
    {
            col.transform.parent = null;
    }
}