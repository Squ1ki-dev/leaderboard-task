using System.Collections;
using System.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;

namespace Infrastructure.Service.Leaderboard
{
    public interface ILeaderboardService
    {
        Task<List<PlayerData>> LoadLeaderboard();
    }
}