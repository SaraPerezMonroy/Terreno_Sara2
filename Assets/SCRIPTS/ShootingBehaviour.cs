using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingBehaviour : MonoBehaviour
{
    [SerializeField]
    GameObject balaPrefab;

    public float bulletSpeed = 100f;
    Vector3 impulso;

    [SerializeField]
    public AudioSource blasterSound;


    // Start is called before the first frame update
    void Start()
    {
        ObjectPool.PreLoad(balaPrefab, 10);
        impulso = Vector3.forward * bulletSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            GameObject bala = ObjectPool.GetObject(balaPrefab); // Igualar nuestro gameobject al de la función GetObject del object pool

            Rigidbody rb_bala = bala.GetComponent<Rigidbody>();
            blasterSound.Play();
            bala.transform.position = transform.position; // Igualar posición de la bala al cañón
            rb_bala.velocity = transform.forward * bulletSpeed;
            StartCoroutine(Recicle(balaPrefab, bala, 1.5f)); // Reciclamos la bala, pasamos el prefab y la bala del getObject
        }
       
    }

    IEnumerator Recicle(GameObject prefab, GameObject copiaPrefab, float time) // Para llamar a la función de reciclado del pool
    {
        yield return new WaitForSeconds(time);
        ObjectPool.RecicleObject(prefab, copiaPrefab);
    }
}
