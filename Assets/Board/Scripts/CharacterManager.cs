using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class CharacterManager : MonoBehaviour
{
    [SerializeField] private Canvas _prefab;
    [SerializeField] private Vector3 _startingPosition;
    [SerializeField] private List<Sprite> _CharacterSprites;

    [HideInInspector] public Vector3 destinationPosition;

    private bool _characterMoving;
    private float _timer;
    private float _animTimer;
    private float _movingDuration;

    private int _spriteIndex;

    private GameObject _characterInstance;
    public void InitCharacter()
    {
        _characterInstance = Instantiate(_prefab.gameObject);
        _characterInstance.transform.position = _startingPosition;
        _characterMoving = false;
        _spriteIndex = 0;
    }

    public void MoveCharacter(Vector3 destinationPosition)
    {
        _movingDuration = 1f;
        _characterInstance.transform.DOMove(new Vector3(0, destinationPosition.y, _startingPosition.z), _movingDuration);
        _timer = Time.deltaTime;
        _timer = 0f;
        _animTimer = 0f;
        _characterMoving = true;
    }

    private void Update()
    {
        if (_characterMoving)
        {
            if (_timer < _movingDuration)
            {
                _timer += Time.deltaTime;
                _animTimer += Time.deltaTime;

                if(_animTimer>_movingDuration / 10)
                {
                    _animTimer = 0f;
                    _spriteIndex++;
                    _characterInstance.GetComponentInChildren<Image>().sprite = _CharacterSprites[_spriteIndex%3];
                }
            }
            else
                _characterMoving = false;
        }
    }
}
