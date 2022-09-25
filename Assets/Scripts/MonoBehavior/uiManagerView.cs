using System.Collections;
using System.Collections.Generic;
using Leopotam.Ecs;
using TMPro;
using UnityEngine;

public class uiManagerView : MonoBehaviour
{
    public TextMeshProUGUI moneyText;
    public GameObject playerButtonObj;
    public TextMeshProUGUI playerMoneyText;
    public GameObject priceButtonObj;
    public TextMeshProUGUI priceMoneyText;
    public EcsEntity dataEntity;
    public gameData myData;

    public void PressPlayerButton()
    {
        if (myData.mySaveData.money >= myData.gameplaySettings.playerProgression[myData.mySaveData.playerLevel+1].price)
        {
            ref increaseDataComponent dataComponent = ref dataEntity.Get<increaseDataComponent>();
            dataComponent.playerAmount = 1;
            dataComponent.moneyAmount = - myData.gameplaySettings.playerProgression[myData.mySaveData.playerLevel+1].price;
            dataEntity.Get<heroSpawnComponent>();
        }
    }

    public void PressPriceButton()
    {
        if (myData.mySaveData.money >= myData.gameplaySettings.priceProgression[myData.mySaveData.priceLevel+1].price)
        {
            ref increaseDataComponent dataComponent = ref dataEntity.Get<increaseDataComponent>();
            dataComponent.priceAmount = 1;
            dataComponent.moneyAmount = -myData.gameplaySettings.priceProgression[myData.mySaveData.priceLevel+1].price;
        }
    }
}
