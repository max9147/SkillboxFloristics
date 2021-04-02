using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrangeFlowers : MonoBehaviour
{
    public Button buttonCreate;
    public bool flowerInside = false;

    private GameObject collisionFlower; //Цветок только что вошедший в букет
    private int flowerCount; //Число цветков в букете

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
        if (collisionFlower)
        {
            collisionFlower.transform.rotation = Quaternion.identity; //Обнуляем вращение цветка при выходе из букета
        }
    }

    public bool CheckFlower() //Проверка на нахождение цветка в области букета
    {
        if (flowerInside)
        {
            flowerCount++;
            if (collisionFlower)
            {
                buttonCreate.interactable = true; //Можно создать букет если цветок опустили в букет
                collisionFlower.GetComponent<SpriteRenderer>().sortingOrder = flowerCount; //Сдвигаем каждый следующий цветок на передний план относительно предыдущих
                collisionFlower = null;
            }
            return true;
        }
        else
        {
            return false;
        }
    }

    public int GetFlowerCount()
    {
        return flowerCount;
    }

    public void ClearBouquet()
    {
        flowerCount = 0;
    }
}