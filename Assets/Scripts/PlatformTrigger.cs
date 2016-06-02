using UnityEngine;
using System.Collections;
using DG.Tweening;

public class PlatformTrigger : MonoBehaviour 
{
    void OnTriggerStay2D (Collider2D col)
    {
        if(col.tag == "Player" || col.tag == "Zombie" || col.tag == "Insulin")
            col.transform.parent = this.transform;
        //this.transform.SetParent(col.transform,false);
    }
    void OnTriggerExit2D (Collider2D col)
    {
        col.transform.parent = null;
    }
}