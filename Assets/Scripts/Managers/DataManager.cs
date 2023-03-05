using HughGenerics;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataManager : Singleton<DataManager>
{
    private string GameDataFileName = "GameProgressData.json";
    private string filePath = "/Resources/Data";
    private GameProgressData gameProgressData;

    #region PlayerPrefab Control
    private string userSession = "EFU.UserSession";
    
    /// <summary>
    /// ������ �������� ���� 1ȸ �����ϸ� value�� 1 ����
    /// </summary>
    public void SetUserLoginRecord()
    {
        PlayerPrefs.SetInt(userSession, 1);
    }

    /// <summary>
    /// ������ User�� ���� 1ȸ ������ �������� �ִ��� �ҷ��´�.
    /// </summary>
    /// <returns> �ش� ���� 1�̸� true, ���ų� 0�̸� false return </returns>
    public bool GetUserLoginRecord()
    {
        var isSession = PlayerPrefs.GetInt(userSession);
        if (isSession == 1)
        {
            return true;
        }
        return false;
    }
    #endregion

    #region Json Data Control
    /// <summary>
    /// ���� ������ ���� ��Ȳ�� Josn���� �����մϴ�.
    /// �׸� ������ �����մϴ�.
    /// </summary>
    public void SaveData(string theme)
    {
        LoadDataInJson();
        if (gameProgressData == null)
        {
            gameProgressData = new GameProgressData
            {
                ThemeName = theme
            };

            string jsonData = JsonUtility.ToJson(gameProgressData);
            string path = Path.Combine(Application.dataPath + filePath, GameDataFileName);
            File.WriteAllText(path, jsonData);
        }
        else
        {
            string jsonData = JsonUtility.ToJson(gameProgressData);
            JsonUtility.FromJsonOverwrite(jsonData, gameProgressData);
        }

#if UNITY_EDITOR
        Debug.Log("DataManager: ���� ������ ���� �Ϸ�");
#endif
    }

    public string LoadData()
    {
        LoadDataInJson();
        return gameProgressData.ThemeName;
    }

    /// <summary>
    /// Json���� ����� ���� ���� ��Ȳ �����͸� �о�ɴϴ�.
    /// �ش� �׸��� ù ��°���� �����մϴ�.
    /// </summary>
    private void LoadDataInJson()
    {
        string path = Path.Combine(Application.dataPath + filePath, GameDataFileName);
        if (File.Exists(path))
        {
            string jsonData = File.ReadAllText(path);
            gameProgressData = JsonUtility.FromJson<GameProgressData>(jsonData);
#if UNITY_EDITOR
            Debug.Log("DataManager: ����� ���� ������ �ҷ����� �Ϸ�");
#endif
        }
    }
    #endregion
}
