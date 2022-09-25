using System.Collections;
using System.Collections.Generic;
using Leopotam.Ecs;
using UnityEngine;

public struct heroComponent
{
    public GameObject myObj;
    public Transform itemPoint;
    public GameObject myItem;
    public bool haveItem;
    public Transform minePoint;
    public Transform storagePoint;
    public enum State
    {
        wait,
        moveToMine,
        getItem,
        moveToStorage,
        giveItem,
    }

    public State myState;

    public void SetState(State s)
    {
        myState = s;
    }

    public EcsEntity mineEntity;
    public EcsEntity dataEntity;
}
