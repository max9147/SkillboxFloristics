using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragFlowers : MonoBehaviour
{
    public Camera mainCam;
    public GameObject flowerPrefab; //���������� ������ � ������� ��������
    public GameObject bouquet;

    private GameObject flowerToDrag;
    private bool isDragging = false; //���� �� � ������ � ������� ������
    private bool canTake = true; //����� �� ����� ������

    private void OnMouseDown()
    {
        if (canTake)
        {
            isDragging = true;
            canTake = false;
            Vector3 spawnPos = new Vector3(mainCam.ScreenToWorldPoint(Input.mousePosition).x, mainCam.ScreenToWorldPoint(Input.mousePosition).y, 0); //���������� �������, �� z=0
            flowerToDrag = Instantiate(flowerPrefab, spawnPos, Quaternion.identity); //����� ������ � ������� ������� � ������ ��� � ����������
        }
    }

    private void OnMouseUp()
    {
        isDragging = false;
        flowerToDrag.GetComponent<Rigidbody2D>().simulated = false;

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
                Vector3 curPos = new Vector3(mainCam.ScreenToWorldPoint(Input.mousePosition).x, mainCam.ScreenToWorldPoint(Input.mousePosition).y, 0); //���������� �������, �� z=0
                flowerToDrag.transform.position = Vector3.MoveTowards(flowerToDrag.transform.position, curPos, 100); //���������� ������ � �������
            }
            else
            {
                flowerToDrag.transform.position = Vector3.MoveTowards(flowerToDrag.transform.position, transform.position, 0.5f); //���������� ������ � ��� ��������
            }

            if (flowerToDrag.transform.position == transform.position) //����� ������ ������� �� ��������
            {
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