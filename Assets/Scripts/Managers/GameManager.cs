using HughGenerics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;

public class GameManager : Singleton<GameManager>, IDisposable
{
    [Header("Player Prefab")]
    [SerializeField] private GameObject playerPrefab;
    private GameObject player;

    [Header("Game Option Canvas")]
    [SerializeField] private Canvas gameOptionCanvas;
    [SerializeField] private Button gameExitBtn;

    private KeyCode optionKeyCode = KeyCode.Escape;
    private bool isOptionKeyDown;

    public bool IsGamePause { get; private set; }
    public bool IsEndTheme { private get; set; }

    private void Start()
    {
        gameOptionCanvas.enabled = false;
        optionKeyCode = KeyCode.Escape;
        isOptionKeyDown = false;
        IsGamePause = false;

        gameExitBtn.onClick.AddListener(ExitGameAndSaveDataAsync);
    }

    private void Update()
    {
        OptionCanvasOpen();
    }

    private void OptionCanvasOpen()
    {
        if (Input.GetKeyDown(optionKeyCode))
        {
            if (!isOptionKeyDown)
            {
                Time.timeScale = 0;
                IsGamePause = true;

                gameOptionCanvas.enabled = true;
                isOptionKeyDown = true;
            }
            else
            {
                Time.timeScale = 1;
                IsGamePause = false;

                gameOptionCanvas.enabled = false;
                isOptionKeyDown = false;
            }
        }
    }

    /// <summary>
    /// ������ �����ϴ� �׸��Ϸ��� �������� Exit Button�� ������ ȣ��
    /// �ڵ����� ���� �����Ȳ�� �������ش�.
    /// </summary>
    private void ExitGameAndSaveDataAsync()
    {
        Dispose();
        DataManager.GetInstance.SaveData(SceneController.GetInstance.CurSceneName);
        SceneController.GetInstance.LoadScene("Main");
    }

    /// <summary>
    /// ThemeFirst�� �����ϸ� Player�� ������Ų��.
    /// ���� 1ȸ�� ������Ų��.
    /// </summary>
    public void SpawnPlayer()
    {
        if (player == null)
        {
            player = Instantiate(playerPrefab, this.transform);
        }
        player.SetActive(true);
        player.GetComponent<PlayerManager>().PlayerSetUp();
    }

    /// <summary>
    /// Theme�� �̵��Ҷ����� ȣ���Ѵ�.
    /// </summary>
    public void DespawnPlayer()
    {
        if (player != null)
        {
            player.SetActive(false);
        }
    }

    /// <summary>
    /// �׸��� �����ϴ� ������ �� �� ȣ��
    /// �׸��� �� Ŭ���������� ȣ���Ѵ�.
    /// </summary>
    public void Dispose()
    {
        Destroy(player);
        GC.SuppressFinalize(player);
    }

    #region Game ���� �÷ο� ���� �Լ�
    /// <summary>
    /// Main Scene���� ������ �ٽ� ������ ��� �ҷ��´�.
    /// </summary>
    public void StartNewGame()
    {
        if (!DataManager.GetInstance.GetUserLoginRecord())
        {
            //������ ������ ����� ���ٸ� ��� ���ֱ�
            DataManager.GetInstance.SetUserLoginRecord();
        }
        SceneController.GetInstance.LoadScenario().Forget();
    }

    /// <summary>
    /// ������ ��ϵ� ������ �ҷ��� �����Ѥ�.
    /// </summary>
    public void StartSavedGame()
    {
        var loadScene = DataManager.GetInstance.LoadData();
        SceneController.GetInstance.LoadScene(loadScene);
    }

    /// <summary>
    /// ������ Clear���� ������ ���, ���� ���� �ٽ� �ҷ��� ó������ �����Ѵ�.
    /// </summary>
    public void FailedGameAndRestart()
    {
        DespawnPlayer();
        SceneController.GetInstance.LoadScene(SceneController.GetInstance.CurSceneName);
    }

    /// <summary>
    /// Scene�� Clear�ϸ� ���� ������ ������.
    /// �̶�, ������ Scene�̸� Main���� ����.
    /// </summary>
    /// <param name="nextScene">�̵��� ���� Scene �̸�</param>
    public void CelarGame()
    {
        if (IsEndTheme)
        {
            Dispose();
        }
        else
        {
            DespawnPlayer();
        }
        SceneController.GetInstance.LoadScene("Main");
    }
    #endregion
}
