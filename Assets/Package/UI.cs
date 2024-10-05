using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    public GameObject canvas;   // Referenz auf das Men� Objekt
    public GameObject cylinder; // Referenz auf das Zylinderobjekt

    private bool pauseMenuActive;   // Signalisiert ob das Men� an ist

    void Start()
    {
        // === Men� & Zylinder standardm��ig deaktivieren ===
        canvas.SetActive(false);
        cylinder.SetActive(false);
    }

    void Update()
    {
        // === �ffnen / schlie�en des Men�s -> Tastenabfrage f�r ESC ===
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseMenuActive = !pauseMenuActive;
            canvas.SetActive(pauseMenuActive);

            // === Pausieren des Spiels (der Zeit) ===
            Time.timeScale = pauseMenuActive ? 0 : 1;
        }

        // === Sichtbarkeit des Cursor ===
        Cursor.visible = pauseMenuActive;
    }

    // === Aktivieren des Zylinders ===
    public void ActivateCylinder()
    {
        cylinder.SetActive(true);
    }
}
