using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ButtonManagerPause : ButtonManager
{
    public Animator anim;

    void Start () 
    {
        //make it not be affected by Time.Timescale = 0
        anim.updateMode = AnimatorUpdateMode.UnscaledTime;
        anim.SetBool("Intro", true);
    }
        
    public new void UnloadScene(string scene)
    {
        StartCoroutine("Outro", scene);
    }

    IEnumerator Outro(string scene)
    {
        Time.timeScale = 1f;
        AudioListener.volume = 1;
        _IsPauseActive = false;
        anim.SetBool("Outro", true);
        //get the animation length of the Outro animation
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);
        SceneManager.UnloadScene(scene);
        yield return null;
    }
}
    