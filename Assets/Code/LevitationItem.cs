using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevitationItem : MonoBehaviour
{
    [SerializeField] private float heightRange = 1.3f;
    [SerializeField] private float speed = 1.5f;

    [SerializeField] private float rotationSpeed = 1.5f;
    [SerializeField] private bool rotateX = false;
    [SerializeField] private bool rotateY = false;
    [SerializeField] private bool rotateZ = false;

    private float startHeight;

    void Start()
    {
        startHeight = transform.position.y;
    }

    void Update()
    {
        transform.position = new Vector3(transform.position.x, (float)(startHeight + Mathf.Sin(Time.time * speed) * heightRange / 2.0), transform.position.z);

        if (rotateX)
            transform.Rotate(rotationSpeed, 0, 0);
        if (rotateY)
            transform.Rotate(0, rotationSpeed, 0);
        if (rotateZ)
            transform.Rotate(0, 0, rotationSpeed);
    }
}
