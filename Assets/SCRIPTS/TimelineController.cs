using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimelineController : MonoBehaviour
{
    [SerializeField]
    private PlayableDirector playableDirector;

    bool cinematicSkipped = false;

    public void SkipCinematic(float time) // Función para reproducir la línea de tiempo desde un tiempo específico
    {
        playableDirector.time = time; 
        playableDirector.Play();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !cinematicSkipped)
        {
            SkipCinematic(1416f); // Mi cinemática termina en el frame 1416
            cinematicSkipped = true;
        }
    }
}
