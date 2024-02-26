using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimelineController : MonoBehaviour
{
    [SerializeField]
    private PlayableDirector playableDirector;

    bool cinematicSkipped = false;

    public void SkipCinematic(float time) // Funci�n para reproducir la l�nea de tiempo desde un tiempo espec�fico
    {
        playableDirector.time = time; 
        playableDirector.Play();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !cinematicSkipped)
        {
            SkipCinematic(1416f); // Mi cinem�tica termina en el frame 1416
            cinematicSkipped = true;
        }
    }
}
