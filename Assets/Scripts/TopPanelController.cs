using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TopPanelController : MonoBehaviour
{
    [SerializeField] private TMP_Text woodStockTXT;
    [SerializeField] private TMP_Text stoneStockTXT;
    [SerializeField] private TMP_Text foodStockTXT;
    [SerializeField] private TMP_Text herbStockTXT;
    private int _woodStock;
    private int _stoneStock;
    private int _foodStock;
    private int _herbStock;

    private void Start()
    {
        _woodStock = 200;
        _stoneStock = 200;
        _foodStock = 200;
        _herbStock = 200;
        UpdateTexts();
    }

    private void UpdateTexts()
    {
        woodStockTXT.text = _woodStock.ToString();
        stoneStockTXT.text = _stoneStock.ToString();
        foodStockTXT.text = _foodStock.ToString();
        herbStockTXT.text = _herbStock.ToString();
    }

    private void SetPopulation()
    {

    }

    public void SetStock(int newWoodCount, int newStoneCount, int newFoodCount, int newHerbCount)
    {
        _woodStock += newWoodCount;
        _stoneStock += newStoneCount;
        _foodStock += newFoodCount;
        _herbStock += newHerbCount;
        UpdateTexts();
    }

    public void Purchase(int wastedWoodCount, int wastedStoneCount)
    {
        _woodStock -= wastedWoodCount;
        _stoneStock -= wastedStoneCount;
        UpdateTexts();
    }

    public int GetHerbStock()
    {
        return _herbStock;
    }
    public int GetFoodStock()
    {
        return _foodStock;
    }
    public int GetMineStock()
    {
        return _stoneStock;
    }
    public int GetWoodStock()
    {
        return _woodStock;
    }
}
