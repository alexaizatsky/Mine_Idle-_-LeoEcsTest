using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct playerDataComponent
{
    private int _money;
    public int money
    {
        get
        {
            return _money;
        }
    }

    private int _playerLevel;

    public int playerlevel
    {
        get
        {
            return _playerLevel;
        }
    }
    private int _priceLevel;

    public int pricelevel
    {
        get
        {
            return _priceLevel;
        }
    }

    public playerDataComponent(int _mon, int _player, int _price)
    {
        _money = _mon;
        _playerLevel = _player;
        _priceLevel = _price;
    }
}
