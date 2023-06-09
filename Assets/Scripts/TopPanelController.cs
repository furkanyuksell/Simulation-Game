using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TopPanelController : MonoBehaviour
{
    public static TopPanelController Instance { get; set; }
    [SerializeField] private TMP_Text woodStockTXT;
    [SerializeField] private TMP_Text stoneStockTXT;
    [SerializeField] private TMP_Text foodStockTXT;
    [SerializeField] private TMP_Text herbStockTXT;


    [SerializeField] private TMP_Text happyCountTXT;
    [SerializeField] private TMP_Text sadCountTXT;
    [SerializeField] private TMP_Text populationCountTXT;
    private int _woodStock;
    private int _stoneStock;
    private int _foodStock;
    private int _herbStock;

    private int popoulationCount = 3;
    private int happyCount = 3;
    private int sadCount = 0;


    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _woodStock = 5000;
        _stoneStock = 5000;
        _foodStock = 5000;
        _herbStock = 5000;
        UpdateTexts();

        populationCountTXT.text = popoulationCount.ToString();
        happyCountTXT.text = happyCount.ToString();
        sadCountTXT.text = "0";
    }

    private void UpdateTexts()
    {
        woodStockTXT.text = _woodStock.ToString();
        stoneStockTXT.text = _stoneStock.ToString();
        foodStockTXT.text = _foodStock.ToString();
        herbStockTXT.text = _herbStock.ToString();
    }

    public void TurnVillagerHappy()
    {
        happyCount++;
        sadCount--;
        happyCountTXT.text = happyCount.ToString();
        sadCountTXT.text = sadCount.ToString();

    }

    public void TurnVillagerSad()
    {
        happyCount--;
        sadCount++;
        happyCountTXT.text = happyCount.ToString();
        sadCountTXT.text = sadCount.ToString();
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
