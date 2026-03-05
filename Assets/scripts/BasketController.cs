using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Dit script zorgt ervoor dat de basket links en rechts kan bewegen
public class BasketController : MonoBehaviour
{
    // Snelheid van de basket
    public float speed = 8f;
    // Grenzen waar de basket niet voorbij mag bewegen
    public float minX = -7.5f;
    public float maxX = 7.5f;

    void Update()
    {
        // Lees de input van het toetsenbord (A/D of pijltjes)
        float input = Input.GetAxisRaw("Horizontal"); // pijltjes/A-D
        // Huidige positie van de basket opslaan
        Vector3 pos = transform.position;
        // Verplaats de basket horizontaal
        pos.x += input * speed * Time.deltaTime;
        // Zorg dat de basket binnen de grenzen blijft
        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        // Pas de nieuwe positie toe op de basket
        transform.position = pos;
    }
}