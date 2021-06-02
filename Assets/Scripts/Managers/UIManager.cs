using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [HideInInspector] public Action<Color> onColorSet;

    [SerializeField] private GameObject objectPanelMenu = default;
    [SerializeField] private TextMeshProUGUI labelTimer;
    [SerializeField] private TextMeshProUGUI labelScore;
    [SerializeField] private Button buttonMenu = default;
    [SerializeField] private Button buttonSet = default;
    [SerializeField] private Button buttonReset = default;
    [SerializeField] private Button buttonClose = default;
    [SerializeField] private TMP_InputField inputHexValue = default;

    private void Start()
    {
        objectPanelMenu.SetActive(false);

        buttonMenu.onClick.AddListener(MenuClicked);
        buttonSet.onClick.AddListener(SetColor);
        buttonReset.onClick.AddListener(ResetScore);
        buttonClose.onClick.AddListener(ClosePanel);
    }

    private void MenuClicked()
    {
        Time.timeScale = 0;
        objectPanelMenu.SetActive(true);
    }

    public void UpdateTimer(int value)
    {
        if (value > 0)
        {
            labelTimer.text = value.ToString();
        }
        else
        {
            labelTimer.text = string.Empty;
        }
    }

    private void ResetScore()
    {
        PlayerPrefs.SetInt(K.PrefKey.playerScore, 0);
        PlayerPrefs.SetInt(K.PrefKey.opponentScore, 0);
        GameManager.Instance.playerScore = 0;
        GameManager.Instance.opponentScore = 0;
        UpdateScore(0, 0);
    }

    public void UpdateScore(int playerScore, int opponentScore)
    {
        labelScore.text = $"{playerScore} : {opponentScore}";
    }

    private void SetColor()
    {
        ColorUtility.TryParseHtmlString(inputHexValue.text, out Color colorToSet);
        onColorSet?.Invoke(colorToSet);
    }

    private void ClosePanel()
    {
        inputHexValue.text = string.Empty;

        Time.timeScale = 1;
        objectPanelMenu.SetActive(false);
    }

}
