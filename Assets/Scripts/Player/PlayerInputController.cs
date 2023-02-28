using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class PlayerInputController : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    public Transform cameraViewTrans { private get; set; }

    private PlayerMovementController playerMovement;
    private void Start()
    {
        playerMovement = GetComponent<PlayerMovementController>();
    }

    private void Update()
    {
        InputMove();
        InputMouse();
    }

    private void InputMove()
    {
        Vector2 moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        bool isMove = moveInput.magnitude != 0;
        //animator.SetBool("IsMove", isMove);

        if (isMove)
        {
            //ĳ���� ������ �� ī�޶� �ٶ󺸴� ������ �ٶ󺸰� ����
            Vector3 lookForward = new Vector3(cameraViewTrans.forward.x, 0.0f, cameraViewTrans.forward.z).normalized;
            Vector3 lookRight = new Vector3(cameraViewTrans.right.x, 0.0f, cameraViewTrans.right.z).normalized;
            Vector3 moveDir = (lookForward * moveInput.y) + (lookRight * moveInput.x);

            playerTransform.forward = lookForward; //player�� �ٶ󺸴� ����� ī�޶� �ٶ󺸴� ���� �����ϰ� ����
            playerMovement.MoveDirection(moveDir);
        }
    }

    private void InputMouse()
    {
        Vector2 mousePos = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        Vector3 cameraAngle = cameraViewTrans.rotation.eulerAngles;

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
        cameraViewTrans.rotation = Quaternion.Euler(rotateX, cameraAngle.y + mousePos.x, cameraAngle.z);
    }
}
