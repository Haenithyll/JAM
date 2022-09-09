using DG.Tweening;
using TMPro;
using UnityEngine;

public class CardGameObject : MonoBehaviour //
{
    public CardsAttributes Attributes { get => _attributes; }
    public Vector3 ShouldBePosition { get => _shouldBePosition; }
    
    [SerializeField] private MeshRenderer _pictureRenderer;
    [SerializeField] private TMP_Text _descriptionText;

    private CardsAttributes _attributes;
    private Vector3 _shouldBePosition;
    
    private readonly float _moveDuration = 0.8f;

    public void Init(CardsAttributes cardData)
    {
        _attributes = cardData;
        if (cardData.CardImage != null)
            _pictureRenderer.material.mainTexture = _attributes.CardImage;
        _descriptionText.text = _attributes.CardText;
    }

    public void Move(Vector3 destination)
    {
        transform.DOKill();
        transform.DORotate(new Vector3(0.0f, 0.0f, 0.0f), 0.5f).SetEase(Ease.OutCubic);
        transform.DOMove(destination, _moveDuration).SetEase(Ease.OutCubic);
        transform.DORotateQuaternion(Quaternion.LookRotation(Camera.main.transform.position - destination, Camera.main.transform.up), 0.5f);
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
