using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    // Creo otro script por si acaso quiero ponerle vidas a mi nave 
    [SerializeField]
    public ParticleSystem explosionVFX;

    [SerializeField]
    public AudioSource explosionSound;

    public int hitsToDie = 1;

    public void RecieveHit()
    {
        hitsToDie--;
        if (hitsToDie == 0)
        {
            explosionVFX.transform.position = transform.position;
            explosionVFX.Play();
            explosionSound.Play();
            Destroy(gameObject);
        }
    }
}