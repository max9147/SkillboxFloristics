using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrangeFlowers : MonoBehaviour
{
    public Button buttonCreate;
    public bool flowerInside = false;

    private GameObject collisionFlower; //������ ������ ��� �������� � �����
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
                buttonCreate.interactable = true; //����� ������� ����� ���� ������ �������� � �����
                collisionFlower.GetComponent<SpriteRenderer>().sortingOrder = flowerCount; //�������� ������ ��������� ������ �� �������� ���� ������������ ����������
                collisionFlower = null;
            }
            return true;
        }
        else
        {
            return false;
        }
    }

    public int GetFlowerCount()
    {
        return flowerCount;
    }

    public void ClearBouquet()
    {
        flowerCount = 0;
    }
}