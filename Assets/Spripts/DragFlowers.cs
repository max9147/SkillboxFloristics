using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DragFlowers : MonoBehaviour
{
    public Camera mainCam;
    public Flower flower;
    public GameObject flowerPrefab; //Конкретный цветок у каждого спавнера
    public GameObject bouquet;
    public GameObject bouquetTip;
    public Button buttonCreate;
    public Button buttonBack;

    private GameObject flowerToDrag;
    private bool isDragging = false; //Есть ли у игрока в курсоре цветок
    private bool canTake = true; //Можно ли взять цветок

    private void OnMouseDown()
    {
        if (canTake)
        {
            buttonCreate.interactable = false; //Запрещаем создавать букет пока тащим цветок
            isDragging = true;
            canTake = false;
            Vector3 spawnPos = new Vector3(mainCam.ScreenToWorldPoint(Input.mousePosition).x, mainCam.ScreenToWorldPoint(Input.mousePosition).y, 0); //Координаты курсора, но z=0
            flowerToDrag = Instantiate(flowerPrefab, spawnPos, Quaternion.identity); //Спавн цветка в позиции курсора и запись его в переменную
        }
    }

    private void OnMouseUp()
    {
        isDragging = false;

        if (bouquet.GetComponent<ArrangeFlowers>().CheckFlowerPos()) //Проверка на нахождение цветка в области букета
        {
            flowerToDrag.GetComponent<SpriteRenderer>().sortingLayerName = flower.size;
            ReleaseFlower();
        }
    }

    private void FixedUpdate()
    {
        if (flowerToDrag)
        {
            if (isDragging) //Выполняется когда игрок тащит цветок
            {
                Vector3 curPos = new Vector3(mainCam.ScreenToWorldPoint(Input.mousePosition).x, mainCam.ScreenToWorldPoint(Input.mousePosition).y, 0); //Текущая позиция курсора

                if (bouquet.GetComponent<ArrangeFlowers>().CheckFlowerInside())
                {
                    Vector3 snappedPos = new Vector3((bouquet.transform.position.x + curPos.x) / 2, (bouquet.transform.position.y + curPos.y) / 3, 0); //Находим место цветка в букете
                    flowerToDrag.transform.position = Vector3.MoveTowards(flowerToDrag.transform.position, snappedPos, 10); //Перемещаем цветок в букет
                    flowerToDrag.transform.up = (bouquetTip.transform.position - flowerToDrag.transform.position) * -1; //Задаем цветку поворот внутри букета

                    if (bouquet.transform.position.x - flowerToDrag.transform.position.x > 0.35f) //Если цветок в левой части букета
                    {
                        flowerToDrag.GetComponent<SpriteRenderer>().sprite = flower.imageL;
                    }
                    else if (bouquet.transform.position.x - flowerToDrag.transform.position.x < -0.35f) //Если цветок в правой части букета
                    {
                        flowerToDrag.GetComponent<SpriteRenderer>().sprite = flower.imageR;
                    }
                    else //Если цветок в центре букета
                    {
                        flowerToDrag.GetComponent<SpriteRenderer>().sprite = flower.imageC;
                    }
                }
                else
                {
                    flowerToDrag.transform.position = Vector3.MoveTowards(flowerToDrag.transform.position, curPos, 0.5f); //Перемещаем цветок к курсору
                    flowerToDrag.GetComponent<SpriteRenderer>().sprite = flower.imageC;
                }
            }
            else
            {
                flowerToDrag.transform.position = Vector3.MoveTowards(flowerToDrag.transform.position, transform.position, 0.5f); //Перемещаем цветок к его спавнеру
            }

            if (flowerToDrag.transform.position == transform.position) //Когда цветок долетел до спавнера
            {
                if (bouquet.GetComponent<ArrangeFlowers>().GetFlowerCount() > 0)
                {
                    buttonCreate.interactable = true; //Можем создать букет если цветок возвращается на место, а в букете есть цветы
                }
                canTake = true;
                Destroy(flowerToDrag);
            }
        }
    }

    private void ReleaseFlower() //Считаем цветок используемым в букете
    {
        flowerToDrag = null;
        canTake = true;
        buttonBack.interactable = true;
    }

    public void AllowTaking()
    {
        canTake = true;
    }
}