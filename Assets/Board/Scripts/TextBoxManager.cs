using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextBoxManager : MonoBehaviour
{
    public TextMeshProUGUI textBox;

    private bool _initInProgress;
    private int _index;

    private float _timer;

    [SerializeField] private List<string> _TextList;
    [SerializeField] private List<float> _Durations;

    public void Init()
    {
        _initInProgress = true;
        _index = 0;
        _timer = 0f;
        DisplayNextText();
    }

    private void Update()
    {
        _timer += Time.deltaTime;

        if(_index == _TextList.Count)
            GameManager.instance.GameInitialized();
        else
        {
            if (_timer > _Durations[_index])
                DisplayNextText();
        }
    }

    private void DisplayNextText()
    {
        _timer = 0;
        textBox.text = _TextList[_index];
        _index++;
    }
}
