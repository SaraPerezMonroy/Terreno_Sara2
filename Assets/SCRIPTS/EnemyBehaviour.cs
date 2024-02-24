using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
    public Transform[] waypoints; // Array con los puntos a los que ir
    int currentWaypointIndex;
    public float enemySpeed = 20f;

    [SerializeField]
    public ParticleSystem explosion;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Transform waypointTarget = waypoints[currentWaypointIndex];
        Vector3 waypointDirection = (waypointTarget.position - transform.position).normalized;
        transform.position += waypointDirection * enemySpeed * Time.deltaTime;
        transform.LookAt(waypointTarget); // Para hacer que mire al waypoint al que tiene que ir
        
        if (Vector3.Distance(transform.position, waypointTarget.position) < 0.5f) // Si la nave llega al waypoint actual, pasa al siguiente waypoint. Distance pide 2 valores, tenemos posición actual y posición del siguiente waypoint
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        explosion.transform.position = transform.position;
        explosion.Play();
        Destroy(gameObject);
    }
}
