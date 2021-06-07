using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditFlowers : MonoBehaviour
{
    public GameObject highlight;

    private GameObject prevFlower;
    private string prevLayer;

    public void SelectFlower(GameObject flower)
    {
        if (flower.GetComponent<SpriteRenderer>().sortingLayerName == "Default") return;
        prevFlower = flower;
        prevLayer = flower.GetComponent<SpriteRenderer>().sortingLayerName;
        flower.GetComponent<SpriteRenderer>().sortingLayerName = "Foreground";        
    }

    public void DisselectFlower()
    {
        if (prevFlower) prevFlower.GetComponent<SpriteRenderer>().sortingLayerName = prevLayer;
    }
}