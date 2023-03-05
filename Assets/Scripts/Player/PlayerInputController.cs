using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputController : MonoBehaviour
{
    private PlayerMovementController playerMovementController;

    [Header("Player Transform")]
    [SerializeField] private Transform playerTransform;

    public Transform cameraView { private get; set; }

    private void Start()
    {
        playerMovementController = GetComponentInChildren<PlayerMovementController>();
    }

    private void Update()
    {
        if (!GameManager.GetInstance.IsGamePause)
        {
            InputMovementControl();
            InputJumpControl();
            InputMouseViewControl();
        }
    }

    private void InputMovementControl()
    {
        Vector3 moveInput = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        
        bool isMove = moveInput.magnitude != 0;
        //animator.SetBool("IsMove", isMove);
        if (isMove)
        {
            //Character -> Player & Camera (���� �����) �����̸� ����ϱ�
            /*
            //ĳ���� ������ �� ī�޶� �ٶ󺸴� ������ �ٶ󺸰� ����
            Vector3 lookForward = new Vector3(cameraView.forward.x, 0.0f, cameraView.forward.z).normalized;
            Vector3 lookRight = new Vector3(cameraView.right.x, 0.0f, cameraView.right.z).normalized;
            Vector3 moveDir = (lookForward * moveInput.y) + (lookRight * moveInput.x);

            playerTransform.forward = lookForward; //player�� �ٶ󺸴� ����� ī�޶� �ٶ󺸴� ���� �����ϰ� ����
            */
            playerMovementController.MoveDirection(moveInput);
        }
        
    }

    private void InputJumpControl()
    {
        if (Input.GetButtonDown("Jump"))
        {
            playerMovementController.JumpDirection(Vector3.up);
        }
    }


    private void InputMouseViewControl()
    {
        Vector2 mousePos = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        Vector3 cameraAngle = cameraView.rotation.eulerAngles;

        //ī�޶� �������� �ʹ� ȸ���ϸ� �������� ���� �ذ�
        float rotateX = cameraAngle.x - mousePos.y;
        if (rotateX < 180.0f)
        {
            //-1.0f�� �ּҿ��� ī�޶� ����� �Ʒ��� ��������.
            rotateX = Mathf.Clamp(rotateX, -1.0f, 70.0f);
        }
        else
        {
            //25�� ���� �����ϱ� ���� 360.0f - 25.0f�� ������ �ְ�
            //361.0f�� �ִ뿩�� ī�޶� ����� ���� �� �ö󰣴�.
            //-1.0f�� �������� ī�޶� ����� �Ʒ��� ��������.
            rotateX = Mathf.Clamp(rotateX, 335.0f, 361.0f);
        }
        //���콺 �¿� ���������� ī�޶� �¿� ������ ����, ���콺 ���� ���������� ī�޶� ���� ������ ����
        //camera.x rotate�ϸ� �� �Ʒ��� ȸ���ϰ�, camera.y rotate�ϸ� �¿�� ȸ���Ѵ�
        cameraView.rotation = Quaternion.Euler(rotateX, cameraAngle.y + mousePos.x, cameraAngle.z);
    }
}
