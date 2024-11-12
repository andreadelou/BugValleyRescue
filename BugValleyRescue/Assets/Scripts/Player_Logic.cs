using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Logic : MonoBehaviour
{

    [SerializeField]
    private float captureRange = 2.0f;

    void Update()
    {

        HandleInputs();
    }

    private void HandleInputs()
    {
        // Si el jugador presiona espacio, intenta capturar insectos cercanos
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TryCaptureNearbyObjects();
        }
    }

    private void TryCaptureNearbyObjects()
    {
        // Buscar todos los objetos Pickable en la escena
        Pickable[] pickables = FindObjectsOfType<Pickable>();

        foreach (Pickable pickable in pickables)
        {
            // Si el objeto Pickable está dentro del rango de captura, capturarlo
            if (pickable.IsPlayerNearby(transform, captureRange))
            {
                pickable.Capture();
            }
        }
    }

}