using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum GameModeOptions { SingleRoad=1, DoubleRoad, TripleRoad, QuadRoad }
public static class GameMode 
{
    private static GameModeOptions gameModeOption = GameModeOptions.SingleRoad;
    public static GameModeOptions GameModeOption { get { return gameModeOption; } set { gameModeOption = value; } } 
}
