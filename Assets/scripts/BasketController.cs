using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketController : MonoBehaviour
{
    public float speed = 8f;
    public float minX = -7.5f;
    public float maxX = 7.5f;

    void Update()
    {
        float input = Input.GetAxisRaw("Horizontal"); // pijltjes/A-D
        Vector3 pos = transform.position;
        pos.x += input * speed * Time.deltaTime;
        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        transform.position = pos;
    }
}