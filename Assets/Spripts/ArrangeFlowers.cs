using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrangeFlowers : MonoBehaviour
{
    public Button buttonCreate;
    public Button buttonReset;
    public Button buttonBack;

    private GameObject collisionFlower; //Цветок только что вошедший в букет
    private GameObject[] toDestroy;
    private int flowerCount; //Число цветков в букете
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
        if (collisionFlower)
        {
            collisionFlower.transform.rotation = Quaternion.identity; //Обнуляем вращение цветка при выходе из букета
        }
    }    

    public void ClearBouquet()
    {
        flowerCount = 0;
    }

    public void RemoveLast()
    {
        toDestroy = GameObject.FindGameObjectsWithTag("Flower");
        foreach (var item in toDestroy) //Перебираем цветы на сцене
        {
            if (item.GetComponent<SpriteRenderer>().sortingOrder == flowerCount)
            {
                Destroy(item);
                flowerCount--;
            }

            if (flowerCount == 0)
            {
                buttonCreate.interactable = false;
                buttonReset.interactable = false;
                buttonBack.interactable = false;
            }
        }
    }

    public int GetFlowerCount()
    {
        return flowerCount;
    }

    public bool CheckFlowerPos() //Проверка на нахождение цветка в области букета
    {
        if (flowerInside)
        {
            flowerCount++;
            if (collisionFlower)
            {
                buttonCreate.interactable = true; //Можно создать букет если цветок опустили в букет
                buttonReset.interactable = true;
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

    public bool CheckFlowerInside()
    {
        return flowerInside;
    }
}