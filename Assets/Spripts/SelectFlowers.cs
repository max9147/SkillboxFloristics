using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectFlowers : MonoBehaviour
{
    private GameObject gameManager;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager");
    }

    private void OnMouseDown()
    {
        gameManager.GetComponent<EditFlowers>().SelectFlower(gameObject);
    }
}