using System.Collections;
using System.Collections.Generic;
using Leopotam.Ecs;
using UnityEngine;

public class dataControlSystem : IEcsRunSystem
{
    private EcsFilter<playerDataComponent, increaseDataComponent> levelDataIcrease;
    private EcsFilter<uiComponent> ui;
    private EcsFilter<playerDataComponent> data;
    private gameData myData;
    public void Run()
    {

        foreach (var l in levelDataIcrease)
        {
            ref var player = ref levelDataIcrease.Get1(l);
            ref var data = ref levelDataIcrease.Get2(l);
            saveData  save = new saveData(myData.mySaveData.money+data.moneyAmount, myData.mySaveData.playerLevel+data.playerAmount, myData.mySaveData.priceLevel+data.priceAmount);
            myData.myDataLoader.WriteToJson(save);
            myData.myDataLoader.ReadFromJson();
            myData.mySaveData = myData.myDataLoader.GetMyData();
            player = new playerDataComponent(myData.mySaveData.money, myData.mySaveData.playerLevel, myData.mySaveData.priceLevel);
            levelDataIcrease.GetEntity(l).Del<increaseDataComponent>();
            foreach (var j in ui)
            {
                ref var myUI = ref ui.Get1(j);
                ui.GetEntity(j).Get<uiChangeComponent>();
            }
        }
    }
}
