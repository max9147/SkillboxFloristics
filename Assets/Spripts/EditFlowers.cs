using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditFlowers : MonoBehaviour
{
    private void OnMouseDown()
    {
        if (GetComponent<SpriteRenderer>().flipY == false)
        {
            GetComponent<SpriteRenderer>().flipY = true;
        }
        else
        {
            GetComponent<SpriteRenderer>().flipY = false;
        }
    }
}