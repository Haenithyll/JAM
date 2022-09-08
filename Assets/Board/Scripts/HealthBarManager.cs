using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarManager : MonoBehaviour
{
    [SerializeField] private List<Sprite> _Sprites;
    [SerializeField] private Image _firstHeart;
    [SerializeField] private Image _secondHeart;
    [SerializeField] private Image _thirdHeart;

    public int _currentHealth;

    public void Init()
    {
        _currentHealth = 6;
    }

    public void RemoveHalfHeart()
    {
        _currentHealth--;

        switch (_currentHealth)
        {
            case 0:
                GameManager.instance.QueueGameOver();
                _firstHeart.sprite = _Sprites[0];
                break;
            case 1:
                _firstHeart.sprite = _Sprites[1];
                break;
            case 2:
                _secondHeart.sprite = _Sprites[0];
                break;
            case 3:
                _secondHeart.sprite = _Sprites[1];
                break;
            case 4:
                _thirdHeart.sprite = _Sprites[0];
                break;
            case 5:
                _thirdHeart.sprite = _Sprites[1];
                break;
        }
    }

    public void AddHalfHeart()
    {
        if (_currentHealth == 6)
            return;

        _currentHealth++;

        switch (_currentHealth)
        {
            case 0:
                GameManager.instance.QueueGameOver();
                _firstHeart.sprite = _Sprites[0];
                break;
            case 1:
                _firstHeart.sprite = _Sprites[1];
                break;
            case 2:
                _firstHeart.sprite = _Sprites[2];
                break;
            case 3:
                _secondHeart.sprite = _Sprites[1];
                break;
            case 4:
                _secondHeart.sprite = _Sprites[2];
                break;
            case 5:
                _thirdHeart.sprite = _Sprites[1];
                break;
            case 6:
                _thirdHeart.sprite = _Sprites[2];
                break;
        }
    }
}
