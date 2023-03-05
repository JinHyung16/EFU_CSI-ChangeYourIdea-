using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HughGenerics
{
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T instance;
        public static T GetInstance
        {
            get
            {
                if (instance == null)
                {
                    Debug.LogError("Singleton Generic�� ��ӹ��� ������Ʈ�� �����ϴ�");
                    return null;
                }
                return instance;
            }
        }

        private void Awake()
        {
            if (instance == null)
            {
                instance = this as T;
            }

            //T type�� �ߺ��Ǿ� ���� ��� 1���� ����� ���������ش�.
            var T_typeObjects = FindObjectsOfType<T>();
            if (T_typeObjects.Length == 1)
            {
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
