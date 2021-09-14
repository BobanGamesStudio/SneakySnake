using System;

namespace DiffLevel
{
    public class CampaignDataClass{

        public enum DifficultyLevel{
            Easy,
            Medium,
            Hard
        };

        // public int[] levelsUnlocked = new int[3];
        // public DifficultyLevel difficultyLevel;

        public DifficultyLevel GiveDiffLvlFromStr(string diffLvl){
            switch(diffLvl){
                case "Easy":
                    return DifficultyLevel.Easy;
                case "Medium":
                    return DifficultyLevel.Medium;
                case "Hard":
                    return DifficultyLevel.Hard;
                default:
                    return DifficultyLevel.Easy;
            }
        }
    }
    
}