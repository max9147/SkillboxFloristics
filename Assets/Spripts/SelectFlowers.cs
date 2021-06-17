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

    private void OnMouseEnter()
    {
        gameManager.GetComponent<EditFlowers>().SelectFlower(gameObject);
    }

    private void OnMouseExit()
    {
        gameManager.GetComponent<EditFlowers>().DisselectFlower();
    }

    private void OnMouseDown()
    {
        gameManager.GetComponent<EditFlowers>().StartEdit(gameObject);
    }
}