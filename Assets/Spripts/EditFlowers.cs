using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditFlowers : MonoBehaviour
{
    public GameObject highlight;

    public void SelectFlower(GameObject flower)
    {
        Destroy(GameObject.Find("Highlight(Clone)"));

        Instantiate(highlight, flower.transform.position,flower.transform.rotation);
    }

    public void Disselect()
    {
        Destroy(GameObject.Find("Highlight(Clone)"));
    }
}
