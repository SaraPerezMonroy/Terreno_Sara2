using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    Rigidbody rb;
    public float playerSpeed;
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
    }

    private void OnCollisionEnter(Collision collision)
    {
       if(collision.gameObject.CompareTag("Terrain") || collision.gameObject.CompareTag("Enemy"))
        {
            transform.position = spawn.transform.position;
        }
    }

}
