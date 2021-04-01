using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrangeFlowers : MonoBehaviour
{
    public GameObject collisionFlower; //������ ������ ��� �������� � �����
    public bool flowerInside = false;

    private int flowerCount; //����� ������� � ������

    private void OnTriggerEnter2D(Collider2D collision) //���������� ��� ������ ������ � ������
    {        
        collisionFlower = collision.gameObject;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {        
        collisionFlower = null;
    }

    private void OnMouseEnter() //���������� ��� ����������� ���������� ������ � �������
    {
        flowerInside = true;
    }

    private void OnMouseExit()
    {
        flowerInside = false;
        if (collisionFlower)
        {
            collisionFlower.transform.rotation = Quaternion.identity; //�������� �������� ������ ��� ������ �� ������
        }
    }

    public bool CheckFlower() //�������� �� ���������� ������ � ������� ������
    {
        if (flowerInside)
        {
            flowerCount++;
            if (collisionFlower)
            {
                collisionFlower.GetComponent<SpriteRenderer>().sortingOrder = flowerCount; //�������� ������ ��������� ������ �� �������� ���� ������������ ����������
            }
            return true;
        }
        else
        {
            return false;
        }
    }
}