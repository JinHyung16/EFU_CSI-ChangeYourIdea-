using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HughCanvas;
using HughUIType;
using HughGenerics;

public class UIManager : Singleton<UIManager>
{
    /// <summary>
    /// Canvas���� CanvasManager�� ��ӹ޾� UIManager���� ������ CanvasType�� ������ �־��ش�.
    /// �׸��� UIManager���� Control�� �����Ѵ�
    /// </summary>
    private Dictionary<CanvasType, CanvasManager> canvasDictionary = new Dictionary<CanvasType, CanvasManager>();
    private Queue<CanvasManager> canvasQueue = new Queue<CanvasManager>();

    public void AddCanvasInDictionary(CanvasType canvasType, CanvasManager canvasManager)
    {
        canvasDictionary.Add(canvasType, canvasManager);
        if (canvasType != CanvasType.FixedCanvas)
        {
            canvasManager.HideCanvas();
        }
    }

    public void ShowCanvas<T>(CanvasType canvasType) where T : CanvasManager
    {
        canvasDictionary[canvasType].ShowCanvas();

        if (canvasType == CanvasType.FixedCanvas)
        {
            if (canvasQueue.Count > 0)
            {
                foreach (var panel in canvasQueue)
                {
                    panel.gameObject.SetActive(false);
                }
                canvasQueue.Clear();
            }
        }

        if (canvasDictionary.TryGetValue(canvasType, out CanvasManager obj) && canvasType != CanvasType.FixedCanvas)
        {
            if (canvasQueue.Contains(obj))
            {
                obj.HideCanvas();
                canvasQueue.Clear();
            }
            else
            {
                if (canvasQueue.Count > 0)
                {
                    var removeObj = canvasQueue.Peek();
                    removeObj.HideCanvas();
                    canvasQueue.Dequeue();
                }

                canvasQueue.Enqueue(obj);
                obj.ShowCanvas();
            }
        }
    }

    /// <summary>
    /// CanvasManager�� ��ӹ��� �� Canvas���� OnDestroy()�� ȣ���Ѵ�.
    /// </summary>
    public void ClearAllCanvas()
    {
        canvasDictionary.Clear();
        canvasQueue.Clear();
    }
}
