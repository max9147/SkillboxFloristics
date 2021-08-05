using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ArrangeBouquet : MonoBehaviour
{
    public GameObject positionArrow;
    public Button buttonCreate;
    public Button buttonReset;
    public Button buttonBack;
    public Slider sizeSlider;
    public TextMeshProUGUI amountText;

    private GameObject collisionFlower; //Цветок только что вошедший в букет
    private List<float> rotations = new List<float>();
    private int flowerCount; //Число цветков в букете
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
        flowerInside = true;
    }

    private void OnMouseExit()
    {
        flowerInside = false;
        if (collisionFlower)
        {
            collisionFlower.transform.rotation = Quaternion.identity; //Обнуляем вращение цветка при выходе из букета
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

    public bool CheckFlowerPos() //Проверка на нахождение цветка в области букета
    {
        if (flowerInside)
        {
            flowerCount++;
            if (flowerCount > 30) amountText.text = "Много цветов";
            else if (flowerCount >= 10)
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