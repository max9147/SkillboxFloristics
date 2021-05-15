using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrangeBasket : MonoBehaviour
{
    public Button buttonCreate;
    public Button buttonReset;
    public Button buttonBack;

    private GameObject collisionFlower; //Цветок только что вошедший в букет
    private int flowerCount; //Число цветков в корзине
    private bool flowerInside = false;

    private void OnTriggerEnter2D(Collider2D collision) //Используем для записи цветка в объект
    {
        collisionFlower = collision.gameObject;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        collisionFlower = null;
    }

    private void OnMouseEnter() //Используем для обозначения нахождения цветка в объекте
    {
        flowerInside = true;
    }

    private void OnMouseExit()
    {
        flowerInside = false;
    }

    public void ClearBasket()
    {
        flowerCount = 0;
    }

    public void RemoveLast()
    {
        flowerCount--;
    }

    public int GetFlowerCount()
    {
        return flowerCount;
    }

    public bool CheckFlowerPos() //Проверка на нахождение цветка в области корзины
    {
        if (flowerInside)
        {
            flowerCount++;
            if (flowerCount >= 20 && flowerCount <= 35)
            {
                buttonCreate.interactable = true;
            }
            if (collisionFlower)
            {                
                buttonReset.interactable = true;
                collisionFlower = null;
            }
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool CheckFlowerInside()
    {
        return flowerInside;
    }
}
