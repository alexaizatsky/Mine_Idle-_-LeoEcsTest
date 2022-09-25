using System.Collections;
using System.Collections.Generic;
using Leopotam.Ecs;
using UnityEngine;

public class uiInitSystem : IEcsInitSystem
{
    private EcsWorld myWorld;
    private gameData myData;
    private EcsEntity uiEntity;
    private EcsFilter<playerDataComponent> playerData;
    public void Init()
    {
        foreach (var i in playerData)
        {


            uiEntity = myWorld.NewEntity();
            ref uiComponent ui = ref uiEntity.Get<uiComponent>();
            myData.uiManager.dataEntity = playerData.GetEntity(i);
            myData.uiManager.myData = myData;
            ui.moneyText = myData.uiManager.moneyText;
            ui.playerButtonObj = myData.uiManager.playerButtonObj;
            ui.playerMoneyText = myData.uiManager.playerMoneyText;
            ui.priceButtonObj = myData.uiManager.priceButtonObj;
            ui.priceMoneyText = myData.uiManager.priceMoneyText;
            ui.moneyText.text = myData.mySaveData.money.ToString();
            if (myData.mySaveData.playerLevel < myData.gameplaySettings.playerProgression.Length - 1)
            {
                ui.playerButtonObj.SetActive(true);
                ui.playerMoneyText.text =
                    myData.gameplaySettings.playerProgression[myData.mySaveData.playerLevel + 1].price.ToString();
                if (myData.mySaveData.money >=
                    myData.gameplaySettings.playerProgression[myData.mySaveData.playerLevel + 1].price)
                    ui.playerMoneyText.color = Color.green;
                else
                    ui.playerMoneyText.color = Color.red;

            }
            else
            {
                ui.playerButtonObj.SetActive(false);
            }

            if (myData.mySaveData.priceLevel < myData.gameplaySettings.priceProgression.Length - 1)
            {
                ui.priceButtonObj.SetActive(true);
                ui.priceMoneyText.text =
                    myData.gameplaySettings.priceProgression[myData.mySaveData.priceLevel + 1].price.ToString();
                if (myData.mySaveData.money >=
                    myData.gameplaySettings.priceProgression[myData.mySaveData.priceLevel + 1].price)
                    ui.priceMoneyText.color = Color.green;
                else
                    ui.priceMoneyText.color = Color.red;
            }
            else
            {
                ui.priceButtonObj.SetActive(false);
            }
        }
    }
}
