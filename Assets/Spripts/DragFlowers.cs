using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragFlowers : MonoBehaviour
{
    public Camera mainCam;
    public GameObject flowerPrefab; //Конкретный цветок у каждого спавнера
    public GameObject bouquet;

    private GameObject flowerToDrag;
    private bool isDragging = false; //Есть ли у игрока в курсоре цветок
    private bool canTake = true; //Можно ли взять цветок

    private void OnMouseDown()
    {
        if (canTake)
        {
            isDragging = true;
            canTake = false;
            Vector3 spawnPos = new Vector3(mainCam.ScreenToWorldPoint(Input.mousePosition).x, mainCam.ScreenToWorldPoint(Input.mousePosition).y, 0); //Координаты курсора, но z=0
            flowerToDrag = Instantiate(flowerPrefab, spawnPos, Quaternion.identity); //Спавн цветка в позиции курсора и запись его в переменную
        }
    }

    private void OnMouseUp()
    {
        isDragging = false;
        flowerToDrag.GetComponent<Rigidbody2D>().simulated = false;

        if (bouquet.GetComponent<ArrangeFlowers>().CheckFlower()) //Проверка на нахождение цветка в области букета
        {
            ReleaseFlower();
        }
    }

    private void FixedUpdate()
    {
        if (flowerToDrag)
        {
            if (isDragging) //Выполняется когда игрок тащит цветок
            {
                Vector3 curPos = new Vector3(mainCam.ScreenToWorldPoint(Input.mousePosition).x, mainCam.ScreenToWorldPoint(Input.mousePosition).y, 0); //Координаты курсора, но z=0
                flowerToDrag.transform.position = Vector3.MoveTowards(flowerToDrag.transform.position, curPos, 100); //Перемещаем цветок к курсору
            }
            else
            {
                flowerToDrag.transform.position = Vector3.MoveTowards(flowerToDrag.transform.position, transform.position, 0.5f); //Перемещаем цветок к его спавнеру
            }

            if (flowerToDrag.transform.position == transform.position) //Когда цветок долетел до спавнера
            {
                canTake = true;
                Destroy(flowerToDrag);
            }
        }
    }

    public void ReleaseFlower() //Считаем цветок используемым в букете
    {
        flowerToDrag = null;
        canTake = true;
    }
}