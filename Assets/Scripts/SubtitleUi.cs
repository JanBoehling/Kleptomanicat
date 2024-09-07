using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class SubtitleUi : MonoBehaviour
{
    enum DisplayState
    {
        Hidden, WriteText, SkipTextDisplay, WaitForInput
    }

    [SerializeField] private float _timeBetweenChars = 0.1f;
    [SerializeField] private TextMeshProUGUI _displayTmp;
    [SerializeField] private Canvas _subtitleCanvas;

    private string _wannaDisplayText;
    private int _displayTextIndex = 0;
    private float _betweenCharTimer = 0;
    private DisplayState _displayState = DisplayState.Hidden;

    private void Update()
    {
        if (_displayState == DisplayState.Hidden || _displayState == DisplayState.WaitForInput)
            return;
        
        if(_displayState == DisplayState.SkipTextDisplay)
        {
            _displayTmp.text = _wannaDisplayText;
            _displayTextIndex = _wannaDisplayText.Length;

            _displayState = DisplayState.WaitForInput;

            return;
        }

        _betweenCharTimer += Time.deltaTime;
        if (_betweenCharTimer < _timeBetweenChars)
            return;

        _betweenCharTimer = 0;
        _displayTextIndex++;
        _displayTmp.text = _wannaDisplayText.Substring(0, _displayTextIndex);

        if (_displayTextIndex == _wannaDisplayText.Length)
            _displayState = DisplayState.WaitForInput;
    }

    [ContextMenu("Test Text")]
    public void TestText()
    {
        StartDisplayText("Dies ist ein Test Text... zum testen und so");
    }

    public void StartDisplayText(string text)
    {
        _displayTmp.text = "";
        _subtitleCanvas.enabled = true;
        _wannaDisplayText = text;
        _displayTextIndex = 0;
        _betweenCharTimer = 0;
        _displayState = DisplayState.WriteText;
    }

    private void HideText()
    {
        _subtitleCanvas.enabled = false;
        _displayState = DisplayState.Hidden;
    }

    public void GiveInput(CallbackContext ctx)
    {
        if (ctx.ReadValue<float>() <= 0)
            return;

        switch (_displayState)
        {
            case DisplayState.WriteText:
                _displayState = DisplayState.SkipTextDisplay;
                break;
            case DisplayState.WaitForInput:
                HideText();
                break;
        }
    }
}
