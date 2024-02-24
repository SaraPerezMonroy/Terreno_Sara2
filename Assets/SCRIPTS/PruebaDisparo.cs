using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PruebaDisparo : MonoBehaviour
{
    [SerializeField]
    GameObject balaPrefab;

    public float bulletSpeed = 100f;
    Vector3 impulso;

    // Start is called before the first frame update
    void Start()
    {
        ObjectPool.PreLoad(balaPrefab, 10);
        impulso = Vector3.forward * bulletSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonUp("Fire1"))
        {
            GameObject bala = ObjectPool.GetObject(balaPrefab); // Igualar nuestro gameobject al de la funci�n GetObject del object pool
            
            Rigidbody rb_bala = bala.GetComponent<Rigidbody>();
            bala.transform.position = transform.position; // Igualar posici�n de la bala al ca��n
            rb_bala.velocity = transform.forward * bulletSpeed; 
            StartCoroutine(Recicle(balaPrefab, bala, 2.0f)); // Reciclamos la bala, pasamos el prefab y la bala del getObject
        }
    }
 
    IEnumerator Recicle(GameObject prefab, GameObject copiaPrefab, float time) // Para llamar a la funci�n de reciclado del pool
    {
        yield return new WaitForSeconds(time);
        ObjectPool.RecicleObject(prefab, copiaPrefab);
    }
}
