using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CreateBouquet : MonoBehaviour
{
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
        GetComponent<ArrangeFlowers>().ClearBouquet();
        toDestroy = GameObject.FindGameObjectsWithTag("Flower");
        foreach (var item in toDestroy) //���������� ����� �� �����
        {
            countFlowers++;
            Destroy(item);
        }
        textLast.text = $"�������: {countFlowers}";
        countFlowers = 0;
    }

    public void PressReset()
    {
        buttonCreate.interactable = false;
        buttonReset.interactable = false;
        buttonBack.interactable = false;
        GetComponent<ArrangeFlowers>().ClearBouquet();
        toDestroy = GameObject.FindGameObjectsWithTag("Flower");
        foreach (var item in toDestroy) //���������� ����� �� �����
        {
            countFlowers++;
            Destroy(item);
        }
        foreach (var item in flowerSpawers)
        {
            item.GetComponent<DragFlowers>().AllowTaking();
        }
    }
}