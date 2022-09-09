using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class CharacterManager : MonoBehaviour
{
    public static CharacterManager instance;

    [SerializeField] private Canvas _prefab;
    [SerializeField] private Vector3 _startingPosition;
    [SerializeField] private List<Sprite> _CharacterSprites;

    [HideInInspector] public Vector3 destinationPosition;

    public bool characterMoving;
    private float _timer;
    private float _animTimer;
    private float _movingDuration;

    private int _spriteIndex;

    private GameObject _characterInstance;
    public void InitCharacter()
    {
        instance = this;
        _characterInstance = Instantiate(_prefab.gameObject);
        _characterInstance.transform.position = _startingPosition;
        characterMoving = false;
        _spriteIndex = 0;
    }

    public void MoveCharacter(Vector3 destinationPosition)
    {
        _movingDuration = 1f;
        _characterInstance.transform.DOMove(new Vector3(0, destinationPosition.y, _startingPosition.z), _movingDuration);
        _timer = Time.deltaTime;
        _timer = 0f;
        _animTimer = 0f;
        characterMoving = true;
    }

    private void Update()
    {
        if (characterMoving)
        {
            if (_timer < _movingDuration)
            {
                _timer += Time.deltaTime;
                _animTimer += Time.deltaTime;

                if (_animTimer > _movingDuration / 5)
                {
                    _animTimer = 0f;
                    _spriteIndex++;
                    _characterInstance.GetComponentInChildren<Image>().sprite = _CharacterSprites[(_spriteIndex % 2) + 1];
                }
            }
            else
            {
                characterMoving = false;
                _characterInstance.GetComponentInChildren<Image>().sprite = _CharacterSprites[0];
            }
        }
    }
}
