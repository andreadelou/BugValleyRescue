using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderMovement : MonoBehaviour
{
    public float areaRadius = 10f; // Radio del área donde se moverán los insectos
    public float speed = 2f; // Velocidad de movimiento del insecto
    public float waitTime = 2f; // Tiempo de espera entre movimientos

    private Vector3 targetPosition;

    void Start()
    {
        // Selecciona un primer punto al azar
        ChooseNewPosition();
    }

    void Update()
    {
        MoveToTarget();

        // Si llega al destino, espera y luego elige un nuevo punto
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            Invoke(nameof(ChooseNewPosition), waitTime);
        }
    }

    void ChooseNewPosition()
    {
        // Genera un punto aleatorio en el área
        float randomX = Random.Range(-areaRadius, areaRadius);
        float randomZ = Random.Range(-areaRadius, areaRadius);
        targetPosition = new Vector3(randomX, transform.position.y, randomZ);
    }

    void MoveToTarget()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
    }
}
