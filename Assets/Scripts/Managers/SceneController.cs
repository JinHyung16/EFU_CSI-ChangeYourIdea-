using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cysharp.Threading.Tasks;
using HughGenerics;

public class SceneController : Singleton<SceneController>
{
    public string CurSceneName { get; private set; } //���� ���� �ӹ����ִ� Scene�̸�
    public string LoadSceneName { get; private set; } //���� �̵��� Scene�̸�

    /// <summary>
    /// Loading Scene�� ������ ��� Scene�� �ִ� Manager���� ȣ���Ѵ�.
    /// ���� ���� �ִ� Scene�� �̸��� �޾Ƴ��´�.
    /// </summary>
    public void SetCurScene()
    {
        CurSceneName = SceneManager.GetActiveScene().name;
    }

    #region �񵿱� Scene �̵� ó�� Functions
    /// <summary>
    /// �⺻������ ȣ���ϴ� �Լ�
    /// �̵��� Scene�� �̸��� �޾� �����صΰ� LoadingScene���� �̵��� ��, �̸� �޾Ƴ��� Scene���� �̵���Ų��.
    /// </summary>
    /// <param name="loadSceneName"> �̵��� �� �̸��� �޴´� </param>
    public void LoadScene(string loadSceneName)
    {
        GameManager.GetInstance.DespawnPlayer();

        this.CurSceneName = "LoadingScene";
        this.LoadSceneName = loadSceneName;

        SceneManager.LoadScene("LoadingScene");
    }

    public async UniTask LoadScenario()
    {
        AsyncOperation loadSceneAsync = SceneManager.LoadSceneAsync("ScenarioScene");
        await loadSceneAsync;
    }
    #endregion
}
