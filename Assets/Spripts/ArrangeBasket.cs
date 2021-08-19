using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ArrangeBasket : MonoBehaviour
{
    public GameObject positionArrow;
    public GameObject gameManager;
    public Button buttonCreate;
    public Button buttonBack;
    public Slider sizeSlider;
    public TextMeshProUGUI amountText;   

    private GameObject collisionFlower; //Цветок только что вошедший в букет
    private List<float> rotations = new List<float>();
    private int flowerCount; //Число цветков в корзине
    private float positionValue = 0;
    private float lastRotation = 0;
    private bool flowerInside = false;

    private void OnTriggerEnter2D(Collider2D collision) //Используем для записи цветка в объект
    {
        collisionFlower = collision.gameObject;        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        collisionFlower = null;
    }

    private void OnMouseEnter() //Используем для обозначения нахождения цветка в объекте
    {
        if (gameManager.GetComponent<SelectType>().CheckIsOpen()) return;
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

    public bool CheckFlowerPos() //Проверка на нахождение цветка в области корзины
    {
        if (flowerInside)
        {
            flowerCount++;
            if (flowerCount > 30) amountText.text = "Много цветов";
            else if (flowerCount >= 5)
            {
                sizeSlider.value += 0.033f;
                amountText.text = "";
                buttonCreate.interactable = true;
            }
            else
            {
                sizeSlider.value += 0.033f;
                amountText.text = "Мало цветов";
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
