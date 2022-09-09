using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CharacterManager : MonoBehaviour
{
    [SerializeField] private Canvas _prefab;
    [SerializeField] private Vector3 _startingPosition;

    [HideInInspector] public Vector3 destinationPosition;

    private GameObject _characterInstance;
    public void InitCharacter()
    {
        _characterInstance = Instantiate(_prefab.gameObject);
        _characterInstance.transform.position = _startingPosition;
    }

    public void MoveCharacter(Vector3 destinationPosition)
    {
        _characterInstance.transform.DOMove(new Vector3(0, destinationPosition.y, _startingPosition.z), 0.5f);
    }
}
