using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEnd : MonoBehaviour
{
    public GameObject prefab;   // Unser Sphären-Prefab
    public MeshRenderer _meshRenderer;  // Der Renderer des Würfels

    private AudioSource audioSource;    // Referenz auf die AudioSource
    private bool gameFinished;          // Signalisiert ob das Spiel gefinished wurde

    void Start()
    {
        // === Private Komponenten initialisieren, da sie nicht im Editor sichtbar sind ===
        audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other)
    {
        // === Wenn der untere Code bereits ausgeführt wurde, hier abbrechen ===
        if (gameFinished)
            return;

        gameFinished = true;

        // === Prefab am Punkt (0 | 1 | 0) spawnen ===
        Instantiate(prefab, new(0, 1, 0), Quaternion.identity);

        // === Sound abspielen ===
        audioSource.PlayOneShot(audioSource.clip);

        // === Farbe des Cubes auf Grün ändern ===
        _meshRenderer.material.color = Color.green;
    }
}
