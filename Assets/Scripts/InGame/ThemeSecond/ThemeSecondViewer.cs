using Cysharp.Threading.Tasks;
using HughEnumData;
using System;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ThemeSecondViewer : MonoBehaviour
{
    [SerializeField] private List<Canvas> canvasList = new List<Canvas>();

    [Header("Dialogue UI들")]
    [SerializeField] private Canvas dialogueCanvas;
    [SerializeField] private Button nextDialogueBtn;
    [SerializeField] private TMP_Text dialogueCharacterText;
    [SerializeField] private TMP_Text dialogueText;
    private int dialgoueIndex = 0;

    [Header("Narrative UI들")]
    [SerializeField] private Canvas narrativeCanvas;
    [SerializeField] private TMP_Text narrativeText;

    [Header("Timer Text")]
    [SerializeField] private TMP_Text resultTimerText;

    private CancellationTokenSource tokenSource;

    private void Awake()
    {
        dialogueCanvas.enabled = false;
        nextDialogueBtn.onClick.AddListener(NextDialogueBtn);
    }

    private void Start()
    {
        foreach (var canvas in canvasList)
        {
            UIManager.GetInstance.AddCanvasInDictionary(canvas.name, canvas);
        }

        narrativeCanvas.enabled = false;

        if (tokenSource != null)
        {
            tokenSource.Cancel();
            tokenSource.Dispose();
        }
        tokenSource = new CancellationTokenSource();
    }
    private void OnDisable()
    {
        nextDialogueBtn.onClick.RemoveAllListeners();
        UIManager.GetInstance.ClearAllCanvas();
    }

    public void DialogueStart()
    {
        GameManager.GetInstance.CursorSet(true);
        GameManager.GetInstance.IsDialogueStart = true;
        dialgoueIndex = 0;
        dialogueCharacterText.text = "[" + DataManager.GetInstance.ThemeSecondCharacter[0] + "] ";
        dialogueText.text = DataManager.GetInstance.ThemeSecondContext[0];
        dialogueCanvas.enabled = true;
        Time.timeScale = 0;
    }

    private void NextDialogueBtn()
    {
        dialgoueIndex += 1;
        AudioManager.GetInstance.PlaySFX(AudioManager.SFX.DialogueBtn);
        if (DataManager.GetInstance.ThemeSecondContext.Count <= dialgoueIndex)
        {
            GameManager.GetInstance.IsDialogueStart = false;
            dialogueCanvas.enabled = false;
            Time.timeScale = 1;
            ThemeSecondPresenter.GetInstance.DoneDialogue();
            return;
        }
        dialogueCharacterText.text = "[" + DataManager.GetInstance.ThemeSecondCharacter[dialgoueIndex] + "] ";
        dialogueText.text = DataManager.GetInstance.ThemeSecondContext[dialgoueIndex];
    }

    public void NarrativeCanvas(string context)
    {
        narrativeText.text = context;
        NarrativeUI().Forget();
    }
    private async UniTaskVoid NarrativeUI()
    {
        narrativeCanvas.enabled = true;
        await UniTask.Delay(TimeSpan.FromSeconds(1.5f), cancellationToken: this.GetCancellationTokenOnDestroy());
        narrativeCanvas.enabled = false;
    }

    public void DoorLockCanvasOpen()
    {
        GameManager.GetInstance.CursorSet(true);
        GameManager.GetInstance.IsUIOpen = true;
        UIManager.GetInstance.ShowCanvas("DoorLock Canvas");
    }

    public void InteractiveDoorCanvas()
    {
        GameManager.GetInstance.CursorSet(true);
        GameManager.GetInstance.IsUIOpen = true;
        UIManager.GetInstance.ShowCanvas("IDoor Canvas");
    }
    public void InteractiveShowcanseCanvasOpen()
    {
        GameManager.GetInstance.CursorSet(true);
        GameManager.GetInstance.IsUIOpen = true;
        UIManager.GetInstance.ShowCanvas("IShowcase Canvas");
    }

    public void NoteCanvaseOpen(int index)
    {
        UIManager.GetInstance.ShowCanvas("Note Canvas");
        GameManager.GetInstance.CursorSet(true);
        GameManager.GetInstance.IsUIOpen = true;
        NoteManager.GetInstance.NotePanelOpen(index);
    }

    public void NPCMissionCanvasOpen()
    {
        GameManager.GetInstance.CursorSet(true);
        GameManager.GetInstance.IsUIOpen = true;
        UIManager.GetInstance.ShowCanvas("NPCMission Canvas");
    }


    public void OpenResultCanvas(bool isClear)
    {
        GameManager.GetInstance.CursorSet(true);
        if (isClear)
        {
            resultTimerText.text = TimerManager.GetInstance.CurTimeString.ToString();
            AudioManager.GetInstance.PlaySFX(AudioManager.SFX.GameResult_Celar);
            UIManager.GetInstance.ShowCanvas("GameClearResult Canvas");
        }
        else
        {
            AudioManager.GetInstance.PlaySFX(AudioManager.SFX.GAmeResult_Fail);
            UIManager.GetInstance.ShowCanvas("GameFailedResult Canvas");
        }
        GameManager.GetInstance.IsUIOpen = true;
    }


    /// <summary>
    /// ThemeFirst Scene에서 Canvas들에게 붙어있는 Close버튼을 누르면 사용하는 공용함수
    /// </summary>
    public void CloseCanvas()
    {
        GameManager.GetInstance.IsUIOpen = false;
        AudioManager.GetInstance.PlaySFX(AudioManager.SFX.UIClick);
        UIManager.GetInstance.HideCanvas();
        GameManager.GetInstance.CursorSet(false);
    }

    #region Game Result Canvas하위 Button 기능
    public void GoToMain()
    {
        GameManager.GetInstance.GameDataClear();
        SceneController.GetInstance.LoadScene("Main");
    }

    public void NextStage()
    {
        GameManager.GetInstance.GameDataClear();
        SceneController.GetInstance.LoadScene("ThemeThird");
        //SceneController.GetInstance.LoadScene("ThemeThird_01");
    }
    public void RetryGame()
    {
        GameManager.GetInstance.GameDataClear();
        SceneController.GetInstance.LoadScene("ThemeSecond");
    }

    public void QuitGame()
    {
        GameManager.GetInstance.ProgramQuit();
    }
    #endregion
}
