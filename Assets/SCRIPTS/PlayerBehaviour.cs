using System.Collections;
using System.Collections.Generic;
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


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>(); // Acceder al rigidbody
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        transform.position = new Vector3(480, 130, 110);
    }

    // Update is called once per frame
    void Update()
    {

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
       if(collision.gameObject.CompareTag("Terrain"))
        {
            transform.position = new Vector3(480, 130, 110);
            Debug.Log("Moriste");
        }
    }

}
