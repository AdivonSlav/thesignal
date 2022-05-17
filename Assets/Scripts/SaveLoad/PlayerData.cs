
namespace TheSignal.SaveLoad
{
    //makes the class able to be saved in a file
    [System.Serializable]
    public class PlayerData 
    {
        public int level;

        public PlayerData(int CurrentLevel)
        {
            level = CurrentLevel;
        }
    }
}
