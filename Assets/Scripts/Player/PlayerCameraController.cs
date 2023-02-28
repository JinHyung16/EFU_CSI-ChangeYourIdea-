using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraController : MonoBehaviour
{
    [SerializeField] private GameSetUpData gameSetUpData; //game setup data

    [SerializeField] private Transform cameraViewTransform; //ī�޶� �ش� ������Ʈ �ڽ����� �־���Ѵ�.
    [SerializeField] private Transform playerTransform; //�����̴� player�� Transform�� ���� ����

    private GameObject mainCamera; //scene���� ã�� main camera�� ���� ����

    private PlayerInputController playerInputController;
    private void Awake()
    {
        //Character ������Ʈ ������ Player�� CameraView�� �����Ƿ� �� ���� ��ġ�� ����ȭ �����ش�.
        cameraViewTransform.position = gameSetUpData.characterTransform;
        playerTransform.position = gameSetUpData.characterTransform;

        //���� �ִ� ī�޶� ã�Ƽ� ������ �ΰ� ���� �����δ�.
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        mainCamera.transform.parent = this.cameraViewTransform;
        mainCamera.transform.position = gameSetUpData.cameraPosition;
        mainCamera.transform.rotation = Quaternion.Euler(gameSetUpData.cameraRotation);

        playerInputController = GetComponent<PlayerInputController>();
        playerInputController.cameraViewTrans = mainCamera.transform;
    }


    private void Update()
    {
        //ī�޶� player�� ��� �Ѿư���. �̶�, cameraView�� mainCamera�� �����̹Ƿ� �̰� �̵���Ų��
        cameraViewTransform.transform.position = playerTransform.position;

        /*
        new Vector3(playerTransform.position.x, 
            playerTransform.position.y + gameSetUpData.cameraPosition.y, 
            playerTransform.position.z + gameSetUpData.cameraPosition.z
            );
        */
    }
}
