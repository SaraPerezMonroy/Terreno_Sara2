using System.Collections;
using TMPro;
using UnityEngine;

public class ShootingBehaviour : MonoBehaviour
{
    [SerializeField]
    GameObject bulletPrefab;
    public float bulletSpeed = 100f;
    Vector3 impulse;

    [SerializeField]
    public AudioSource blasterSound;

    public bool canShoot;
    [SerializeField]
    public float shootCoolDown;
    [SerializeField]
    public int bulletAmount;
    [SerializeField]
    public float reloadTime;
    [SerializeField]
    public float manualReloadTime;
    [SerializeField]
    TextMeshProUGUI bulletLabel;
    public bool isReloading;

    [SerializeField]
    public AudioSource reloadingSound;

    // Start is called before the first frame update
    void Start()
    {
        ObjectPool.PreLoad(bulletPrefab, 10);
        impulse = Vector3.forward * bulletSpeed;
        canShoot = true;
    }

    // Update is called once per frame
    void Update()
    {
        bulletLabel.text = bulletAmount.ToString() + "/15";

        if(Input.GetButton("Fire1") && canShoot && !isReloading)
        {            
            GameObject bullet = ObjectPool.GetObject(bulletPrefab); // Igualar nuestro gameobject al de la función GetObject del object pool
            Rigidbody rb_bala = bullet.GetComponent<Rigidbody>();
            bullet.transform.position = transform.position; // Igualar posición de la bala al cañón
            rb_bala.velocity = transform.forward * bulletSpeed;
            StartCoroutine(Recicle(bulletPrefab, bullet, 1.5f)); // Reciclamos la bala, pasamos el prefab y la bala del getObject
            blasterSound.Play();

            bulletAmount--;
            canShoot = false;
            StartCoroutine(ShootTimer(shootCoolDown));
            if (bulletAmount == 0)
            {
                isReloading = true;
                StartCoroutine(Reload(reloadTime));
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            isReloading = true;
            StartCoroutine(Reload(manualReloadTime));
        }
        if(isReloading)
        {
            bulletLabel.text = "RELOADING...";
        }
    }

    IEnumerator Recicle(GameObject prefab, GameObject prefabCopy, float time) // Para llamar a la función de reciclado del pool
    {
        yield return new WaitForSeconds(time);
        ObjectPool.RecicleObject(prefab, prefabCopy);
    }


    IEnumerator ShootTimer(float shootCoolDown)
    {
        yield return new WaitForSeconds(shootCoolDown);
        canShoot=true;
    }
    IEnumerator Reload(float reloadTime) 
    {
        reloadingSound.Play();
        yield return new WaitForSeconds(reloadTime);
        bulletAmount = 15;
        isReloading = false;
        reloadingSound.Stop();
    }
}
