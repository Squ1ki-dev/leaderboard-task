using System.Collections;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;

namespace Infrastructure.Service.Leaderboard
{
    public class LocalLeaderboardService : ILeaderboardService
    {
        public async Task<List<PlayerData>> LoadLeaderboard()
        {
            TextAsset jsonFile = Resources.Load<TextAsset>("Leaderboard");

            if (jsonFile != null)
            {
                PlayerDataList playerDataList = JsonUtility.FromJson<PlayerDataList>(jsonFile.text);
                return playerDataList.leaderboard;
            }
            else
            {
                Debug.LogError("Leaderboard data not found!");
                return null;
            }
        }
    }
}