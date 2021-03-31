using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrangeFlowers : MonoBehaviour
{
    public GameObject bouquetTip; //Нижний конец букета

    private GameObject collisionFlower; //Цветок только что вошедший в букет
    private int flowerCount; //Число цветков в букете
    private bool flowerInside = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        flowerInside = true;
        collisionFlower = collision.gameObject;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        flowerInside = false;
        collisionFlower = null;
    }

    public bool CheckFlower() //Проверка на нахождение цветка в области букета
    {
        if (flowerInside)
        {
            collisionFlower.transform.position = CalcPosition(); //Вычисляем позицию цветка в букете
            collisionFlower.transform.up = (bouquetTip.transform.position - collisionFlower.transform.position) * -1; //Вычисляем поворот цветка в букете
            flowerCount++;
            collisionFlower.GetComponent<SpriteRenderer>().sortingOrder = flowerCount; //Сдвигаем каждый следующий цветок на передний план относительно предыдущих
            GetComponent<SpriteRenderer>().sortingOrder = flowerCount + 1; //Сдвигаем букет на передний план относительно цветов
            return true;
        }
        else
        {
            return false;
        }
    }

    private Vector3 CalcPosition() //Вычисляем позицию цветка в букете
    {
        if (flowerCount == 0)
        {
            return new Vector3(0, 0, 0);
        }
        else
        {
            return new Vector3(collisionFlower.transform.position.x, 0, 0);
        }
    }
}
