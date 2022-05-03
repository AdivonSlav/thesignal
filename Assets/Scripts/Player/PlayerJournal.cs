using System.Collections.Generic;
using UnityEngine;

namespace TheSignal
{
    public class PlayerJournal : MonoBehaviour
    {
        private List<TextAsset> addedEntries;
        
        private void Awake()
        {
            addedEntries = new List<TextAsset>();
        }

        public void AddEntry(ref TextAsset entry)
        {
            addedEntries.Add(entry);
        }
    }
}
