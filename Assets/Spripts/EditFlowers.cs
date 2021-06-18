using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EditFlowers : MonoBehaviour
{
    public GameObject editMenu;
    public GameObject canvas;    

    private GameObject prevFlower;
    private GameObject curMenu;
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
        if (curMenu) Destroy(curMenu);
        isEditing = false;
        DisselectFlower();
        SelectFlower(flower);
        isEditing = true;
        curMenu = Instantiate(editMenu, canvas.transform);
        curMenu.transform.position = flower.transform.position - new Vector3(2, 1, 0);
        curMenu.transform.Find("ExitButton").GetComponentInChildren<Button>().onClick.AddListener(delegate { this.StopEdit(); });
        curMenu.transform.Find("RemoveButton").GetComponentInChildren<Button>().onClick.AddListener(delegate { this.RemoveFlower(flower); });
    }

    public void StopEdit()
    {
        isEditing = false;
        DisselectFlower();
        Destroy(curMenu);
    }

    public void RemoveFlower(GameObject flower)
    {
        isEditing = false;
        Destroy(flower);
        Destroy(curMenu);
    }
}