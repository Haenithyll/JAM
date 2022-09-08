using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class TitleScreen : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private RectTransform _logo;
    [SerializeField] private CanvasGroup _logoCanvasGroup;
    [SerializeField] private RectTransform[] _buttons;
    [SerializeField] private Image _blackscreenImage;
    [Header("Parameters")]
    [SerializeField] private float _fadeLength;
    [SerializeField] private float _fadeDelay;
    [SerializeField] private Vector2 _fadeComingFrom;

    private List<CanvasGroup> _buttonsCanvasGroups = new List<CanvasGroup>();

    private bool _interactable = false;
    
    void Start()
    {
        Color blackscreenColor = _blackscreenImage.color;
        blackscreenColor.a = 1.0f;
        _blackscreenImage.color = blackscreenColor;
        _logo.anchoredPosition += _fadeComingFrom;
        _logoCanvasGroup.alpha = 0.0f;
        for (int i = 0; i < _buttons.Length; i++)
        {
            RectTransform button = _buttons[i];
            button.anchoredPosition += _fadeComingFrom;
            CanvasGroup canvasGroup = button.GetComponent<CanvasGroup>();
            _buttonsCanvasGroups.Add(canvasGroup);
            canvasGroup.alpha = 0.0f;
        }
        
        StartCoroutine(ShowTitleScreen());
    }

    IEnumerator ShowTitleScreen()
    {
        _blackscreenImage.DOFade(0.0f, 0.3f);
        yield return new WaitForSeconds(0.3f);
        _logo.DOAnchorPos(new Vector2(_logo.anchoredPosition.x - _fadeComingFrom.x, _logo.anchoredPosition.y - _fadeComingFrom.y), _fadeLength);
        _logoCanvasGroup.DOFade(1.0f, _fadeLength);
        yield return new WaitForSeconds(_fadeDelay);
        for (int i = 0; i < _buttons.Length; i++)
        {
            RectTransform button = _buttons[i];
            button.DOAnchorPos(new Vector2(button.anchoredPosition.x - _fadeComingFrom.x, button.anchoredPosition.y - _fadeComingFrom.y), _fadeLength);
            _buttonsCanvasGroups[i].DOFade(1.0f, _fadeLength);
            yield return new WaitForSeconds(i == _buttons.Length - 1 ? _fadeLength : _fadeDelay);
        }
        _interactable = true;
    }

    IEnumerator HideTitleScreen()
    {
        _interactable = false;
        _blackscreenImage.DOFade(1.0f, 0.3f);
        yield return new WaitForSeconds(0.4f);
        #if UNITY_EDITOR
        EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }

    public void ClickQuit()
    {
        if (_interactable == false) return;
        StartCoroutine(HideTitleScreen());
    }
}
