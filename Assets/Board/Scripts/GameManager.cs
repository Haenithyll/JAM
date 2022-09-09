using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private BoardManager _BoardManager;
    [SerializeField] private CharacterManager _CharManager;
    [SerializeField] private HealthBarManager _HealthManager;

    [SerializeField] private CanvasGroup _Fade;

    public static GameManager instance;

    private float timer;
    private float _fadeTime;

    private bool _bossTile;
    private bool _gameOver;
    private bool _sceneTransition;

    private Tween fadeTween;

    private void Awake()
    {
        instance = this;
        _bossTile = false;
        _gameOver = false;
        _sceneTransition = false;

        _BoardManager.InitBoard(0);
        _CharManager.InitCharacter();
        _HealthManager.Init();
    }

    private void Update()
    {
        if (_sceneTransition)
        {
            if (timer < _fadeTime)
                timer += Time.deltaTime;
            else
                SceneManager.LoadScene("Battle");
        }
        else if (_bossTile)
            FadeOut();
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            Vector3 destinationPos = _BoardManager.GetDestinationTilePosition();
            _CharManager.MoveCharacter(destinationPos);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
            _HealthManager.RemoveHalfHeart();
        else if (Input.GetKeyDown(KeyCode.RightArrow))
            _HealthManager.AddHalfHeart();
    }

    public void QueueBoss()
    {
        _bossTile = true;
    }
    public void QueueGameOver()
    {
        _gameOver = true;
    }

    private void FadeOut()
    {
        float fadeTime = 2f;
        fadeTween = _Fade.DOFade(1, fadeTime);
        QueueNextScene(fadeTime);
    }

    private void QueueNextScene(float fadeTime)
    {
        _sceneTransition = true;
        _fadeTime = fadeTime;
        timer = Time.deltaTime;
        timer = 0f;
    }
}
