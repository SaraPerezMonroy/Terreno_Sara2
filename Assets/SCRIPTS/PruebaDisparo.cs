using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prueba_Disparo : MonoBehaviour
{
    [SerializeField]
    GameObject balaTipo1;
    [SerializeField]
    GameObject balaTipo2;

    [SerializeField]
    GameObject cannonR;
    [SerializeField]
    GameObject cannonL;

    int Speed = 130;


    // Start is called before the first frame update
    void Start()
    {
        ObjectPool.PreLoad(balaTipo1, 5);
        ObjectPool.PreLoad(balaTipo2, 7);


    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetButtonUp("Fire1"))
        {
            GameObject Bala1 = ObjectPool.GetObject(balaTipo1);


            Rigidbody rb = balaTipo1.GetComponent<Rigidbody>();
            //Add force to bullet
            rb.AddForce(transform.TransformDirection(0, 0, 1) * Speed);
            StartCoroutine(Recicle(balaTipo1, Bala1, 2.0f)); // Pasamos el prefab y la bala del getObject
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            GameObject Bala2 = ObjectPool.GetObject(balaTipo2);

            Rigidbody rb_2 = balaTipo2.GetComponent<Rigidbody>();

            //Add force to bullet
            rb_2.AddForce(transform.TransformDirection(0, 0, 1) * Speed);
            StartCoroutine(Recicle(balaTipo2, Bala2, 2.0f));
        }
    }

    // Estructura usar métodos pool

    // Object pool.metodoAUsar

    IEnumerator Recicle(GameObject primitive, GameObject go, float time)
    {
        yield return new WaitForSeconds(time);
        ObjectPool.RecicleObject(primitive, go);
    }
}
