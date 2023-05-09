using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftPanelController : MonoBehaviour
{
    [System.Serializable]
    public class CraftElementData
    {
        public string name;
        public GameObject craftElement;
    }

    private RectTransform _rt;
    [SerializeField] private CraftElementData[] workShopElementList;
    [SerializeField] private CraftElementData[] furnitureElementList;
    [SerializeField] private CraftElementData[] constructionElementList;
    [SerializeField] private CraftElement[] craftElementBTNList;

    
    private bool isEnable = true;
    private bool isCraftElementsEnabled = true;

    private void Awake()
    {
        _rt = GetComponent<RectTransform>();
    }

    private void MoveRight()
    {
        _rt.anchoredPosition += new Vector2(110,0);
    }
    public void OpenCraftMenu()
    {
        if(isEnable)
        {
            MoveRight();
            isEnable = false;
        }
    }

    public void CloseCraftMenu()
    {
        _rt.anchoredPosition = Vector2.zero;
        isEnable = true;
        isCraftElementsEnabled = true; 
    }

    public void OpenWorkshopMenu()
    {
        SetMenu(workShopElementList);
    }
    public void OpenFurnitureMenu()
    {
        SetMenu(furnitureElementList);
    }
    public void OpenConstructionMenu()
    {
        SetMenu(constructionElementList);
    }

    public void SetMenu(CraftElementData[] craftElementOfList)
    {
        if(isCraftElementsEnabled)
        {
            MoveRight();
            isCraftElementsEnabled = false;
        }
        int i = 0;
        while(i<craftElementOfList.Length)
        {
            //craftElementBTNList[i].SetData(() => {Instantiate(craftElementOfList[i].craftElement,Vector3.zero,Quaternion.identity);},craftElementOfList[i].name);
            craftElementBTNList[i].SetData(null,craftElementOfList[i].name);
            i++;
        }
        while(i<craftElementBTNList.Length)
        {
            craftElementBTNList[i].gameObject.SetActive(false);
            i++;
        }
    }

}
