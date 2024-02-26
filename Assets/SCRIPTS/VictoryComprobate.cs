using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class VictoryComprobate : MonoBehaviour
{
    public GameObject victoryScreen;
    public GameObject player;

    [SerializeField]
    TextMeshProUGUI enemiesLeftLabel;

    // Update is called once per frame
    void Update()
    {
        enemiesLeftLabel.text = transform.childCount.ToString("00") + " left";

        if (transform.childCount == 0 && victoryScreen.activeSelf == false)
        {
            victoryScreen.SetActive(true);
            player.SetActive(false);
        }
    }
 }