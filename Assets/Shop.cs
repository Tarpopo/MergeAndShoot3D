using System;
using System.Collections.Generic;
using DefaultNamespace;
using DG.Tweening;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

public class Shop : ManagerBase, IStart
{
    [SerializeField] private PunchScale _punchScale;
    [SerializeField] private TMP_Text _moneyCount;
    [SerializeField] private List<ShopItem> _shopItems;

    private int _currentMoney;
    private Tween _tween;
    private CellMerger _cellMerger;

    public void OnStart()
    {
        _cellMerger = Toolbox.Get<CellMerger>();
        AddMoney(250);
    }

    [Button]
    public void AddMoney(int moneyCount)
    {
        if (moneyCount <= 0) return;
        _currentMoney += moneyCount;
        UpdateMoneyText();
        _punchScale.GetTween().TryPlayTween(ref _tween);
    }

    public void TryBuyNewItem()
    {
        if (_shopItems.Count <= 0) return;
        var item = _shopItems[0];
        if (_currentMoney - item.Price < 0 || _cellMerger.HavePlace == false) return;
        _currentMoney -= item.Price;
        _cellMerger.GetFreeCell().OccupiedCell(item.ItemPrefab);
        _shopItems.Remove(item);
    }

    private void UpdateMoneyText() => _moneyCount.text = _currentMoney.ToString();
}

[Serializable]
public class ShopItem
{
    public int Price => _price;
    public GameObject ItemPrefab;
    [SerializeField] private int _price;
    [SerializeField] private GameObject _itemPrefab;
}