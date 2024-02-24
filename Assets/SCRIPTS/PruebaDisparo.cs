using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PruebaDisparo : MonoBehaviour
{
    [SerializeField]
    GameObject balaPrefab;
  //  [SerializeField]
    //GameObject misilPrefab;

    // [SerializeField] GameObject bombaPrefab;


    public float bulletSpeed = 100f;
    Vector3 impulso;

    // Start is called before the first frame update
    void Start()
    {
        ObjectPool.PreLoad(balaPrefab, 10);
       // ObjectPool.PreLoad(misilPrefab, 3);
        impulso = Vector3.forward * bulletSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonUp("Fire1"))
        {
            GameObject bala = ObjectPool.GetObject(balaPrefab);
            Rigidbody rb_bala = bala.GetComponent<Rigidbody>();

            bala.transform.position = transform.position;
            rb_bala.velocity = transform.forward * bulletSpeed; // Corregido aquí
            StartCoroutine(Recicle(balaPrefab, bala, 2.0f)); // Reciclamos la bala, pasamos el prefab y la bala del getObject
        }

        /*if (Input.GetKeyDown(KeyCode.E))
        {
            GameObject misil = ObjectPool.GetObject(misilPrefab);
            Rigidbody rb_misil = misil.GetComponent<Rigidbody>(); // Corregido aquí
            misil.transform.position = transform.position;
            rb_misil.velocity = transform.forward * bulletSpeed; // Corregido aquí
            StartCoroutine(Recicle(misilPrefab, misil, 2.0f)); // Reciclamos la bala, pasamos el prefab y la bala del getObject
        }*/
    }

 
    IEnumerator Recicle(GameObject prefab, GameObject copiaPrefab, float time) // Para llamar a la función de reciclado del pool
    {
        yield return new WaitForSeconds(time);
        ObjectPool.RecicleObject(prefab, copiaPrefab);
    }
}
