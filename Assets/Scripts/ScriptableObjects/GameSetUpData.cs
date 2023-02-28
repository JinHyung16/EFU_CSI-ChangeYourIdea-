using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "GameSetUpData", menuName = "ScriptableObjects/GameSetUpData", order = 1)]
public class GameSetUpData : ScriptableObject
{
    //Main Camera ��ġ ����
    public Vector3 cameraPosition;
    public Vector3 cameraRotation;

    //Character ������ �ִ� Player�� CameraView ��ġ ����
    public Vector3 characterTransform;

    //PlayerMovement�� ���Ǵ� ������
    public float moveSpeed;
    public float jumpPower;
}
