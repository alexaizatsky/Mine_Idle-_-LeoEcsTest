using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class saveData 
{
    public int money;
    public int playerLevel;
    public int priceLevel;

    public saveData(int _money, int _player, int _price)
    {
        money = _money;
        playerLevel = _player;
        priceLevel = _price;
    }
}
