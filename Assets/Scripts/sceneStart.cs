using System;
using System.Collections;
using System.Collections.Generic;
using Leopotam.Ecs;
using UnityEngine;

public class sceneStart : MonoBehaviour
{
    [SerializeField] private uiManagerView uiManager;
    [SerializeField] private levelManagerView levelManager;
    [SerializeField] private gameplaySettingsSO gameplaySettings;
    private EcsWorld world;
    private EcsSystems initSystems;
    private EcsSystems updateSystems;
    private dataLoader myLoader;
    private gameData myData;
    void Start()
    {
        world = new EcsWorld();
        myLoader = new dataLoader();
        myData = new gameData(myLoader.GetMyData(), uiManager, levelManager, gameplaySettings, myLoader);

        initSystems = new EcsSystems(world)
            .Add(new levelInitSystem())
            .Add(new uiInitSystem())
            .Inject(myData);
        initSystems.ProcessInjects();
        initSystems.Init();

        updateSystems = new EcsSystems(world)
            .Add(new playersControlSystem())
            .Add(new dataControlSystem())
            .Add(new uiControlSystem())
            .Add(new heroSpawnSystem())
            .Inject(myData);
        updateSystems.ProcessInjects();
        updateSystems.Init();

    }

    // Update is called once per frame
    void Update()
    {
        updateSystems.Run();
    }

    private void OnDestroy()
    {
        initSystems.Destroy();
        updateSystems.Destroy();
        world.Destroy();
    }
}
