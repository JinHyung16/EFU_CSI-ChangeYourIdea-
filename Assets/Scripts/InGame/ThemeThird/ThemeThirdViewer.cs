using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class ThemeThirdViewer : MonoBehaviour
{
    [SerializeField] private List<Canvas> canvasList = new List<Canvas>();

    [Header("Dialogue UI들")]
    private int dialgoueIndex = 0;
    [SerializeField] private Button nextDialogueBtn;
    [SerializeField] private TMP_Text dialgoueText;


    [Header("Narrative UI들")]
    [SerializeField] private Canvas narrativeCanvas;
    [SerializeField] private TMP_Text narrativeText;

    [Header("Timer Text")]
    [SerializeField] private TMP_Text resultTimerText;

    private CancellationTokenSource tokenSource;
    private void Start()
    {
        for (int i = 0; i < canvasList.Count; i++)
        {
            UIManager.GetInstance.AddCanvasInDictionary(canvasList[i].name, canvasList[i]);
        }

        nextDialogueBtn.onClick.AddListener(NextDialogueBtn);

        narrativeCanvas.enabled = false;

        if (tokenSource != null)
        {
            tokenSource.Cancel();
            tokenSource.Dispose();
        }
        tokenSource = new CancellationTokenSource();
    }

    public void DialogueStart()
    {
        Time.timeScale = 0;
        dialgoueIndex = 0;
        dialgoueText.text = DataManager.GetInstance.ThemeSecondContent[0];
        UIManager.GetInstance.ShowCanvas("Dialogue Canvas");
    }

    private void NextDialogueBtn()
    {
        dialgoueIndex += 1;
        if (DataManager.GetInstance.ThemeThirdContent.Count <= dialgoueIndex)
        {
            UIManager.GetInstance.HideCanvas();
            Time.timeScale = 1;
            ThemeThirdPresenter.GetInstance.DoneDialogue();
            return;
        }
        dialgoueText.text = DataManager.GetInstance.ThemeThirdContent[dialgoueIndex];
    }

    public void NarrativeCanvas(string context)
    {
        narrativeText.text = context;
        NarrativeUI().Forget();
    }

    private async UniTaskVoid NarrativeUI()
    {
        narrativeCanvas.enabled = true;
        await UniTask.Delay(TimeSpan.FromSeconds(1.5f), cancellationToken: tokenSource.Token);
        narrativeCanvas.enabled = false;
    }

    #region Game Result Canvas하위 Button 기능
    public void GoToMain()
    {
        GameManager.GetInstance.GameClear();
        SceneController.GetInstance.LoadScene("Main");
    }

    public void NextStage()
    {
        GameManager.GetInstance.GameClear();
        SceneController.GetInstance.LoadScene("Main");
    }
    public void RetryGame()
    {
        GameManager.GetInstance.GameClear();
        SceneController.GetInstance.LoadScene("ThemeSecond");
    }

    public void QuitGame()
    {
        GameManager.GetInstance.OnApplicationQuit();
    }
    #endregion

    /// <summary>
    /// ThemeFirst Scene에서 Canvas들에게 붙어있는 Close버튼을 누르면 사용하는 공용함수
    /// </summary>
    public void CloseCanvas()
    {
        GameManager.GetInstance.IsUIOpen = false;
        UIManager.GetInstance.HideCanvas();
    }
}
