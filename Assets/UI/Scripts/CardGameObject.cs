using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CardGameObject : MonoBehaviour
{
    [SerializeField] private MeshRenderer _pictureRenderer;

    private Card _card;
    private readonly float _moveDuration = 0.8f;

    public void Init(Card cardData)
    {
        _pictureRenderer.material.color = cardData.Color;
    }

    void Move(Vector3 destination)
    {
        transform.DOKill();
        transform.DOMove(destination, _moveDuration).SetEase(Ease.OutCubic);
    }

    public void MoveGrow(Vector3 destination)
    {
        transform.localScale = Vector3.zero;
        Move(destination);
        transform.DOScale(1.0f, _moveDuration);
    }

    public void MoveShrink(Vector3 destination, bool destroy)
    {
        if (destroy)
            Destroy(gameObject, _moveDuration);
        transform.localScale = Vector3.one;
        Move(destination);
        transform.DOScale(0.0f, _moveDuration);
    }
}
