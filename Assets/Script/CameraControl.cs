using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public Transform target;
    private Vector3 offset;

    void Start()
    {
        // Ustal ró¿nicê pozycji miêdzy kamer¹ a graczem
        offset = transform.position - target.position;
    }

    void LateUpdate()
    {
        // Ustaw pozycjê kamery wzglêdem gracza z uwzglêdnieniem offsetu
        Vector3 newPosition = target.position + offset;
        transform.position = newPosition;
    }
}

