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
    public GameObject flowerPrefab; //?????????? ?????? ? ??????? ????????
    public GameObject bouquet;
    public GameObject bouquetTip;
    public GameObject basket;
    public GameObject basketTip;
    public GameObject content; //??????? ???????? scrollview
    public GameObject desctiptionMenu;
    public GameObject gameManager;
    public GameObject editCanvas;
    public Button buttonCreate;
    public Button buttonBack;

    private GameObject flowerToDrag;
    private GameObject curMenu;
    private Vector3 snappedPos;
    private Ray ray;
    private float contentPos;
    private bool isDragging = false; //???? ?? ? ?????? ? ??????? ??????
    private bool canTake = true; //????? ?? ????? ??????

    private void OnMouseDown()
    {
        ray = mainCam.ScreenPointToRay(Input.mousePosition); //???????? ?? ?????????? ??????? ? content
        if (canTake && !gameManager.GetComponent<ScoringSystem>().CheckIsOpen() && !gameManager.GetComponent<SelectType>().CheckIsOpen() && !gameManager.GetComponent<TabsChange>().CheckDescriptionOpen() && ray.origin.y < 2.7f && ray.origin.y > -1.7f)
        {
            editCanvas.GetComponent<Canvas>().sortingLayerName = "Background";
            Destroy(curMenu);
            gameManager.GetComponent<EditFlowers>().StopEdit();
            bouquet.transform.position = new Vector3(bouquet.transform.position.x, bouquet.transform.position.y, -0.01f);
            basket.transform.position = new Vector3(basket.transform.position.x, basket.transform.position.y, -0.01f);
            GetComponent<AudioSource>().Play();
            contentPos = content.GetComponent<RectTransform>().localPosition.y; //????????? content ?? ????? ?????? ??????
            buttonCreate.interactable = false; //????????? ????????? ?????????? ???? ????? ??????
            gameManager.GetComponent<EditFlowers>().isDragging = true;
            isDragging = true;
            canTake = false;
            Vector3 spawnPos = new Vector3(mainCam.ScreenToWorldPoint(Input.mousePosition).x, mainCam.ScreenToWorldPoint(Input.mousePosition).y, 0); //?????????? ???????, ?? z=0
            flowerToDrag = Instantiate(flowerPrefab, spawnPos, Quaternion.identity); //????? ?????? ? ??????? ??????? ? ?????? ??? ? ??????????
        }
    }

    private void OnMouseUp()
    {
        editCanvas.GetComponent<Canvas>().sortingLayerName = "Menus";
        if (!flowerToDrag) return;
        isDragging = false;
        gameManager.GetComponent<EditFlowers>().isDragging = false;        
        if (bouquet.GetComponent<ArrangeBouquet>().CheckFlowerPos() || basket.GetComponent<ArrangeBasket>().CheckFlowerPos()) ReleaseFlower();
        bouquet.transform.position = new Vector3(bouquet.transform.position.x, bouquet.transform.position.y, 0.01f);
        basket.transform.position = new Vector3(basket.transform.position.x, basket.transform.position.y, 0.01f);
    }

    private void OnMouseEnter()
    {
        ray = mainCam.ScreenPointToRay(Input.mousePosition); //???????? ?? ?????????? ??????? ? content
        if (ray.origin.y > 2.7f || ray.origin.y < -1.7f) return;
        if (gameManager.GetComponent<TabsChange>().CheckDescriptionOpen() || gameManager.GetComponent<ScoringSystem>().CheckIsOpen() || gameManager.GetComponent<SelectType>().CheckIsOpen() || gameManager.GetComponent<EditFlowers>().isDragging) return;
        curMenu = Instantiate(desctiptionMenu, content.transform);
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
            content.GetComponent<RectTransform>().localPosition = new Vector3(0, contentPos, 0); //?????????? content ? ????????? ? ??????? ?? ??? ??? ?????? ??????
        }
    }

    private void FixedUpdate()
    {
        if (flowerToDrag)
        {
            if (isDragging) //??????????? ????? ????? ????? ??????
            {
                Vector3 curPos = new Vector3(mainCam.ScreenToWorldPoint(Input.mousePosition).x, mainCam.ScreenToWorldPoint(Input.mousePosition).y, 0); //??????? ??????? ???????

                if (bouquet.GetComponent<ArrangeBouquet>().CheckFlowerInside())
                {
                    flowerToDrag.GetComponent<SpriteRenderer>().sortingLayerName = "Flowers"; //?????????? ?????? ?? ??? ????
                    flowerToDrag.GetComponent<SelectFlowers>().type = flower.size;
                    flowerToDrag.GetComponent<SpriteRenderer>().sortingOrder = bouquet.GetComponent<ArrangeBouquet>().GetFlowerCount() + 1;
                    flowerToDrag.GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.VisibleInsideMask;

                    snappedPos = new Vector3(curPos.x, (curPos.y + 1) / 2, 0); //??????? ????? ?????? ? ??????
                    float deviation = snappedPos.x - bouquet.transform.position.x;
                    float coeff;

                    switch (bouquet.GetComponent<ArrangeBouquet>().bouquetID)
                    {
                        case 0:
                            coeff = Mathf.Clamp(snappedPos.y - bouquet.transform.position.y, 0, 2) / 2;
                            snappedPos.x -= deviation * (1 - coeff);
                            break;
                        case 2:
                            coeff = Mathf.Clamp(snappedPos.y - bouquet.transform.position.y, 0, 3) / 3;
                            snappedPos.x -= deviation * (1 - coeff * 0.7f);
                            break;
                        default:
                            coeff = Mathf.Clamp(snappedPos.y - bouquet.transform.position.y, 0, 3) / 3;
                            snappedPos.x -= deviation * (1 - coeff * 0.9f);
                            break;
                    }                    

                    flowerToDrag.transform.position = snappedPos;
                    flowerToDrag.transform.up = (bouquetTip.transform.position - flowerToDrag.transform.position) * -1; //?????? ?????? ??????? ?????? ??????

                    if (bouquet.transform.position.x - flowerToDrag.transform.position.x > 0.35f) //???? ?????? ? ????? ????? ??????
                    {
                        flowerToDrag.GetComponent<SpriteRenderer>().sprite = flower.imageL;
                    }
                    else if (bouquet.transform.position.x - flowerToDrag.transform.position.x < -0.35f) //???? ?????? ? ?????? ????? ??????
                    {
                        flowerToDrag.GetComponent<SpriteRenderer>().sprite = flower.imageR;
                    }
                    else //???? ?????? ? ?????? ??????
                    {
                        flowerToDrag.GetComponent<SpriteRenderer>().sprite = flower.imageC;
                    }
                }
                else if (basket.GetComponent<ArrangeBasket>().CheckFlowerInside())
                {
                    flowerToDrag.GetComponent<SpriteRenderer>().sortingLayerName = "Flowers"; //?????????? ?????? ?? ??? ????
                    flowerToDrag.GetComponent<SelectFlowers>().type = flower.size;
                    flowerToDrag.GetComponent<SpriteRenderer>().sortingOrder = basket.GetComponent<ArrangeBasket>().GetFlowerCount() + 1;
                    flowerToDrag.GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.VisibleInsideMask;

                    snappedPos = new Vector3(curPos.x, (curPos.y / 2) + 0.1f, 0); //??????? ????? ?????? ? ???????
                    float deviation = snappedPos.x - bouquet.transform.position.x;

                    if (deviation > 1.1f)
                    {
                        snappedPos.x -= (deviation - 1.1f) / 2;
                        flowerToDrag.transform.up = (new Vector3(1.1f - deviation, -1, 0)) * -1;
                    }
                    else if (deviation < -1.1f)
                    {
                        snappedPos.x -= (1.1f + deviation) / 2;
                        flowerToDrag.transform.up = (new Vector3(-1.1f - deviation, -1, 0)) * -1;
                    }
                    else flowerToDrag.transform.up = Vector3.up;

                    flowerToDrag.transform.position = snappedPos;
                }
                else
                {
                    flowerToDrag.GetComponent<SpriteRenderer>().sortingLayerName = "Default"; //?????????? ?????? ?? ??????????? ???? ??? ????????? ?? ??????
                    flowerToDrag.GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.None;
                    flowerToDrag.GetComponent<SpriteRenderer>().sprite = flower.imageC;
                    flowerToDrag.transform.rotation = Quaternion.identity; //???????? ???????? ??????
                    flowerToDrag.transform.position = curPos;
                }
            }
            else
            {
                flowerToDrag.transform.position = Vector3.MoveTowards(flowerToDrag.transform.position, transform.position, 0.5f); //?????????? ?????? ? ??? ????????
            }

            if (flowerToDrag.transform.position == transform.position) //????? ?????? ??????? ?? ????????
            {
                canTake = true;
                Destroy(flowerToDrag);
                if ((bouquet.GetComponent<ArrangeBouquet>().GetFlowerCount() >= 5 && bouquet.GetComponent<ArrangeBouquet>().GetFlowerCount() <= 30) || (basket.GetComponent<ArrangeBasket>().GetFlowerCount() >= 5 && basket.GetComponent<ArrangeBasket>().GetFlowerCount() <= 30))
                {
                    buttonCreate.interactable = true;
                }
            }
        }
    }

    private void ReleaseFlower() //??????? ?????? ???????????? ? ??????
    {
        gameManager.GetComponent<LogSystem>().AddFlowerToLog(flower);
        gameManager.GetComponent<ScoringSystem>().AddFlower(flowerToDrag);
        gameManager.GetComponent<ScoringSystem>().AddColor(flower.color);
        flowerToDrag = null;
        canTake = true;
        buttonBack.interactable = true;
        gameManager.GetComponent<ScoringSystem>().CheckColorMeter();
    }

    public void AllowTaking()
    {
        canTake = true;
    }
}