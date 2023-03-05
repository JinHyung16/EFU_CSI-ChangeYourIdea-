using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HughGenerics;
using System;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private GameSetUpData gameSetUpData; //game setup data

    [Header("Player Transform")]
    [SerializeField] private Transform playerTransform; //�����̴� player�� Transform�� ���� ����

    [Header("Player Camera")]
    [SerializeField] private GameObject playerCamera = null; //scene���� ã�� main camera�� ���� ����

    private PlayerInputController playerInputController;
    
    private void OnEnable()
    {
        playerInputController = GetComponent<PlayerInputController>();
        playerInputController.cameraView = playerCamera.transform;
    }

    public void PlayerSetUp()
    {
        playerTransform.position = gameSetUpData.characterTransform;

        playerCamera.transform.position = gameSetUpData.cameraPosition;
        playerCamera.transform.rotation = Quaternion.Euler(gameSetUpData.cameraRotation);
    }
}
