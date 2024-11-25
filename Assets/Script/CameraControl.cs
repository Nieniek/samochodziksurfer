using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public Transform target;
    private Vector3 offset;

    void Start()
    {
        // Ustal r�nic� pozycji mi�dzy kamer� a graczem
        offset = transform.position - target.position;
    }

    void LateUpdate()
    {
        // Ustaw pozycj� kamery wzgl�dem gracza z uwzgl�dnieniem offsetu
        Vector3 newPosition = target.position + offset;
        transform.position = newPosition;
    }
}

