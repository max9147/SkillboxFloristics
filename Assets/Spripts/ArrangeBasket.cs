using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ArrangeBasket : MonoBehaviour
{
    public GameObject positionArrow;
    public Button buttonCreate;
    public Button buttonReset;
    public Button buttonBack;
    public Slider sizeSlider;
    public TextMeshProUGUI amountText;

    private GameObject collisionFlower; //������ ������ ��� �������� � �����
    private List<float> rotations = new List<float>();
    private int flowerCount; //����� ������� � �������
    private float positionValue = 0;
    private float lastRotation = 0;
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

    public void ClearBasket()
    {
        flowerCount = 0;
        positionValue = 0;
    }

    public void RemoveFlower()
    {
        flowerCount--;
    }

    public int GetFlowerCount()
    {
        return flowerCount;
    }

    public float GetRotation()
    {
        lastRotation = rotations[rotations.Count - 1];
        rotations.RemoveAt(rotations.Count - 1);
        positionValue -= lastRotation;
        return positionValue;
    }

    public float GetRotation(int id)
    {
        lastRotation = rotations[id];
        rotations.RemoveAt(id);
        positionValue -= lastRotation;
        return positionValue;
    }

    public bool CheckFlowerPos() //�������� �� ���������� ������ � ������� �������
    {
        if (flowerInside)
        {
            flowerCount++;
            if (flowerCount > 30) amountText.text = "����� ������";
            else if (flowerCount >= 10)
            {
                sizeSlider.value += 0.033f;
                amountText.text = "";
                buttonCreate.interactable = true;
            }
            else
            {
                sizeSlider.value += 0.033f;
                amountText.text = "���� ������";
            }
            if (collisionFlower)
            {
                rotations.Add((collisionFlower.transform.position.x - transform.position.x) * -40);
                positionValue -= (collisionFlower.transform.position.x - transform.position.x) * 40;
                Vector3 arrowRotation = positionArrow.transform.eulerAngles;
                if (positionValue > 90) arrowRotation.z = 90;
                else if (positionValue < -90) arrowRotation.z = -90;
                else arrowRotation.z = positionValue;
                positionArrow.transform.eulerAngles = arrowRotation;
                buttonReset.interactable = true;
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
