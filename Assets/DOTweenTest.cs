//using UnityEngine;
//using DG.Tweening;
//
//public class DOTweenTest : MonoBehaviour
//{
//
//    void Start()
//    {
//        this.transform.DOMove(Vector3.forward, 100.0f ).SetUpdate(UpdateType.Fixed).OnUpdate( DOTweenUpdate );
//    }
//
//    void DOTweenUpdate()
//    {
//        Debug.Log(string.Format("D t={0:00.000} dt={1:0.000}", Time.time, Time.deltaTime));
//    }
//
//    void Update ()
//    {
//        Debug.Log(string.Format("U t={0:00.000} dt={1:0.000}", Time.time, Time.deltaTime));
//    }
//
//    void FixedUpdate()
//    {
//        Debug.Log(string.Format("F t={0:00.000} dt={1:0.000}", Time.time, Time.deltaTime));
//    }
//}