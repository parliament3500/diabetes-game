using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    private GameManager _gameManager;
    public static bool _IsPauseActive;

    void Start()
    {
        
        //creates GameManager if it doesn't exist
        if(GameObject.Find("GameManager") == null) {
            new GameObject("GameManager").AddComponent<GameManager>();
        } else {
            _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();      
        }
    }
    //loads additive scene
    public void AddScene(string scene)
    {
        if (_IsPauseActive == false)
        {
            AudioListener.volume = 0;
            SceneManager.LoadScene(scene, LoadSceneMode.Additive);
            Time.timeScale = 0f;
            _IsPauseActive = true;
        }
    }

    public void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public virtual void UnloadScene(string scene)
    {
        AudioListener.volume = 1;
        _IsPauseActive = false;
        SceneManager.UnloadScene(scene);
        Time.timeScale = 1f;
    }

    public void ResetGameScene(string scene)
    {
        AudioListener.volume = 1;
        SceneManager.UnloadScene(scene);
        _gameManager.ResetGame();
        _IsPauseActive = false;
    }

    public void ResetGameScene()
    {
        AudioListener.volume = 1;
        _gameManager.ResetGame();
        _IsPauseActive = false;
    }

    public void ErasePlayerprefs()
    {
        PlayerPrefs.DeleteAll();
        Timer.bestTime = 0f;
    }
}
