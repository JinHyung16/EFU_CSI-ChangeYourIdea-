using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HughGenerics;
using UnityEngine.EventSystems;

public class UIManager : Singleton<UIManager>
{
    /// <summary>
    /// Scene�� �ٲ𶧸���, �ش� Scene���� ����� Panel���� �־��ش�.
    /// Panel�̸�, �ش� Panel GameObject �������� �����صд�.
    /// </summary>
    private Dictionary<string, GameObject> panelDictionary = new Dictionary<string, GameObject>();
    private Queue<GameObject> panelQueue = new Queue<GameObject>();

    public void AddPanelInDictionary(string panelName, GameObject panel)
    {
        panelDictionary.Add(panelName, panel);
        panel.SetActive(false);
    }

    /// <summary>
    /// Panel�� �� �� ȣ��
    /// �ش� panel�� stack�� �־� �����Ѵ�.
    /// </summary>
    /// <param name="panelName">������� Panel �̸�</param>
    public void ShowPanel(string panelName)
    {
        if (panelDictionary.TryGetValue(panelName, out GameObject obj))
        {
            if (panelQueue.Contains(obj))
            {
                var removeObj = panelQueue.Peek();
                obj.SetActive(false);
                panelQueue.Clear();
            }
            else
            {
                if (panelQueue.Count > 0)
                {
                    var removeObj = panelQueue.Peek();
                    removeObj.SetActive(false);
                    panelQueue.Dequeue();
                }

                panelQueue.Enqueue(obj);
                obj.SetActive(true);
            }
        }
    }

    /// <summary>
    /// Panel�� ���� �� ȣ��ȴ�.
    /// �ش� panel�� �����ϴ� stack���� �� ���� ���ΰ͵鵵 �� �ݾ��ش�.
    /// </summary>
    /// <param name="panelName"> ���� panel�� �̸� </param>
    public void HidePanel()
    {
        if (panelQueue.Count > 0)
        {
            foreach (var panel in panelQueue)
            {
                panel.SetActive(false);
            }
        }
        panelQueue.Clear();
    }

    /// <summary>
    /// CanvasManager�� ��ӹ��� �� Canvas���� OnDestroy()�� ȣ���Ѵ�.
    /// </summary>
    public void ClearAll()
    {
        panelDictionary.Clear();
        panelQueue.Clear();
    }
}
