using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HughCanvas;
using HughUIType;

public class MainCanvas : CanvasManager
{
    private void Start()
    {
        SceneController.GetInstance.SetCurScene();
        UIManager.GetInstance.AddCanvasInDictionary(CanvasType.FixedCanvas, this);
    }
    private void OnDestroy()
    {
        UIManager.GetInstance.ClearAllCanvas();
    }

    /// <summary>
    /// ���ο� ���� ������ �� ȣ��
    /// </summary>
    public void StartNewGame()
    {
        GameManager.GetInstance.StartNewGame();
    }


    /// <summary>
    /// �̾ �ϱ� ������ ȣ��
    /// </summary>
    public void StartLoadGame()
    {
        GameManager.GetInstance.StartSavedGame();
    }
}
