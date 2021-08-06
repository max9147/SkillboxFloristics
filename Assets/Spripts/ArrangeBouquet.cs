using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ArrangeBouquet : MonoBehaviour
{
    public GameObject positionArrow;
    public Button buttonCreate;
    public Button buttonBack;
    public Slider sizeSlider;
    public TextMeshProUGUI amountText;

    private GameObject collisionFlower; //������ ������ ��� �������� � �����
    private List<float> rotations = new List<float>();
    private int flowerCount; //����� ������� � ������
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
        if (collisionFlower)
        {
            collisionFlower.transform.rotation = Quaternion.identity; //�������� �������� ������ ��� ������ �� ������
        }
    }    

    public void ClearBouquet()
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

    public bool CheckFlowerPos() //�������� �� ���������� ������ � ������� ������
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
                float rotateMult = 1;
                switch (collisionFlower.GetComponent<SelectFlowers>().type)
                {
                    case "Focus":
                        rotateMult = 1.5f;
                        break;
                    case "Base":
                        rotateMult = 1;
                        break;
                    case "Fill":
                        rotateMult = 0.7f;
                        break;
                    case "Details":
                        rotateMult = 0.5f;
                        break;
                    case "Green":
                        rotateMult = 0.3f;
                        break;
                    default:
                        break;
                }
                rotations.Add((collisionFlower.transform.position.x - transform.position.x) * -40 * rotateMult);
                positionValue -= (collisionFlower.transform.position.x - transform.position.x) * 40 * rotateMult;
                Vector3 arrowRotation = positionArrow.transform.eulerAngles;
                if (positionValue > 90) arrowRotation.z = 90;
                else if (positionValue < -90) arrowRotation.z = -90;
                else arrowRotation.z = positionValue;
                positionArrow.transform.eulerAngles = arrowRotation;
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