using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TabsChange : MonoBehaviour
{
    public GameObject description;
    public GameObject content;
    public GameObject soundButton;
    public GameObject focusFlowers;
    public GameObject baseFlowers;
    public GameObject fillFlowers;
    public GameObject detailFlowers;
    public GameObject green;
    public TextMeshProUGUI descriptionText;

    public void ChangeTab(int id)
    {
        content.GetComponent<AudioSource>().Play();
        content.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
        switch (id)
        {
            case 1:
                focusFlowers.SetActive(true);
                baseFlowers.SetActive(false);
                fillFlowers.SetActive(false);
                detailFlowers.SetActive(false);
                green.SetActive(false);
                content.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 900);
                descriptionText.text = "�������� ����� - ��� ������� �������� � �������, ��� �������, ��� ��������� ��������� �������� ��� �������� ������������ " +
                    "�� ���� �������� ������� ��� ������ ������.";
                break;

            case 2:
                focusFlowers.SetActive(false);
                baseFlowers.SetActive(true);
                fillFlowers.SetActive(false);
                detailFlowers.SetActive(false);
                green.SetActive(false);
                content.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 900);
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
                content.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 400);
                descriptionText.text = "�������� � ������ ������� ��� � �������� ���������� �������, ���������� � ��������. ��� ��������� ������������ " +
                    "����� �������� ��������, ��������� � ����� ����������������.";
                break;

            case 4:
                focusFlowers.SetActive(false);
                baseFlowers.SetActive(false);
                fillFlowers.SetActive(false);
                detailFlowers.SetActive(true);
                green.SetActive(false);
                content.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 650);
                descriptionText.text = "��� �������, ��� �������� � ��������� �������� ������, �� ������ ����������� ������� ������, �� ��� ���� �� ��������� ������ ��������� ��������� " +
                    "������������� �������� �������. ������ �� ����������� ��� ����� ������� ������ � ����� �������� ����������.";
                break;

            case 5:
                focusFlowers.SetActive(false);
                baseFlowers.SetActive(false);
                fillFlowers.SetActive(false);
                detailFlowers.SetActive(false);
                green.SetActive(true);
                content.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 780);
                descriptionText.text = "������ � ������ ����� ��������� ����������� ���� (�������� �������, �������� ������� �������) ��� ������������ (���������� ����� " +
                    "��� ����� �������, ������). ��� �������� ����������� ������� ����������� �������� ��������� ����� ������, ������������ �� �������, ����� ��� ������ �����.";
                break;

            default:
                break;
        }
    }

    public void OpenDescription()
    {
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