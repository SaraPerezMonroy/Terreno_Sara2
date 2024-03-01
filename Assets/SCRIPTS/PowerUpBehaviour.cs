using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class PowerUpBehaviour : MonoBehaviour
{
    [SerializeField]
    public GameObject boostSpawnLimit1;
    [SerializeField]
    public GameObject boostSpawnLimit2;
    [SerializeField]
    public float spawnDelay;    
    [SerializeField]
    public float rotateSpeed;

    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            
            float randomX = Random.Range(boostSpawnLimit1.transform.position.x, boostSpawnLimit2.transform.position.x);
            float randomY = Random.Range(boostSpawnLimit1.transform.position.y, boostSpawnLimit2.transform.position.y);
            float randomZ = Random.Range(boostSpawnLimit1.transform.position.z, boostSpawnLimit2.transform.position.z);
            rb.GetComponent<Renderer>().enabled = false; // Le desactivamos la malla
            GetComponent<BoxCollider>().enabled = false; // Le desactivamos el collider 
            StartCoroutine(SpawnTimer(spawnDelay));

            Vector3 randomRange = new Vector3(randomX, randomY, randomZ);
            transform.position = randomRange;
        }
    }
    void OnDrawGizmosSelected() // Para que marque la zona dentro de los dos puntos, solo se ve en la esccena
    {
        float boostSpawnX = boostSpawnLimit1.transform.position.x - boostSpawnLimit2.transform.position.x;
        float boostSpawnY = boostSpawnLimit1.transform.position.y - boostSpawnLimit2.transform.position.y;
        float boostSpawnZ = boostSpawnLimit1.transform.position.z - boostSpawnLimit2.transform.position.z;

        Vector3 centro = (boostSpawnLimit1.transform.position + boostSpawnLimit2.transform.position) * 0.5f;
        Vector3 tamaño = new Vector3(Mathf.Abs(boostSpawnX), Mathf.Abs(boostSpawnY), Mathf.Abs(boostSpawnZ));

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(centro, tamaño);
    }

    IEnumerator SpawnTimer(float time)
    {
        yield return new WaitForSeconds(time);
        rb.GetComponent<Renderer>().enabled = true; // Activamos la malla
        GetComponent<BoxCollider>().enabled = true; // Activamos collider
    }
}
