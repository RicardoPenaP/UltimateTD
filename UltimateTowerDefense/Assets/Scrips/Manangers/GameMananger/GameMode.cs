using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum GameModeOptions { SingleRoad=1, DoubleRoad, TripleRoad, QuadRoad }
public static class GameMode 
{
    public static GameModeOptions GameModeOption { get; set; } 
}
