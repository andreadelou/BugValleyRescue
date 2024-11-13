using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceAI : MonoBehaviour
{
    public float detectionRadius = 5.0f; // Radio de detección del policía
    public float moveSpeed = 1.0f; // Velocidad de movimiento lento
    public float randomMoveInterval = 2.0f; // Intervalo de tiempo entre movimientos aleatorios
    public Transform player; // Referencia al transform del jugador

    private LineRenderer lineRenderer; // LineRenderer para el círculo
    private Vector3 targetPosition;
    private int circleSegments = 50; // Número de segmentos para el círculo
    private bool isPlayerInsideRadius = false; // Bandera para saber si el jugador está dentro del radio

    private UIController uiController; // Referencia al UIController

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();

        if (lineRenderer != null)
        {
            lineRenderer.positionCount = circleSegments + 1;
            lineRenderer.useWorldSpace = false;
            lineRenderer.loop = true;
            lineRenderer.startWidth = 0.1f;
            lineRenderer.endWidth = 0.1f;
            lineRenderer.material = new Material(Shader.Find("Sprites/Default"));

            DrawDetectionRadius();
        }
        else
        {
            Debug.LogError("LineRenderer no encontrado en el objeto Police. Asegúrate de que el objeto tenga un LineRenderer.");
        }

        // Encuentra el UIController en la escena
        uiController = FindObjectOfType<UIController>();
        if (uiController == null)
        {
            Debug.LogError("UIController no encontrado en la escena. Asegúrate de que el UIController está en la escena.");
        }

        StartCoroutine(RandomMovement());
    }

    void Update()
    {
        DetectPlayer();
    }

    private IEnumerator RandomMovement()
    {
        while (true)
        {
            Vector3 randomDirection = new Vector3(
                Random.Range(-1f, 1f),
                0,
                Random.Range(-1f, 1f)
            ).normalized;

            targetPosition = transform.position + randomDirection * 2f;

            float elapsedTime = 0f;
            while (elapsedTime < randomMoveInterval)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            yield return new WaitForSeconds(randomMoveInterval);
        }
    }

    // Detecta si el jugador entra o sale del radio de detección
    private void DetectPlayer()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRadius)
        {
            if (!isPlayerInsideRadius)
            {
                // El jugador acaba de entrar al radio, llamar a IncrementarFaltas en el UIController
                if (uiController != null)
                {
                    uiController.IncrementarFaltas(); // Incrementa las faltas en UIController
                }
                isPlayerInsideRadius = true;
            }
        }
        else
        {
            isPlayerInsideRadius = false;
        }
    }

    // Dibuja el círculo de detección usando LineRenderer
    private void DrawDetectionRadius()
    {
        float angle = 0f;
        for (int i = 0; i < circleSegments + 1; i++)
        {
            float x = Mathf.Sin(Mathf.Deg2Rad * angle) * detectionRadius;
            float z = Mathf.Cos(Mathf.Deg2Rad * angle) * detectionRadius;
            lineRenderer.SetPosition(i, new Vector3(x, 0, z));
            angle += 360f / circleSegments;
        }
    }
}
