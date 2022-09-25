using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Leopotam.Ecs;
using UnityEngine;

public class playersControlSystem : IEcsRunSystem
{
    private EcsFilter<heroComponent> players;
    
    
    private gameData myData;
    public void Run()
    {
        foreach (var i in players)
        {
            ref heroComponent hero = ref players.Get1(i);
            if (hero.myState == heroComponent.State.wait)
            {
                Vector3 dir = (hero.minePoint.position - hero.myObj.transform.position).normalized;
                dir.y = 0;
                hero.myObj.transform.forward = dir;
                hero.myObj.transform.DOMove(hero.minePoint.position, 3);
                hero.SetState(heroComponent.State.moveToMine);
            }
            else if (hero.myState == heroComponent.State.moveToMine)
            {
                float mag = (hero.myObj.transform.position - hero.minePoint.position).magnitude;
                if (mag < 0.5f)
                {
                    hero.SetState(heroComponent.State.getItem);
                }
            }
            else if (hero.myState == heroComponent.State.getItem)
            {
                HeroGetItem(ref hero);
                Vector3 dir = (hero.storagePoint.position - hero.myObj.transform.position).normalized;
                dir.y = 0;
                hero.myObj.transform.forward = dir;
                
                hero.myObj.transform.DOMove(hero.storagePoint.position, 3);
                hero.SetState(heroComponent.State.moveToStorage);
            }
            else if (hero.myState == heroComponent.State.moveToStorage)
            {
                float mag = (hero.myObj.transform.position - hero.storagePoint.position).magnitude;
                //Debug.Log("HERP MOVE TO STORAGE "+mag);
                if (mag < 0.5f)
                {
                    hero.SetState(heroComponent.State.giveItem);
                }
            }
            else if (hero.myState == heroComponent.State.giveItem)
            {
                //Debug.Log("HERP GIVE ");
                HeroGiveItem(ref hero);
                Vector3 dir = (hero.minePoint.position - hero.myObj.transform.position).normalized;
                dir.y = 0;
                hero.myObj.transform.forward = dir;
                hero.myObj.transform.DOMove(hero.minePoint.position, 3);
                hero.SetState(heroComponent.State.moveToMine);
            }
        }
    }

    void HeroGetItem(ref heroComponent _hero)
    {


        ref mineComponent myMine = ref _hero.mineEntity.Get<mineComponent>();
        for (int i = 0; i < myMine.itemsPool.Length; i++)
        {
            if (!myMine.itemsPool[i].activeSelf)
            {
                myMine.itemsPool[i].SetActive(true);
                myMine.itemsPool[i].transform.SetParent(_hero.itemPoint);
                myMine.itemsPool[i].transform.localPosition = Vector3.zero;
                _hero.haveItem = true;
                _hero.myItem = myMine.itemsPool[i];
                break;
            }
        }
    }

    void HeroGiveItem(ref heroComponent _hero)
    {
        ref mineComponent myMine = ref _hero.mineEntity.Get<mineComponent>();
        _hero.myItem.transform.SetParent(myMine.minePoint);
        _hero.myItem.SetActive(false);
        _hero.haveItem = false;
        _hero.myItem = null;
        int lev = myData.mySaveData.priceLevel;
        if (lev >= myData.gameplaySettings.priceProgression.Length)
            lev = myData.gameplaySettings.priceProgression.Length - 1;
        int award = myData.gameplaySettings.priceProgression[lev].value;
        ref increaseDataComponent dataComponent = ref _hero.dataEntity.Get<increaseDataComponent>();
        dataComponent.moneyAmount = award;
    }
}
