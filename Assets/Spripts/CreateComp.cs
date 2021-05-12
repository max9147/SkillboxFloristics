using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CreateComp : MonoBehaviour
{
    public GameObject bouquet;
    public GameObject basket;
    public GameObject[] flowerSpawers;
    public TextMeshProUGUI textLast; //����� ������������ ���������� � ��������� ������
    public Button buttonCreate;
    public Button buttonReset;
    public Button buttonBack;

    private GameObject[] toDestroy;
    private int countFlowers = 0; //����� ������� � ��������� ������

    public void PressCreate() //������� �� ������ ������� �����
    {
        buttonCreate.interactable = false; //��������� �������� �� �������� ������ ����� ��������
        buttonReset.interactable = false;
        buttonBack.interactable = false;
        if (bouquet.activeInHierarchy)
        {
            bouquet.GetComponent<ArrangeBouquet>().ClearBouquet();
        }
        if (basket.activeInHierarchy)
        {
            basket.GetComponent<ArrangeBasket>().ClearBasket();
        }
        toDestroy = GameObject.FindGameObjectsWithTag("Flower");
        foreach (var item in toDestroy) //���������� ����� �� �����
        {
            countFlowers++;
            Destroy(item);
        }
        textLast.text = $"�������: {countFlowers}";
        countFlowers = 0;
        GetComponent<SelectType>().InitSelection();
    }

    public void PressReset()
    {
        buttonCreate.interactable = false;
        buttonReset.interactable = false;
        buttonBack.interactable = false;
        if (bouquet.activeInHierarchy)
        {
            bouquet.GetComponent<ArrangeBouquet>().ClearBouquet();
        }
        if (basket.activeInHierarchy)
        {
            basket.GetComponent<ArrangeBasket>().ClearBasket();
        }
        toDestroy = GameObject.FindGameObjectsWithTag("Flower");
        foreach (var item in toDestroy) //���������� ����� �� �����
        {
            Destroy(item);
        }
        foreach (var item in flowerSpawers)
        {
            item.GetComponent<DragFlowers>().AllowTaking();
        }
    }
}