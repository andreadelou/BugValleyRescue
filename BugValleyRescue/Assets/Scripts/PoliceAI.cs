using System.Collections;
using UnityEngine;

public class PoliceAI : MonoBehaviour
{
    public float detectionRadius = 5.0f; // Radio de detección del policía
    public float moveSpeed = 1.0f; // Velocidad de movimiento lento
    public float randomMoveInterval = 1.0f; // Intervalo de tiempo entre movimientos aleatorios
    public Transform player; // Referencia al transform del jugador

    public AudioClip[] warningSounds; // Lista de sonidos de advertencia
    private AudioSource audioSource; // Componente para reproducir audio

    private LineRenderer lineRenderer; // LineRenderer para el círculo
    private Vector3 targetPosition;
    private int circleSegments = 50; // Número de segmentos para el círculo
    private bool isPlayerInsideRadius = false; // Bandera para saber si el jugador está dentro del radio

    private CharacterController characterController;

    private UIController uiController;

    void Start()
    {
        characterController = GetComponent<CharacterController>();

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
            Debug.LogError("LineRenderer no encontrado en el objeto Police");
        }

        uiController = FindObjectOfType<UIController>();
        if (uiController == null)
        {
            Debug.LogError("UIController no encontrado en la escena");
        }

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("AudioSource no encontrado en el objeto Police");
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

            targetPosition = transform.position + randomDirection * 5f;

            float elapsedTime = 0f;
            while (elapsedTime < randomMoveInterval)
            {
                Vector3 direction = (targetPosition - transform.position).normalized;
                Vector3 velocity = direction * moveSpeed;
                characterController.SimpleMove(velocity);

                elapsedTime += Time.deltaTime;
                yield return null;
            }

            yield return new WaitForSeconds(randomMoveInterval);
        }
    }

    private void DetectPlayer()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRadius)
        {
            if (!isPlayerInsideRadius)
            {
                // Incrementa las faltas
                uiController?.IncrementarFaltas();

                // Reproduce un sonido aleatorio
                PlayRandomWarningSound();

                isPlayerInsideRadius = true;
            }
        }
        else
        {
            isPlayerInsideRadius = false;
        }
    }

    private void PlayRandomWarningSound()
    {
        if (warningSounds.Length > 0 && audioSource != null)
        {
            int randomIndex = Random.Range(0, warningSounds.Length);
            audioSource.PlayOneShot(warningSounds[randomIndex]);
        }
    }

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
