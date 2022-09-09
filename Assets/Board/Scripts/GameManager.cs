using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private BoardManager _BoardManager;
    [SerializeField] private CharacterManager _CharManager;
    [SerializeField] private HealthBarManager _HealthManager;
    [SerializeField] private TextBoxManager _TextBoxManager;

    [SerializeField] private CanvasGroup _Fade;
    [SerializeField] private CanvasGroup _UI;

    [SerializeField] private List<Sprite> _DiceSprites;
    [SerializeField] private Sprite _defaultDiceSprite;
    [SerializeField] private Image _dice;

    public static GameManager instance;

    private float _timer;
    private float _rollTimer;

    private float _fadeTime;
    private float _rollingTime;

    private bool _bossTile;
    private bool _gameOver;
    private bool _sceneTransition;
    private bool _diceRolling;
    private bool _isMoving;
    private bool _gameInitialized;

    private int _diceValue;
    private int _index;

    private Tween fadeTween;

    private void Awake()
    {
        instance = this;
        _bossTile = false;
        _gameOver = false;
        _sceneTransition = false;
        _diceRolling = false;
        _isMoving = false;
        _gameInitialized = false;

        _BoardManager.InitBoard(0);
        _CharManager.InitCharacter();
        _HealthManager.Init();
        _TextBoxManager.Init();
    }

    private void Update()
    {
        if (!_gameInitialized)
            return;
        else if (_sceneTransition)
        {
            if (_timer < _fadeTime)
                _timer += Time.deltaTime;
            else if (!CharacterManager.instance.characterMoving)
                SceneManager.LoadScene("Battle");
        }
        else if (_bossTile)
            FadeOut();
        else if (_diceRolling)
        {
            _timer += Time.deltaTime;
            _rollTimer += Time.deltaTime;

            if (_timer > _rollingTime)
            {
                _diceRolling = false;
                _diceValue = (_index % 4) + 1;
                //_dice.sprite = _defaultDiceSprite;
                _isMoving = true;
            }
            else if (_rollTimer > .1f)
            {
                _rollTimer = 0f;
                _index++;
                _dice.sprite = _DiceSprites[_index % 4];
            }
        }
        else if (_isMoving)
        {
            if (_diceValue > 0)
            {
                Vector3 destinationPos = _BoardManager.GetDestinationTilePosition();
                _CharManager.MoveCharacter(destinationPos);
            }

            _diceValue--;

            if (_diceValue < 1 && !CharacterManager.instance.characterMoving)
            {
                _isMoving = false;
                _BoardManager.DisplayText(_TextBoxManager.textBox);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            RollDice();
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
            _HealthManager.RemoveHalfHeart();
        else if (Input.GetKeyDown(KeyCode.RightArrow))
            _HealthManager.AddHalfHeart();
    }

    private void RollDice()
    {
        _diceRolling = true;
        _rollingTime = Random.Range(1f,3f);
        _timer = Time.deltaTime;
        _timer = 0f;
        _rollTimer = 0f;
        _index = 0;
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
        fadeTween = _UI.DOFade(0, .5f);
        fadeTween = _Fade.DOFade(1, fadeTime);
        QueueNextScene(fadeTime);
    }

    private void QueueNextScene(float fadeTime)
    {
        _sceneTransition = true;
        _fadeTime = fadeTime;
        _timer = Time.deltaTime;
        _timer = 0f;
    }

    public void GameInitialized()
    {
        _gameInitialized = true;
    }

    public void AddHealth()
    {
        _HealthManager.AddHalfHeart();
    }
    public void RemoveHealth()
    {
        _HealthManager.RemoveHalfHeart();
    }
}
