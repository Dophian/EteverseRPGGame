using UnityEngine;
using System.Collections;

public class Billboard2 : MonoBehaviour
{
    private Camera _mainCamera;

    void Awake()
    {
        _mainCamera = Camera.main;
    }

    void Update()
    {
        transform.rotation = Quaternion.LookRotation(_mainCamera.transform.forward);
    }
}