using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DragFlowers : MonoBehaviour
{
    public Camera mainCam;
    public GameObject flowerPrefab; //���������� ������ � ������� ��������
    public GameObject bouquet;
    public GameObject bouquetTip;
    public Button buttonCreate;

    private GameObject flowerToDrag;
    private bool isDragging = false; //���� �� � ������ � ������� ������
    private bool canTake = true; //����� �� ����� ������

    private void OnMouseDown()
    {
        if (canTake)
        {
            buttonCreate.interactable = false; //��������� ��������� ����� ���� ����� ������
            isDragging = true;
            canTake = false;
            Vector3 spawnPos = new Vector3(mainCam.ScreenToWorldPoint(Input.mousePosition).x, mainCam.ScreenToWorldPoint(Input.mousePosition).y, 0); //���������� �������, �� z=0
            flowerToDrag = Instantiate(flowerPrefab, spawnPos, Quaternion.identity); //����� ������ � ������� ������� � ������ ��� � ����������
        }
    }

    private void OnMouseUp()
    {
        isDragging = false;

        if (bouquet.GetComponent<ArrangeFlowers>().CheckFlower()) //�������� �� ���������� ������ � ������� ������
        {
            ReleaseFlower();
        }
    }

    private void FixedUpdate()
    {
        if (flowerToDrag)
        {
            if (isDragging) //����������� ����� ����� ����� ������
            {
                Vector3 curPos = new Vector3(mainCam.ScreenToWorldPoint(Input.mousePosition).x, mainCam.ScreenToWorldPoint(Input.mousePosition).y, 0); //������� ������� �������

                if (bouquet.GetComponent<ArrangeFlowers>().flowerInside)
                {
                    Vector3 snappedPos = new Vector3(curPos.x / 2, bouquet.transform.position.y, 0);
                    flowerToDrag.transform.position = Vector3.MoveTowards(flowerToDrag.transform.position, snappedPos, 10); //���������� ������ � �����
                    flowerToDrag.transform.up = (bouquetTip.transform.position - flowerToDrag.transform.position) * -1; //������ ������ ������� ������ ������
                }
                else
                {
                    flowerToDrag.transform.position = Vector3.MoveTowards(flowerToDrag.transform.position, curPos, 0.5f); //���������� ������ � �������
                }
            }
            else
            {
                flowerToDrag.transform.position = Vector3.MoveTowards(flowerToDrag.transform.position, transform.position, 0.5f); //���������� ������ � ��� ��������
            }

            if (flowerToDrag.transform.position == transform.position) //����� ������ ������� �� ��������
            {
                if (bouquet.GetComponent<ArrangeFlowers>().GetFlowerCount() > 0)
                {
                    buttonCreate.interactable = true; //����� ������� ����� ���� ������ ������������ �� �����, � � ������ ���� �����
                }
                canTake = true;
                Destroy(flowerToDrag);
            }
        }
    }

    public void ReleaseFlower() //������� ������ ������������ � ������
    {
        flowerToDrag = null;
        canTake = true;
    }
}