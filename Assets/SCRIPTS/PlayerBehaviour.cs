using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    Rigidbody rb;
    public float playerSpeed;
    public float initialSpeed;
    public float speedIncrease;
    public float speedDecrease;
    public float maxSpeed;
    public float minSpeed;

    public Vector2 turnMovement;
    public float mouseSensitivity;
    public float height;

    [SerializeField]
    public GameObject spawn;

    [SerializeField]
    TextMeshProUGUI speedLabel;
    [SerializeField]
    TextMeshProUGUI heightLabel;


    private float deathTimer = 0f;
    public bool deathTimerBegin;
    [SerializeField]
    GameObject UIPlayer;
    [SerializeField]
    GameObject crosshair;

    [SerializeField]
    public GameObject VFXPlayer;

    public ParticleSystem deathExplosion;

    [SerializeField]
    public AudioSource explosionSound;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>(); 
        Cursor.visible = false; // Para que no se vea el cursor en la pantalla
        Cursor.lockState = CursorLockMode.Locked; // Para que el cursor esté en el medio 
        transform.position = new Vector3(480, 130, 110);
        
    }

    // Update is called once per frame
    void Update()
    {
        height = GetComponent<Transform>().position.y;
        speedLabel.text = playerSpeed.ToString("00") + " kts"; // Kts son nudos en inglés :)
        heightLabel.text = height.ToString("00") + " miles";
        transform.position += transform.forward * playerSpeed * Time.deltaTime;
        turnMovement.x += Input.GetAxis("Mouse X") * mouseSensitivity;
        turnMovement.y += Input.GetAxis("Mouse Y") * mouseSensitivity;
        transform.localRotation = Quaternion.Euler(-turnMovement.y, turnMovement.x, 0);

        if(playerSpeed < maxSpeed)
        {
            if (Input.GetKey(KeyCode.W))
            {
                playerSpeed += speedIncrease * Time.deltaTime;
            }
        }
        if (playerSpeed > minSpeed)
        {
            if (Input.GetKey(KeyCode.S))
            {
                playerSpeed -= speedDecrease * Time.deltaTime;
            }
        }

        if(deathTimerBegin)
        {
            deathTimer += Time.deltaTime;
            playerSpeed = 0;
            if (deathTimer >= 2)
            {
                deathTimer = 0;
                deathTimerBegin = false;
                RespawnPlayer();
            }
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Terrain") || collision.gameObject.tag == "Enemy")
        {
            deathTimerBegin = true;
            rb.GetComponent<Renderer>().enabled = false; // Le desactivamos la malla
            rb.isKinematic = true; // Le quitamos físicas
            deathExplosion.Play();
            deathExplosion.transform.position = transform.position;

            crosshair.SetActive(false);
            UIPlayer.SetActive(false);
            VFXPlayer.SetActive(false);
            explosionSound.Play();
        }
    }

    private void RespawnPlayer()
    {
        transform.position = spawn.transform.position;
        transform.rotation = spawn.transform.rotation;
        rb.isKinematic = false;
        rb.GetComponent<Renderer>().enabled = true;

        crosshair.SetActive(true);
        UIPlayer.SetActive(true);
        VFXPlayer.SetActive(true);
        playerSpeed = initialSpeed;
    }
}