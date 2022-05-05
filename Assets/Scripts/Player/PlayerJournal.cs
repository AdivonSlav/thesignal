using System.Collections.Generic;
using TheSignal.Player.Input;
using TheSignal.Scenes.Behaviours;
using UnityEngine;
using UnityEngine.UI;

namespace TheSignal.Player
{
    public class PlayerJournal : MonoBehaviour
    {
        [SerializeField] private GameObject journal;
        [SerializeField] private Text headerText;
        [SerializeField] private GameObject entryContainer;
        [SerializeField] private GameObject buttonContainer;
        [SerializeField] private GameObject buttonPrefab;

        private List<TextAsset> addedEntries;
        private InputManager inputManager;
        private CinemachineController cinemachineController;

        public bool journalOpened;
        
        private void Awake()
        {
            addedEntries = new List<TextAsset>();
            inputManager = GetComponent<InputManager>();
            cinemachineController = Camera.main.GetComponent<CinemachineController>();
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
            addedEntries.Add(entry);

            var button = Instantiate(buttonPrefab, buttonContainer.transform, true);
            button.GetComponent<Text>().text = entry.name;
            button.GetComponent<Button>().onClick.AddListener(delegate { OpenEntry(entry.name); });
        }

        private void OpenEntry(string entryName)
        {
            headerText.text = entryName;
            entryContainer.SetActive(true);
            buttonContainer.SetActive(false);
            entryContainer.GetComponentInChildren<Text>().text = addedEntries.Find(entry => entry.name == entryName).text;
        }
        
        private void CloseEntry()
        {
            headerText.text = "Journal";
            entryContainer.SetActive(false);
            buttonContainer.SetActive(true);
            inputManager.isExiting = false;
        }
    }
}
