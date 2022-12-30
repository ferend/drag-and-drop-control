using System;
using System.Collections;

using UnityEngine;

public enum GameState
{
    pause,
    gameplay,
    gameOver
}


public class GameFlowManager : Singleton<GameFlowManager>
{
    public bool canPlay;
    public GameState currentState = GameState.pause;


    [field: SerializeField] public Managers Managers { get; private set; }
    [field: SerializeField] public Controllers Controllers { get; private set; }
    [field: SerializeField] public Assets Assets { get; private set; }
    [field: SerializeField] public References References { get; private set; }
    

    
}
[Serializable]
public class Managers
{
    public static Managers Instance => GameFlowManager.Instance.Managers;
}
[Serializable]
public class Controllers
{
    // [field: SerializeField] public CameraController CameraController { get; private set; }
    // [field: SerializeField] public MechanicController MechanicController { get; private set; }
    // [field: SerializeField] public LevelController LevelController { get; private set; }
}
[Serializable]
public class Assets
{
    public static Assets Instance =>GameFlowManager.Instance.Assets;
    //Place your static-reachable assets here.
}
[Serializable]
public class References
{
    public static References Instance => GameFlowManager.Instance.References;
    //Place your static-reachable assets here.
}
