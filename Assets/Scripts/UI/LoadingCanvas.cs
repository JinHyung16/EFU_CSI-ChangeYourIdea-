using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HughCanvas;
using HughUIType;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

public class LoadingCanvas : CanvasManager
{
    //���� �ε� �ð�
    [SerializeField] private float realLoadTime = 4.0f;

    //�ε� �ð� �� �ּڰ��� ���� ����
    private float minLoadRatio;

    //��¥ �ε��ð��� ����
    private float fakeLoadTime;
    private float fakeLoadRatio;

    private void Start()
    {
        UIManager.GetInstance.AddCanvasInDictionary(CanvasType.FixedCanvas, this);

        SceneController.GetInstance.SetCurScene();
        LoadThemeScene().Forget();
    }

    private void OnDestroy()
    {
        UIManager.GetInstance.ClearAllCanvas();
    }

    /// <summary>
    /// Loading Scene������ ȣ���ϴ� �Լ�
    /// �׻� LoadingScene���� �̵��� ��, LoadingScene���� ���� �̵��� ���� �ҷ� �� ��ȯ�� ����ȭ�� �����Ѵ�.
    /// </summary>
    /// <returns>�񵿱� ó��</returns>
    private async UniTaskVoid LoadThemeScene()
    {
        AsyncOperation loadSceneAsync = SceneManager.LoadSceneAsync(SceneController.GetInstance.LoadSceneName);
        loadSceneAsync.allowSceneActivation = false;
        while (!loadSceneAsync.isDone)
        {
            //fake �ε� �ð� ����ϱ�
            fakeLoadTime += Time.deltaTime;
            fakeLoadRatio = fakeLoadTime / realLoadTime;

            //���� �ε� �ð��� fake �ε� �ð� �� �ּڰ����� �ε��� �����ϱ�
            minLoadRatio = Mathf.Min(loadSceneAsync.progress + 0.1f, fakeLoadRatio);

            //Scene �ε� ������ UI����
            //loadingGaugeTxt.text = (minLoadRatio * 100).ToString("F0") + "%";

            if (minLoadRatio >= 1.0f)
            {
                break;
            }

            await UniTask.Yield();
        }
        loadSceneAsync.allowSceneActivation = true;
        GameManager.GetInstance.SpawnPlayer();
    }
}
