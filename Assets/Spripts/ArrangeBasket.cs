using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrangeBasket : MonoBehaviour
{
    public Button buttonCreate;
    public Button buttonReset;
    public Button buttonBack;

    private GameObject collisionFlower; //������ ������ ��� �������� � �����
    private GameObject[] toDestroy;
    private int flowerCount; //����� ������� � �������
    private bool flowerInside = false;

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
    }

    public void ClearBouquet()
    {
        flowerCount = 0;
    }

    public void RemoveLast()
    {
        toDestroy = GameObject.FindGameObjectsWithTag("Flower");
        foreach (var item in toDestroy) //���������� ����� �� �����
        {
            if (item.GetComponent<SpriteRenderer>().sortingOrder == flowerCount) //������� �����
            {
                Destroy(item);
                flowerCount--;
            }

            if (flowerCount == 0)
            {
                buttonCreate.interactable = false;
                buttonReset.interactable = false;
                buttonBack.interactable = false;
            }
        }
    }

    public int GetFlowerCount()
    {
        return flowerCount;
    }

    public bool CheckFlowerPos() //�������� �� ���������� ������ � ������� �������
    {
        if (flowerInside)
        {
            flowerCount++;
            if (collisionFlower)
            {
                buttonCreate.interactable = true; //����� ������� ����� ���� ������ �������� � �������
                buttonReset.interactable = true;
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

    public bool CheckFlowerInside()
    {
        return flowerInside;
    }
}
