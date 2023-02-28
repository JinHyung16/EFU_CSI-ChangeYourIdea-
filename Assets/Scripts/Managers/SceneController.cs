using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using HughGenerics;
using Cysharp.Threading.Tasks;

public class SceneController : Singleton<SceneController>
{
    [SerializeField] private Canvas loadingCanvas;


    //scene ��ȯ �񵿱� ������ ���� �����Ǵ� �ڷ�ƾ
    private AsyncOperation loadSceneAsync;

    //���� �ε� �ð�
    [SerializeField] private float realLoadTime = 4.0f;

    //�ε� �ð� �� �ּڰ��� ���� ����
    private float minLoadRatio;

    //��¥ �ε��ð��� ����
    private float fakeLoadTime;
    private float fakeLoadRatio;

    private void Start()
    {
        loadingCanvas.enabled = false;
    }

    public async UniTaskVoid LoadSceneAsync(string sceneName)
    {
        loadingCanvas.enabled = true;

        //���� �ƹ��͵� ���� Scene�� �ø���,
        loadSceneAsync = SceneManager.LoadSceneAsync("LoadingScene");
        if (loadSceneAsync.isDone)
        {
            loadSceneAsync.allowSceneActivation = true;
            loadSceneAsync = null;
        }
        
        loadSceneAsync = SceneManager.LoadSceneAsync(sceneName);
        loadSceneAsync.allowSceneActivation = false;

        while (!loadSceneAsync.isDone)
        {
            //fake �ε� �ð� ����ϱ�
            fakeLoadTime += Time.deltaTime;
            fakeLoadRatio = fakeLoadTime / realLoadTime;

            //���� �ε� �ð��� fake �ε� �ð� �� �ּڰ����� �ε��� �����ϱ�
            minLoadRatio = Mathf.Min(loadSceneAsync.progress + 0.1f, fakeLoadRatio);

            //Loading UI�� text Update�ϱ�
            //loadingGaugeTxt.text = (minLoadRatio * 100).ToString("F0") + "%";

            if (minLoadRatio >= 1.0f)
            {
                //loadingCanvasGroup.DOFade(1, 1.0f);
                loadingCanvas.enabled = false;
                break;
            }

            await UniTask.Yield();
        }

        loadSceneAsync.allowSceneActivation = true;
    }
}
