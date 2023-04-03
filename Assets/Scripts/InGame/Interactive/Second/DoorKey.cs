using HughEnumData;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class DoorKey : InteractiveObject
{
    [SerializeField] private Transform doorKeyTransform;

    [Header("DoorKey UI sprite")]
    [SerializeField] private Sprite doorKeyImage;


    private Vector3 offset;

    public Sprite GetDoorKeyUISprite { get { return this.doorKeyImage; } }

    private void Start()
    {
        offset = new Vector3(0, 0.8f, 0);
    }
    private void OnDisable()
    {
        GameManager.GetInstance.InvisibleInteractiveCanvas();
        NotInteractvie();
        this.gameObject.transform.position = doorKeyTransform.position;
    }

    #region InteractiveObject Override
    protected override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (ThemeSecondPresenter.GetInstance != null && ThemeSecondPresenter.GetInstance.IsInteractiveNum != 1)
            {
                InteractiveManager.GetInstance.IsInteractive = true;
                this.Interacitve();
            }
        }
    }

    protected override void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            InteractiveManager.GetInstance.IsInteractive = false;
            this.NotInteractvie();
        }
    }

    protected override void Interacitve()
    {
        GameManager.GetInstance.VisibleInteractiveCanvas(doorKeyTransform, offset);
        InteractiveManager.GetInstance.SetInteractiving(this);
        InteractiveManager.GetInstance.SetInteractvieObjToInventory(this.gameObject);
    }

    protected override void NotInteractvie()
    {
        GameManager.GetInstance.InvisibleInteractiveCanvas();
        InteractiveManager.GetInstance.SetInteractvieObjToInventory(null);
    }

    public override InteractiveType GetInteractiveType()
    {
        return InteractiveType.ThemeSecond_Key;
    }

    #endregion
}
