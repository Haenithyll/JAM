using DG.Tweening;
using UnityEngine;

public class CardGameObject : MonoBehaviour
{
    public Card CardData { get => _card; }
    public Vector3 ShouldBePosition { get => _shouldBePosition; }
    
    [SerializeField] private MeshRenderer _pictureRenderer;

    private Card _card;
    private Vector3 _shouldBePosition;
    
    private readonly float _moveDuration = 0.8f;

    public void Init(Card cardData)
    {
        _card = cardData;
        _pictureRenderer.material.color = cardData.Color;
    }

    public void Move(Vector3 destination)
    {
        transform.DOKill();
        transform.DORotate(new Vector3(0.0f, 0.0f, 0.0f), 0.5f).SetEase(Ease.OutCubic);
        transform.DOMove(destination, _moveDuration).SetEase(Ease.OutCubic);
        _shouldBePosition = destination;
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

    public void LittleDance()
    {
        transform.DORotate(new Vector3(0.0f, 360.0f, 0.0f), 0.5f, RotateMode.FastBeyond360).SetEase(Ease.OutCubic);
    }
}
