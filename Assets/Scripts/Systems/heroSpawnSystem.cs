using System.Collections;
using System.Collections.Generic;
using Leopotam.Ecs;
using UnityEngine;

public class heroSpawnSystem : IEcsRunSystem
{
    private EcsFilter<playerDataComponent, heroSpawnComponent> spawners;
    private EcsFilter<storageComponent> storage;
    private EcsFilter<mineComponent> mines;
    private EcsWorld world;
    private gameData myData;
    public void Run()
    {
        foreach (var i in spawners)
        {
            ref var playerD = ref spawners.Get1(i);
            ref var spawn = ref spawners.Get2(i);
            foreach (var j in storage)
            {
                ref var storage = ref this.storage.Get1(j);
                foreach (var k in mines)
                {
                    ref var mine = ref mines.Get1(k);
                    
                    GameObject c = GameObject.Instantiate(storage.playerPrefab);
                    Vector3 off = Random.insideUnitCircle;
                    off.y = 0;
                    c.transform.position = off;
                    heroView view = c.GetComponent<heroView>();
                    storage.myPlayers.Add(view);
                    EcsEntity player = world.NewEntity();
                    ref heroComponent hero = ref player.Get<heroComponent>();
                    hero.itemPoint = view.itemPoint;
                    hero.minePoint = myData.levelManager.myMine.minePoint;
                    hero.storagePoint = myData.levelManager.myStorage.storagePoint;
                    hero.myObj = c;
                    hero.dataEntity = spawners.GetEntity(i);
                    hero.mineEntity = mines.GetEntity(k);
                    spawners.GetEntity(i).Del<heroSpawnComponent>();
                }
            }
        }
    }
}
