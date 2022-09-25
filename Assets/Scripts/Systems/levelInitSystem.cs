using System.Collections;
using System.Collections.Generic;
using Leopotam.Ecs;
using UnityEngine;

public class levelInitSystem : IEcsInitSystem
{
    private EcsWorld myWorld;
    private gameData myData;
    private EcsEntity mineEntity;
    private EcsEntity storageEntity;
    private EcsEntity dataEntity;
    public void Init()
    {
        dataEntity = myWorld.NewEntity();
        ref playerDataComponent playerData = ref dataEntity.Get<playerDataComponent>();
        playerData = new playerDataComponent(myData.mySaveData.money, myData.mySaveData.playerLevel, myData.mySaveData.priceLevel);
        
        mineEntity = myWorld.NewEntity();
        ref mineComponent mine = ref mineEntity.Get<mineComponent>();
        mine.minePoint = myData.levelManager.myMine.minePoint;
        mine.itemPrefab = myData.levelManager.myMine.itemPrefab;
        MineInit(ref mine);
        
        storageEntity = myWorld.NewEntity();
        ref storageComponent storage = ref storageEntity.Get<storageComponent>();
        storage.playerPrefab = myData.levelManager.myStorage.playerPrefab;
        storage.storagePoint = myData.levelManager.myStorage.storagePoint;
        StorageInit(ref storage);
        
    }

    void MineInit(ref mineComponent _mine)
    {
        int poolLength = myData.gameplaySettings.playerProgression[myData.gameplaySettings.playerProgression.Length - 1]
            .value * 2;
        _mine.itemsPool = new GameObject[poolLength];
        for (int i = 0; i < poolLength; i++)
        {
            GameObject c = GameObject.Instantiate(_mine.itemPrefab);
            c.transform.SetParent(_mine.minePoint);
            
            c.SetActive(false);
            _mine.itemsPool[i] = c;
        }
    }

    void StorageInit(ref storageComponent _storage)
    {
        int lev = myData.mySaveData.playerLevel;
        if (lev >= myData.gameplaySettings.playerProgression.Length)
            lev = myData.gameplaySettings.playerProgression.Length - 1;
        int playersCount = myData.gameplaySettings.playerProgression[lev].value;
        _storage.myPlayers = new List<heroView>(playersCount);
        for (int i = 0; i < playersCount; i++)
        {
            GameObject c = GameObject.Instantiate(_storage.playerPrefab);
            Vector3 off = Random.insideUnitCircle;
            off.y = 0;
            c.transform.position = off;
            heroView view = c.GetComponent<heroView>();
            _storage.myPlayers.Add(view);
            EcsEntity player = myWorld.NewEntity();
            ref heroComponent hero = ref player.Get<heroComponent>();
            hero.itemPoint = view.itemPoint;
            hero.minePoint = myData.levelManager.myMine.minePoint;
            hero.storagePoint = myData.levelManager.myStorage.storagePoint;
            hero.myObj = c;
            hero.dataEntity = dataEntity;
            hero.mineEntity = mineEntity;
        }
    }
}
