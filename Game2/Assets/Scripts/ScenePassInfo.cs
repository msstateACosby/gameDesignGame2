using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ScenePassInfo
{
    public static int charSelected = -1;

    //if won is -1, no game has been played.
    //if won is 0, then you lost.
    //if won is 1, then you one.
    public static int won = -1;
}
