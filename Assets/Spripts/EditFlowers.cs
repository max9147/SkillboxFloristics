using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditFlowers : MonoBehaviour
{
    private GameObject prevFlower;
    private string prevLayer;
    private bool isEditing = false;

    public void SelectFlower(GameObject flower)
    {
        if (flower.GetComponent<SpriteRenderer>().sortingLayerName == "Default" || isEditing) return;
        prevFlower = flower;
        prevLayer = flower.GetComponent<SpriteRenderer>().sortingLayerName;
        flower.GetComponent<SpriteRenderer>().sortingLayerName = "Foreground";        
    }

    public void DisselectFlower()
    {
        if (prevFlower && !isEditing) prevFlower.GetComponent<SpriteRenderer>().sortingLayerName = prevLayer;
    }

    public void StartEdit(GameObject flower)
    {
        isEditing = false;
        DisselectFlower();
        SelectFlower(flower);
        isEditing = true;
    }

    public void StopEdit()
    {
        isEditing = false;
        DisselectFlower();
    }
}