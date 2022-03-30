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

public interface IDataPersistence {
    void LoadData(GameData data);
    void SaveData(GameData data);
}
