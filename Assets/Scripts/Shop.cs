using System;
using System.Collections.Generic;
using DefaultNamespace;
using DG.Tweening;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Shop : ManagerBase, IStart
{
    [Header("Money")] [SerializeField] private PunchScale _punchScale;
    [SerializeField] private TMP_Text _moneyCount;
    [SerializeField] private RectTransform _moneyTransfrom;
    [SerializeField] private int _startMoney = 100;

    [Header("ShopItems")] [SerializeField] private GameObject _buyButton;
    [SerializeField] private RectTransform _cellTransform;
    [SerializeField] private Image _mainShopItem;
    [SerializeField] private TMP_Text _cellPrice;
    [SerializeField] private List<ShopItem> _shopItems;

    private int _currentMoney;
    private Tween _tween;
    private CellMerger _cellMerger;

    public void OnStart()
    {
        _cellMerger = Toolbox.Get<CellMerger>();
        AddMoney(_startMoney);
        TryBuyNewItem();
    }

    [Button]
    public void AddMoney(int moneyCount)
    {
        if (moneyCount <= 0) return;
        _currentMoney += moneyCount;
        UpdateMoneyText();
    }

    public void TryBuyHealth()
    {
        if (_currentMoney - 100 < 0) return;
        var characterHealth = FindObjectOfType<Character>().Health;
        if (characterHealth == null || characterHealth.IsFull || characterHealth.IsDeath) return;
        characterHealth.ResetHealth();
        _currentMoney -= 100;
        UpdateMoneyText();
    }

    public void TryBuyNewItem()
    {
        if (_shopItems.Count <= 0) return;
        var item = _shopItems[0];
        if (_currentMoney - item.Price < 0 || _cellMerger.HavePlace == false) return;
        _currentMoney -= item.Price;
        UpdateMoneyText();
        _cellMerger.GetFreeCell().OccupiedCell(item.CellIcon, item.WeaponType);
        _shopItems.Remove(item);
        UpdateShopCell();
    }

    private void UpdateShopCell()
    {
        if (_shopItems.Count <= 0)
        {
            _buyButton.SetActive(false);
            return;
        }

        _punchScale.SetRectTransform(_cellTransform);
        _punchScale.GetTween().TryPlayTween(ref _tween);
        var shopItem = _shopItems[0];
        _mainShopItem.sprite = shopItem.CellIcon;
        _cellPrice.text = shopItem.Price.ToString();
    }

    private void UpdateMoneyText()
    {
        _moneyCount.text = _currentMoney.ToString();
        _punchScale.SetRectTransform(_moneyTransfrom);
        _punchScale.GetTween().TryPlayTween(ref _tween);
    }
}

[Serializable]
public class ShopItem
{
    public int Price => _price;
    public Sprite CellIcon => _cellIcon;
    public WeaponType WeaponType => _weaponType;

    [SerializeField] private int _price;
    [SerializeField] private Sprite _cellIcon;
    [SerializeField] private WeaponType _weaponType;
}