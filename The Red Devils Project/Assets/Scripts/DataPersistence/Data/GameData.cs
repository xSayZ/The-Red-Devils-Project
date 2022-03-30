// ########################################################
// #
// #
// #            Script written by Felix
// #
// #            Referenced and sourced:
// #            Trever Mock - Youtube
// #            
// #
// #
// #
// #
// ########################################################

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public string globalVariablesStateJson;

    public Vector3 playerPosition;

    // The values defined in this constructor will be the default values
    // the game starts with when there's no data to Load
    public GameData()
    {
        playerPosition = Vector3.zero;
        this.globalVariablesStateJson = "";
    }
}
