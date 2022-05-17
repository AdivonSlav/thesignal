using System.Collections.Generic;
using TheSignal.Camera;
using TheSignal.Player.Input;
using UnityEngine;
using UnityEngine.UI;

namespace TheSignal.Player.Journal
{
    public static class AddedEntries
    {
        public static Dictionary<int, TextAsset> entries = new Dictionary<int, TextAsset>();
        
        public static bool IsPresent(TextAsset entry)
        {
            return entries.ContainsValue(entry);
        }
    }
    
    public class PlayerJournal : MonoBehaviour
    {
        [SerializeField] private GameObject journal;
        [SerializeField] private Text headerText;
        [SerializeField] private GameObject entryContainer;
        [SerializeField] private GameObject buttonContainer;
        [SerializeField] private GameObject buttonPrefab;

        [SerializeField] private List<TextAsset> textAssets;
        private InputManager inputManager;
        private CinemachineController cinemachineController;

        public bool journalOpened;

        private void Awake()
        {
            inputManager = GetComponent<InputManager>();
            cinemachineController = UnityEngine.Camera.main.GetComponent<CinemachineController>();
        }

        private void Start()
        {
            foreach (var entry in AddedEntries.entries)
            {
                AddEntry(entry.Value);
            }
        }

        private void Update()
        {
            if (inputManager.isOpeningJournal)
                ToggleJournal();
            
            switch (inputManager.isExiting)
            {
                case true when journal.activeInHierarchy && !entryContainer.activeInHierarchy:
                    ToggleJournal();
                    break;
                case true when journal.activeInHierarchy && entryContainer.activeInHierarchy:
                    CloseEntry();
                    break;
            }
        }

        private void ToggleJournal()
        {
            cinemachineController.TogglePause(inputManager.isAiming);
            journal.SetActive(!journal.activeInHierarchy);
            inputManager.isOpeningJournal = false;
            inputManager.isExiting = false;
            journalOpened = !journalOpened;
        }
        
        public void AddEntry(TextAsset entry)
        {
            if (!AddedEntries.IsPresent(entry))
                AddedEntries.entries.Add(textAssets.IndexOf(entry), entry);
            var button = Instantiate(buttonPrefab, buttonContainer.transform, true);
            button.GetComponent<Text>().text = entry.name;
            button.GetComponent<Button>().onClick.AddListener(delegate { OpenEntry(entry.name); });
        }

        private void OpenEntry(string entryName)
        {
            headerText.text = entryName;
            entryContainer.SetActive(true);
            buttonContainer.SetActive(false);
            entryContainer.GetComponentInChildren<Text>().text = textAssets.Find(entry => entry.name == entryName).text;
        }
        
        private void CloseEntry()
        {
            headerText.text = "Journal";
            entryContainer.SetActive(false);
            buttonContainer.SetActive(true);
            inputManager.isExiting = false;
        }

        public void LoadEntries(List<int> keys)
        {
            AddedEntries.entries = new Dictionary<int, TextAsset>();

            for (var i = 0; i < keys.Count; i++)
            {
                AddedEntries.entries.Add(keys[i], textAssets[keys[i]]);
            }
        }
    }
}
