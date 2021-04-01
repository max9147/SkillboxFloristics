using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrangeFlowers : MonoBehaviour
{
    public GameObject collisionFlower; //Цветок только что вошедший в букет
    public bool flowerInside = false;

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
                collisionFlower.GetComponent<SpriteRenderer>().sortingOrder = flowerCount; //Сдвигаем каждый следующий цветок на передний план относительно предыдущих
            }
            return true;
        }
        else
        {
            return false;
        }
    }
}