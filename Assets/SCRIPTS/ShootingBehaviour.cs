using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShootingBehaviour : MonoBehaviour
{
    [SerializeField]
    GameObject balaPrefab;

    public float bulletSpeed = 100f;
    Vector3 impulso;

    [SerializeField]
    public AudioSource blasterSound;

    public bool canShoot;

    [SerializeField]
    public float shootCoolDown;

    [SerializeField]
    public int bulletAmount;

    [SerializeField]
    public float autoReloadTime;

    [SerializeField]
    public float reloadTime;

    [SerializeField]
    TextMeshProUGUI bulletLabel;

    public bool isReloading;



    // Start is called before the first frame update
    void Start()
    {
        ObjectPool.PreLoad(balaPrefab, 10);
        impulso = Vector3.forward * bulletSpeed;
        canShoot = true;
    }

    // Update is called once per frame
    void Update()
    {
        bulletLabel.text = bulletAmount.ToString() + "/15";
        Debug.Log(bulletAmount);

        if(Input.GetButton("Fire1") && canShoot && !isReloading)
        {            
            GameObject bala = ObjectPool.GetObject(balaPrefab); // Igualar nuestro gameobject al de la funci�n GetObject del object pool
            Rigidbody rb_bala = bala.GetComponent<Rigidbody>();
            blasterSound.Play();
            bala.transform.position = transform.position; // Igualar posici�n de la bala al ca��n
            rb_bala.velocity = transform.forward * bulletSpeed;
            StartCoroutine(Recicle(balaPrefab, bala, 1.5f)); // Reciclamos la bala, pasamos el prefab y la bala del getObject

            bulletAmount--;
            canShoot = false;
            StartCoroutine(ShootTimer(shootCoolDown));
            if (bulletAmount == 0)
            {
                isReloading = true;
                StartCoroutine(Reload(autoReloadTime));
            }
        }


        if (Input.GetKeyDown(KeyCode.R))
        {
            isReloading = true;
            StartCoroutine(Reload(reloadTime));
        }

        if(isReloading)
        {
            bulletLabel.text = "RELOADING...";
        }


    }

    IEnumerator Recicle(GameObject prefab, GameObject copiaPrefab, float time) // Para llamar a la funci�n de reciclado del pool
    {
        yield return new WaitForSeconds(time);
        ObjectPool.RecicleObject(prefab, copiaPrefab);
    }


    IEnumerator ShootTimer(float shootCoolDown)
    {
        yield return new WaitForSeconds(shootCoolDown);
        canShoot=true;
    }
    IEnumerator Reload(float reloadTime) 
    {
        yield return new WaitForSeconds(reloadTime);
        bulletAmount = 15;
        isReloading = false;
    }
}
