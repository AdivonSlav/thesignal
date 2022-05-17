
using System.Collections.Generic;
using UnityEngine;

namespace TheSignal.SaveLoad
{
    //makes the class able to be saved in a file
    [System.Serializable]
    public class PlayerData 
    {
        public int level;
        public List<int> addedEntries;

        public PlayerData(int CurrentLevel, List<int> addedEntries)
        {
            level = CurrentLevel;
            this.addedEntries = addedEntries;
        }
    }
}
