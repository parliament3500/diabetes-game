using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private ShowGameend _deathText;
    private Spawn _spawn;
    private StatManager _stat;
    private GameObject _restartButton, _player;
    private ParticleSystem _deathParticle;
    private Text _gameOverText;
    private float _timeshift = 1;
    private Player _playerScript;
    private Animator _anim;
    private SoundManager _soundManager;

    void Start()
    {
        if(SceneManager.GetActiveScene().name == "GamePlay")
        {
            _soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
            _anim = GameObject.Find("death").GetComponent<Animator>();
            _anim.updateMode = AnimatorUpdateMode.UnscaledTime;
            StatManager.death += GameOver;
            _deathParticle = Resources.Load<ParticleSystem>("Prefabs/DeathParticle");
            _deathText = GameObject.Find("StatManager").GetComponent<ShowGameend>();
            _spawn = GameObject.Find("Spawner").GetComponent<Spawn>();
            _stat = GameObject.Find("StatManager").GetComponent<StatManager>();
            _player = GameObject.Find("player");
            _playerScript = GameObject.Find("player").GetComponent<Player>();
            _restartButton = GameObject.Find("RestartButton");
            _spawn.ResetDelay();
            _restartButton.SetActive(false);
            _spawn.StartSpawning();
        }
    }

    void GameOver()
    {
        _soundManager.GameoverSound();
        _playerScript.Jump();
        Player.alive = false;
        Player.IsGameoverTextVisible = true;
        Player._anim.SetBool("IsRunning", false);
        _player.transform.Rotate(0, 0, 90);
        Instantiate(_deathParticle, _player.transform.position, Quaternion.identity);
        _restartButton.SetActive(true);
        _deathText.EnableText();
        _anim.SetBool("Intro", true);
        _spawn.StopSpawning();
        StartCoroutine(Slowspeed(5f));
        StatManager.death -= GameOver;
    }

    public void ResetGame()
    {
        _spawn.StopSpawning();
        _spawn.gbListDestroy();
        _spawn.ResetDelay();
        _spawn.StartSpawning();

        _anim.Play ("EmptyState");
        _deathText.DisableText();
        _stat.ToxinHalf();
        _stat.HungerHalf();
        Time.timeScale = 1;
        Timer.timeElapsed = 0f;
        Timer.newHighscore = false;
        _restartButton.SetActive(false);
        Player.alive = true;
        _player.transform.position = Vector2.zero;
        _player.transform.rotation = Quaternion.identity;

        StatManager.death += GameOver;
    }

    IEnumerator Slowspeed(float fadeSpeed)
    {
        float realtime = 1;
        _timeshift = Mathf.Clamp01(_timeshift);
        while (Time.timeScale >= 0.2)
        {
            _timeshift -= realtime * fadeSpeed * Time.deltaTime;
            Time.timeScale = _timeshift;
            //Debug.Log(_timeshift);
            yield return null;
        }
        _timeshift = 1;

    }
}
