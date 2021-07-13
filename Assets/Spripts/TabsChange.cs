using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TabsChange : MonoBehaviour
{
    public GameObject description;
    public GameObject content;
    public GameObject focusFlowers;
    public GameObject baseFlowers;
    public GameObject fillFlowers;
    public GameObject detailFlowers;
    public GameObject green;
    public GameObject[] buttons;
    public Sprite button;
    public Sprite buttonGreen;
    public Sprite buttonPressed;
    public Sprite buttonGreenPressed;
    public TextMeshProUGUI descriptionText;

    private GameObject toDestroy;

    public void ChangeTab(int id)
    {
        toDestroy = GameObject.FindGameObjectWithTag("Description");
        Destroy(toDestroy);
        content.GetComponent<AudioSource>().Play();
        content.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);

        foreach (var item in buttons)
            item.GetComponent<Image>().sprite = button;
        buttons[4].GetComponent<Image>().sprite = buttonGreen;
        if (id == 5) buttons[4].GetComponent<Image>().sprite = buttonGreenPressed;
        else buttons[id - 1].GetComponent<Image>().sprite = buttonPressed;

        switch (id)
        {
            case 1:                
                focusFlowers.SetActive(true);
                baseFlowers.SetActive(false);
                fillFlowers.SetActive(false);
                detailFlowers.SetActive(false);
                green.SetActive(false);
                content.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 1570);
                descriptionText.text = "�������� ����� - ��� ������� �������� � �������, ��� �������, ��� ��������� ��������� �������� ��� �������� ������������ " +
                    "�� ���� �������� ������� ��� ������ ������.";
                break;

            case 2:
                focusFlowers.SetActive(false);
                baseFlowers.SetActive(true);
                fillFlowers.SetActive(false);
                detailFlowers.SetActive(false);
                green.SetActive(false);
                content.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 3130);
                descriptionText.text = "������� ����� - �� ������ ������� � ������������� ������� ��������� ������, � �������� ������ ������. �� ���� ������ ����, ��� � �������� ������. " +
                    "���� � �� �� �������� ����� ��������� � ������ ��� �������� ��������, ��� � ��� �������, � ����������� �� ������ ������������. " +
                    "� ������� ������� �������� ����� ���������� �� ������ 3 - � ����� ��� ������ �������� � ������ �������� ������, ������������ �� �������� ��������.";
                break;

            case 3:
                focusFlowers.SetActive(false);
                baseFlowers.SetActive(false);
                fillFlowers.SetActive(true);
                detailFlowers.SetActive(false);
                green.SetActive(false);
                content.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 1050);
                descriptionText.text = "�������� � ������ ������� ��� � �������� ���������� �������, ���������� � ��������. ��� ��������� ������������ " +
                    "����� �������� ��������, ��������� � ����� ����������������.";
                break;

            case 4:
                focusFlowers.SetActive(false);
                baseFlowers.SetActive(false);
                fillFlowers.SetActive(false);
                detailFlowers.SetActive(true);
                green.SetActive(false);
                content.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 660);
                descriptionText.text = "��� �������, ��� �������� � ��������� �������� ������, �� ������ ����������� ������� ������, �� ��� ���� �� ��������� ������ ��������� ��������� " +
                    "������������� �������� �������. ������ �� ����������� ��� ����� ������� ������ � ����� �������� ����������.";
                break;

            case 5:
                focusFlowers.SetActive(false);
                baseFlowers.SetActive(false);
                fillFlowers.SetActive(false);
                detailFlowers.SetActive(false);
                green.SetActive(true);
                content.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 790);
                descriptionText.text = "������ � ������ ����� ��������� ����������� ���� (�������� �������, �������� ������� �������) ��� ������������ (���������� ����� " +
                    "��� ����� �������, ������). ��� �������� ����������� ������� ����������� �������� ��������� ����� ������, ������������ �� �������, ����� ��� ������ �����.";
                break;

            default:
                break;
        }
    }

    public void OpenDescription()
    {
        toDestroy = GameObject.FindGameObjectWithTag("Description");
        Destroy(toDestroy);
        content.GetComponent<AudioSource>().Play();
        if (description.activeInHierarchy) description.SetActive(false);
        else description.SetActive(true);
    }

    public bool CheckDescriptionOpen()
    {
        if (description.activeInHierarchy) return true;
        else return false;
    }
}