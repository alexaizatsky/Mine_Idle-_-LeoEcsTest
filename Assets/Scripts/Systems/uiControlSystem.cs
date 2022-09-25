using System.Collections;
using System.Collections.Generic;
using Leopotam.Ecs;
using UnityEngine;

public class uiControlSystem : IEcsRunSystem
{
    private EcsFilter<uiComponent, uiChangeComponent> uiChange;
    private gameData myData;
    public void Run()
    {
        foreach (var i in uiChange)
        {
            ref var uiComp = ref uiChange.Get1(i);
            ref var change = ref uiChange.Get2(i);
            uiComp.moneyText = myData.uiManager.moneyText;
            uiComp.playerButtonObj = myData.uiManager.playerButtonObj;
            uiComp.playerMoneyText = myData.uiManager.playerMoneyText;
            uiComp.priceButtonObj = myData.uiManager.priceButtonObj;
            uiComp.priceMoneyText = myData.uiManager.priceMoneyText;
            uiComp.moneyText.text = myData.mySaveData.money.ToString();
            if (myData.mySaveData.playerLevel<myData.gameplaySettings.playerProgression.Length-1)
            {
                uiComp.playerButtonObj.SetActive(true);
                uiComp.playerMoneyText.text =
                    myData.gameplaySettings.playerProgression[myData.mySaveData.playerLevel+1].price.ToString();
                if(myData.mySaveData.money>=myData.gameplaySettings.playerProgression[myData.mySaveData.playerLevel+1].price)
                    uiComp.playerMoneyText.color = Color.green;
                else
                    uiComp.playerMoneyText.color = Color.red;
            }
            else
            {
                uiComp.playerButtonObj.SetActive(false);
            }
            if (myData.mySaveData.priceLevel<myData.gameplaySettings.priceProgression.Length-1)
            {
                uiComp.priceButtonObj.SetActive(true);
                uiComp.priceMoneyText.text =
                    myData.gameplaySettings.priceProgression[myData.mySaveData.priceLevel+1].price.ToString();
                if(myData.mySaveData.money>=myData.gameplaySettings.priceProgression[myData.mySaveData.priceLevel+1].price)
                    uiComp.priceMoneyText.color = Color.green;
                else
                    uiComp.priceMoneyText.color = Color.red;
            }
            else
            {
                uiComp.priceButtonObj.SetActive(false);
            }
            uiChange.GetEntity(i).Del<uiChangeComponent>();
        }
    }
}
