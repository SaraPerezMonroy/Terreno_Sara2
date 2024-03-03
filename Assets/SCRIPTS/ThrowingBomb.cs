using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ThrowingBomb : MonoBehaviour
{
    [SerializeField]
    GameObject bombaPrefab;
    [SerializeField]
    public float bombSpeed;

    public float bombCoolDown =5f;

    public bool canBomb;

    [SerializeField]
    public AudioSource bombExplosion;
    [SerializeField]
    public AudioSource bombFalling;    
    [SerializeField]
    public ParticleSystem bombParticles;

    // Start is called before the first frame update
    void Start()
    {
        ObjectPool.PreLoad(bombaPrefab, 1);
       
    }

    // Update is called once per frame
    void Update()
    {
        if (canBomb) 
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                GameObject bomba = ObjectPool.GetObject(bombaPrefab); // Igualar nuestro gameobject al de la función GetObject del object pool
                Rigidbody rb_bomba = bomba.GetComponent<Rigidbody>();
                bombFalling.Play();
                bomba.transform.position = transform.position; // Igualar posición de la bala al cañón
                rb_bomba.velocity = transform.forward * bombSpeed;
                StartCoroutine(Recicle(bombaPrefab, bomba, 2f)); // Reciclamos la bala, pasamos el prefab y la bala del getObject
                canBomb = false;
                StartCoroutine(BombTimer(bombCoolDown)); 
            }
        }
    }

    IEnumerator Recicle(GameObject prefab, GameObject copiaPrefab, float time) // Para llamar a la función de reciclado del pool
    {
        yield return new WaitForSeconds(time);
        bombFalling.Stop();
        bombExplosion.Play();
        bombParticles.transform.position = copiaPrefab.transform.position;   
        bombParticles.Play();

        ObjectPool.RecicleObject(prefab, copiaPrefab);
    } 
    IEnumerator BombTimer(float bombCoolDown)  
    {
        yield return new WaitForSeconds(bombCoolDown);
        canBomb = true;
    }
    
}
