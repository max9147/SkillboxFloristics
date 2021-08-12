using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CreateComp : MonoBehaviour
{
    public GameObject bouquet;
    public GameObject basket;
    public GameObject positionArrow;
    public GameObject[] flowerSpawers;
    public Button buttonCreate;
    public Button buttonReset;
    public Button buttonBack;
    public Slider sizeSlider;
    public TextMeshProUGUI amountText;

    private GameObject[] toDestroy;

    public void PressCreate() //������� �� ������ ������� �����
    {
        buttonCreate.GetComponent<AudioSource>().Play();
        GetComponent<EditFlowers>().StopEdit();
        buttonCreate.interactable = false; //��������� �������� �� �������� ������ ����� ��������
        buttonBack.interactable = false;
        GetComponent<ScoringSystem>().CountScore();
        if (bouquet.activeInHierarchy) bouquet.GetComponent<ArrangeBouquet>().ClearBouquet();
        if (basket.activeInHierarchy) basket.GetComponent<ArrangeBasket>().ClearBasket();
    }

    public void PressReset()
    {
        buttonReset.GetComponent<AudioSource>().Play();
        GetComponent<EditFlowers>().StopEdit();
        buttonCreate.interactable = false;
        buttonBack.interactable = false;
        sizeSlider.value = 0;
        amountText.text = "";
        positionArrow.transform.eulerAngles = new Vector3(0, 0, 0);
        if (bouquet.activeInHierarchy) bouquet.GetComponent<ArrangeBouquet>().ClearBouquet();
        if (basket.activeInHierarchy) basket.GetComponent<ArrangeBasket>().ClearBasket();
        GetComponent<ScoringSystem>().addedFlowers.Clear();
        GetComponent<ScoringSystem>().flowerColors.Clear();
        toDestroy = GameObject.FindGameObjectsWithTag("Flower");
        foreach (var item in toDestroy) //���������� ����� �� �����
        {
            Destroy(item);
        }
        foreach (var item in flowerSpawers)
        {
            item.GetComponent<DragFlowers>().AllowTaking();
        }
        GetComponent<SelectType>().InitSelection();
        GetComponent<ScoringSystem>().CheckColorMeter();
        GetComponent<LogSystem>().ClearLog();
    }

    public void PressRemove()
    {
        buttonBack.GetComponent<AudioSource>().Play();
        GetComponent<EditFlowers>().StopEdit();
        if ((bouquet.GetComponent<ArrangeBouquet>().GetFlowerCount() >= 1 && bouquet.GetComponent<ArrangeBouquet>().GetFlowerCount() <= 30) || (basket.GetComponent<ArrangeBasket>().GetFlowerCount() >= 1 && basket.GetComponent<ArrangeBasket>().GetFlowerCount() <= 30))
            sizeSlider.value -= 0.033f;         
        GetComponent<ScoringSystem>().RemoveFromScoring();
        GetComponent<LogSystem>().RemoveFromLog();
        toDestroy = GameObject.FindGameObjectsWithTag("Flower");
        if (bouquet.activeInHierarchy)
        {
            float rotation = bouquet.GetComponent<ArrangeBouquet>().GetRotation();
            if (rotation > 90) positionArrow.transform.eulerAngles = new Vector3(0, 0, 90);
            else if (rotation < -90) positionArrow.transform.eulerAngles = new Vector3(0, 0, -90);
            else positionArrow.transform.eulerAngles = new Vector3(0, 0, rotation);
            foreach (var item in toDestroy) //���������� ����� �� �����
            {
                if (item.GetComponent<SpriteRenderer>().sortingOrder == bouquet.GetComponent<ArrangeBouquet>().GetFlowerCount()) //������� �����
                {
                    Destroy(item);
                    bouquet.GetComponent<ArrangeBouquet>().RemoveFlower();
                }

                if (bouquet.GetComponent<ArrangeBouquet>().GetFlowerCount() == 0)
                {
                    buttonBack.interactable = false;
                }

                if (bouquet.GetComponent<ArrangeBouquet>().GetFlowerCount() < 10 || bouquet.GetComponent<ArrangeBouquet>().GetFlowerCount() > 30)
                {
                    buttonCreate.interactable = false;
                }
            }
        }
        if (basket.activeInHierarchy)
        {
            float rotation = basket.GetComponent<ArrangeBasket>().GetRotation();
            if (rotation > 90) positionArrow.transform.eulerAngles = new Vector3(0, 0, 90);
            else if (rotation < -90) positionArrow.transform.eulerAngles = new Vector3(0, 0, -90);
            else positionArrow.transform.eulerAngles = new Vector3(0, 0, rotation);
            foreach (var item in toDestroy) //���������� ����� �� �����
            {
                if (item.GetComponent<SpriteRenderer>().sortingOrder == basket.GetComponent<ArrangeBasket>().GetFlowerCount()) //������� �����
                {
                    Destroy(item);
                    basket.GetComponent<ArrangeBasket>().RemoveFlower();
                }

                if (basket.GetComponent<ArrangeBasket>().GetFlowerCount() == 0)
                {
                    buttonBack.interactable = false;
                }

                if (basket.GetComponent<ArrangeBasket>().GetFlowerCount() < 10 || basket.GetComponent<ArrangeBasket>().GetFlowerCount() > 30)
                {
                    buttonCreate.interactable = false;
                }
            }
        }

        if (bouquet.GetComponent<ArrangeBouquet>().GetFlowerCount() > 30 || basket.GetComponent<ArrangeBasket>().GetFlowerCount() > 30) amountText.text = "����� ������";
        else if (bouquet.GetComponent<ArrangeBouquet>().GetFlowerCount() >= 10 || basket.GetComponent<ArrangeBasket>().GetFlowerCount() >= 10 || (bouquet.GetComponent<ArrangeBouquet>().GetFlowerCount() == 0 && basket.GetComponent<ArrangeBasket>().GetFlowerCount() == 0)) amountText.text = "";
        else amountText.text = "���� ������";

        if ((bouquet.GetComponent<ArrangeBouquet>().GetFlowerCount() >= 10 && bouquet.GetComponent<ArrangeBouquet>().GetFlowerCount() <= 30) || (basket.GetComponent<ArrangeBasket>().GetFlowerCount() >= 10 && basket.GetComponent<ArrangeBasket>().GetFlowerCount() <= 30))
        {
            buttonCreate.interactable = true;
        }

        GetComponent<ScoringSystem>().CheckColorMeter();
    }
}