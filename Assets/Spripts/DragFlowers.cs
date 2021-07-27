using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragFlowers : MonoBehaviour
{
    public Camera mainCam;
    public Flower flower;
    public GameObject flowerPrefab; //Конкретный цветок у каждого спавнера
    public GameObject bouquet;
    public GameObject bouquetTip;
    public GameObject basket;
    public GameObject basketTip;
    public GameObject content; //Контент элемента scrollview
    public GameObject desctiptionMenu;
    public GameObject gameManager;
    public Button buttonCreate;
    public Button buttonBack;

    private GameObject flowerToDrag;
    private GameObject curMenu;
    private Vector3 snappedPos;
    private Ray ray;
    private float contentPos;
    private bool isDragging = false; //Есть ли у игрока в курсоре цветок
    private bool canTake = true; //Можно ли взять цветок

    private void OnMouseDown()
    {
        ray = mainCam.ScreenPointToRay(Input.mousePosition); //Проверка на нахождение курсора в content
        if (canTake && !gameManager.GetComponent<ScoringSystem>().CheckIsOpen() && !gameManager.GetComponent<SelectType>().CheckIsOpen() && !gameManager.GetComponent<TabsChange>().CheckDescriptionOpen() && ray.origin.y < 2.7f && ray.origin.y > -1.7f)
        {
            Destroy(curMenu);
            gameManager.GetComponent<EditFlowers>().StopEdit();
            bouquet.transform.position = new Vector3(bouquet.transform.position.x, bouquet.transform.position.y, -0.01f);
            basket.transform.position = new Vector3(basket.transform.position.x, basket.transform.position.y, -0.01f);
            GetComponent<AudioSource>().Play();
            contentPos = content.GetComponent<RectTransform>().localPosition.y; //Положение content во время взятия цветка
            buttonCreate.interactable = false; //Запрещаем создавать композицию пока тащим цветок
            gameManager.GetComponent<EditFlowers>().isDragging = true;
            isDragging = true;
            canTake = false;
            Vector3 spawnPos = new Vector3(mainCam.ScreenToWorldPoint(Input.mousePosition).x, mainCam.ScreenToWorldPoint(Input.mousePosition).y, 0); //Координаты курсора, но z=0
            flowerToDrag = Instantiate(flowerPrefab, spawnPos, Quaternion.identity); //Спавн цветка в позиции курсора и запись его в переменную
        }
    }

    private void OnMouseUp()
    {
        if (!flowerToDrag) return;
        isDragging = false;
        gameManager.GetComponent<EditFlowers>().isDragging = false;        
        if (bouquet.GetComponent<ArrangeBouquet>().CheckFlowerPos() || basket.GetComponent<ArrangeBasket>().CheckFlowerPos()) ReleaseFlower();
        bouquet.transform.position = new Vector3(bouquet.transform.position.x, bouquet.transform.position.y, 0.01f);
        basket.transform.position = new Vector3(basket.transform.position.x, basket.transform.position.y, 0.01f);
    }

    private void OnMouseEnter()
    {
        if (transform.position.y <= -1.8f || transform.position.y >= 2.5f) return;
        if (gameManager.GetComponent<TabsChange>().CheckDescriptionOpen() || gameManager.GetComponent<ScoringSystem>().CheckIsOpen() || gameManager.GetComponent<SelectType>().CheckIsOpen() || gameManager.GetComponent<EditFlowers>().isDragging) return;
        curMenu = Instantiate(desctiptionMenu, transform);
        curMenu.transform.Find("FlowerNameText").GetComponent<TextMeshProUGUI>().text = flower.flowerName;
        if (transform.position.y >= -0.85f) curMenu.transform.position = transform.position - new Vector3(0, 0.6f, 0);
        else
        {
            curMenu.transform.position = transform.position + new Vector3(0, 0.6f, 0);
            curMenu.transform.Find("Background").localScale = new Vector3(1, -1, 1);
        }
    }

    private void OnMouseExit()
    {
        Destroy(curMenu);
    }

    private void Update()
    {
        if (flowerToDrag)
        {
            content.GetComponent<RectTransform>().localPosition = new Vector3(0, contentPos, 0); //Перемещаем content в положение в котором он был при взятии цветка
        }
    }

    private void FixedUpdate()
    {
        if (flowerToDrag)
        {
            if (isDragging) //Выполняется когда игрок тащит цветок
            {
                Vector3 curPos = new Vector3(mainCam.ScreenToWorldPoint(Input.mousePosition).x, mainCam.ScreenToWorldPoint(Input.mousePosition).y, 0); //Текущая позиция курсора

                if (bouquet.GetComponent<ArrangeBouquet>().CheckFlowerInside())
                {
                    flowerToDrag.GetComponent<SpriteRenderer>().sortingLayerName = flower.size; //Перемещаем цветок на его слой
                    flowerToDrag.GetComponent<SelectFlowers>().type = flowerToDrag.GetComponent<SpriteRenderer>().sortingLayerName;
                    flowerToDrag.GetComponent<SpriteRenderer>().sortingOrder = bouquet.GetComponent<ArrangeBouquet>().GetFlowerCount() + 1;
                    flowerToDrag.GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.VisibleInsideMask;

                    if (flower.size == "Details")
                    {
                        snappedPos = new Vector3(curPos.x, curPos.y + 1, 0); //Находим место цветка в букете
                    }
                    else
                    {
                        snappedPos = new Vector3((bouquet.transform.position.x + curPos.x) / 2, (bouquet.transform.position.y + curPos.y) / 2, 0); //Находим место цветка в букете
                    }

                    flowerToDrag.transform.position = snappedPos;
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
                else if (basket.GetComponent<ArrangeBasket>().CheckFlowerInside())
                {
                    flowerToDrag.GetComponent<SpriteRenderer>().sortingLayerName = flower.size; //Перемещаем цветок на его слой
                    flowerToDrag.GetComponent<SelectFlowers>().type = flowerToDrag.GetComponent<SpriteRenderer>().sortingLayerName;
                    flowerToDrag.GetComponent<SpriteRenderer>().sortingOrder = basket.GetComponent<ArrangeBasket>().GetFlowerCount() + 1;
                    flowerToDrag.GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
                    flowerToDrag.transform.up = (basketTip.transform.position - flowerToDrag.transform.position) * -1; //Задаем цветку поворот внутри корзины
                    snappedPos = new Vector3((basket.transform.position.x + curPos.x) / 2, curPos.y+2, 0); //Находим место цветка в корзине
                    flowerToDrag.transform.position = snappedPos;
                }
                else
                {
                    flowerToDrag.GetComponent<SpriteRenderer>().sortingLayerName = "Default"; //Перемещаем цветок на стандартный слой при вынимании из букета
                    flowerToDrag.GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.None;
                    flowerToDrag.GetComponent<SpriteRenderer>().sprite = flower.imageC;
                    flowerToDrag.transform.rotation = Quaternion.identity; //Обнуляем вращение цветка
                    flowerToDrag.transform.position = curPos;
                }
            }
            else
            {
                flowerToDrag.transform.position = Vector3.MoveTowards(flowerToDrag.transform.position, transform.position, 0.5f); //Перемещаем цветок к его спавнеру
            }

            if (flowerToDrag.transform.position == transform.position) //Когда цветок долетел до спавнера
            {
                canTake = true;
                Destroy(flowerToDrag);
                if ((bouquet.GetComponent<ArrangeBouquet>().GetFlowerCount() >= 10 && bouquet.GetComponent<ArrangeBouquet>().GetFlowerCount() <= 30) || (basket.GetComponent<ArrangeBasket>().GetFlowerCount() >= 10 && basket.GetComponent<ArrangeBasket>().GetFlowerCount() <= 30))
                {
                    buttonCreate.interactable = true;
                }
            }
        }
    }

    private void ReleaseFlower() //Считаем цветок используемым в букете
    {
        gameManager.GetComponent<ScoringSystem>().CheckColorMeter();
        gameManager.GetComponent<ScoringSystem>().AddFlower(flowerToDrag);
        if (flower.size != "Green")
            gameManager.GetComponent<ScoringSystem>().AddColor(flower.color);
        flowerToDrag = null;
        canTake = true;
        buttonBack.interactable = true;
    }

    public void AllowTaking()
    {
        canTake = true;
    }
}