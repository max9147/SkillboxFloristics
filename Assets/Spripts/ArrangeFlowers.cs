using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrangeFlowers : MonoBehaviour
{
    public GameObject bouquetTip; //������ ����� ������

    private GameObject collisionFlower; //������ ������ ��� �������� � �����
    private int flowerCount; //����� ������� � ������
    private bool flowerInside = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        flowerInside = true;
        collisionFlower = collision.gameObject;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        flowerInside = false;
        collisionFlower = null;
    }

    public bool CheckFlower() //�������� �� ���������� ������ � ������� ������
    {
        if (flowerInside)
        {
            collisionFlower.transform.position = CalcPosition(); //��������� ������� ������ � ������
            collisionFlower.transform.up = (bouquetTip.transform.position - collisionFlower.transform.position) * -1; //��������� ������� ������ � ������
            flowerCount++;
            collisionFlower.GetComponent<SpriteRenderer>().sortingOrder = flowerCount; //�������� ������ ��������� ������ �� �������� ���� ������������ ����������
            GetComponent<SpriteRenderer>().sortingOrder = flowerCount + 1; //�������� ����� �� �������� ���� ������������ ������
            return true;
        }
        else
        {
            return false;
        }
    }

    private Vector3 CalcPosition() //��������� ������� ������ � ������
    {
        if (flowerCount == 0)
        {
            return new Vector3(0, 0, 0);
        }
        else
        {
            return new Vector3(collisionFlower.transform.position.x, 0, 0);
        }
    }
}
