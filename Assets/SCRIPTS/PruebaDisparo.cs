using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prueba_Disparo : MonoBehaviour
{
    [SerializeField]
    GameObject balaPrefab;
    [SerializeField]
    GameObject misilPrefab;

    // [SerializeField] GameObject misilPrefab;

    [SerializeField]
    GameObject cannonR; // El empty del cañón para la derecha
    [SerializeField]
    GameObject cannonL;  // El empty del cañón para la izquierda

    int Speed = 130;


    // Start is called before the first frame update
    void Start()
    {
        ObjectPool.PreLoad(balaPrefab, 10);
        ObjectPool.PreLoad(misilPrefab, 3);

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetButtonUp("Fire1"))
        {
            GameObject bala = ObjectPool.GetObject(balaPrefab);

            Rigidbody rb = balaPrefab.GetComponent<Rigidbody>();
            //Add force to bullet
            rb.AddForce(transform.TransformDirection(0, 0, 1) * Speed);
            StartCoroutine(Recicle(balaPrefab, bala, 2.0f)); // Pasamos el prefab y la bala del getObject
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            GameObject misil = ObjectPool.GetObject(misilPrefab);

            Rigidbody rb_2 = misilPrefab.GetComponent<Rigidbody>();

            //Add force to bullet
            rb_2.AddForce(transform.TransformDirection(0, 0, 1) * Speed);
            StartCoroutine(Recicle(misilPrefab, misil, 2.0f));
        }
    }

    IEnumerator Recicle(GameObject prefab, GameObject copiaPrefab, float time)
    {
        yield return new WaitForSeconds(time);
        ObjectPool.RecicleObject(prefab, copiaPrefab);
    }
}
