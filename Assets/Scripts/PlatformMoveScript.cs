using UnityEngine;
using System.Collections;
using DG.Tweening;

public class PlatformMoveScript : MonoBehaviour 
{
    public Transform endPosition;
    public enum AnimationType {MoveToPos, MoveToPosAndStop};
    public AnimationType aniType = AnimationType.MoveToPos;
    public float duration;
    private Transform _current;

    void Start () 
    {
        _current = GetComponent<Transform>();
        if(aniType == AnimationType.MoveToPos)
            MoveToPos();
    }

    void MoveToPos()
    {
        _current.DOMove(endPosition.position, duration, true).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo).SetDelay(0f);
    }

}
