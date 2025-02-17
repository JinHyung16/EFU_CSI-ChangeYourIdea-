using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainSceneViewer : MonoBehaviour
{
    [Header("mainScene에서 열고 닫을 패널들")]
    [SerializeField] private List<GameObject> mainScenePanelList = new List<GameObject>();

    [Header("ThemeSelectPanel 하위 테마 선택 버튼들")]
    [SerializeField] private List<Button> themeSelectBtnList = new List<Button>(); //테마 순서대로 저장

    private void Awake()
    {
        foreach (var panel in mainScenePanelList)
        {
            UIManager.GetInstance.AddPanelInDictionary(panel.name, panel);
        }
    }
    private void OnDisable()
    {
        if (UIManager.GetInstance != null)
        {
            UIManager.GetInstance.ClearAllPanel();
        }
    }

    public void ThemeSelectOpen(int num)
    {
        for (int i = 0; i < themeSelectBtnList.Count; i++)
        {
            if (i < num)
            {
                themeSelectBtnList[i].interactable = true;
            }
            else
            {
                themeSelectBtnList[i].interactable = false;
            }
        }
    }

    #region Button Functions
    /// <summary>
    /// 게임 시작 버튼을 누르면 호출하여 테마 선택 창을 보여준다.
    /// 호출시 어떤 창을 열건지 이름을 전달해준다.
    /// </summary>
    public void ThemeSelectButton()
    {
        AudioManager.GetInstance.PlaySFX(AudioManager.SFX.UIClick);
        UIManager.GetInstance.ShowPanel("ThemeSelect Panel");
    }

    public void SettingButton()
    {
        AudioManager.GetInstance.PlaySFX(AudioManager.SFX.UIClick);
        UIManager.GetInstance.ShowPanel("Setting Panel");
    }

    public void HelpButton()
    {
        AudioManager.GetInstance.PlaySFX(AudioManager.SFX.UIClick);
        UIManager.GetInstance.ShowPanel("Help Panel");
    }

    /// <summary>
    /// 진행했던 게임 내역을 다 지우고 처음부터 시작한다.
    /// </summary>
    public void ResetGame()
    {
        GameManager.GetInstance.IsFirstGame = false;
        DataManager.GetInstance.SaveThemeIndex = 1;
        DataManager.GetInstance.SaveData(1);
        ClosePanelButton();
        ThemeSelectOpen(1);
    }

    /// <summary>
    /// 패널에 붙어있는 닫기 버튼의 연결되는 함수다.
    /// 해당 씬의 모든 닫기 버튼은 이 함수와 연결되어야한다.
    /// </summary>
    public void ClosePanelButton()
    {
        AudioManager.GetInstance.PlaySFX(AudioManager.SFX.UIClick);
        UIManager.GetInstance.HidePanel();
    }

    /// <summary>
    /// ThemeSelect Panel에서 열려져있는 테마를 선택해 플레이한다.
    /// </summary>
    public void ThemeSelect()
    {
        string themeName = EventSystem.current.currentSelectedGameObject.GetComponent<ThemeSelectData>().ThemeName;
        AudioManager.GetInstance.PlaySFX(AudioManager.SFX.UIClick);
        MainScenePresenter.GetInstance.ThemeSelectedAndLoadScene(themeName);
    }

    public void QuitGame()
    {
        GameManager.GetInstance.ProgramQuit();
    }
    #endregion
}
