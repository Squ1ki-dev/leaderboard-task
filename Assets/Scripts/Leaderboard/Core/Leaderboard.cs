using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using TMPro;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Zenject;

namespace Infrastructure.Service.Leaderboard
{
    using SimplePopupManager;

    public class Leaderboard : MonoBehaviour
    {
        [SerializeField] private GameObject leaderboardCellPrefab;
        [SerializeField] private Transform leaderboardContent;
        [SerializeField] private CellDesignSO cellDesign;

        [SerializeField] GridLayoutGroup gridLayoutGroup;
        private Dictionary<string, Sprite> avatarCache = new Dictionary<string, Sprite>();

        private ILeaderboardService leaderboardService;

        private async void Start()
        {
            gridLayoutGroup = leaderboardContent.GetComponent<GridLayoutGroup>();
            leaderboardService = new LocalLeaderboardService();

            await LoadAndDisplayLeaderboard();
        }

        private async Task LoadAndDisplayLeaderboard()
        {
            List<PlayerData> players = await leaderboardService.LoadLeaderboard();

            if (players != null && players.Count > 0)
                DisplayLeaderboard(players);
            else
                Debug.LogError("No leaderboard data found!");
        }

        private void DisplayLeaderboard(List<PlayerData> players)
        {
            var sortedLeaderboard = players.OrderByDescending(player => player.score).ToList();

            foreach (PlayerData player in sortedLeaderboard)
            {
                GameObject entry = Instantiate(leaderboardCellPrefab, leaderboardContent);
                LeaderboardCell leaderboardEntry = entry.GetComponent<LeaderboardCell>();
                RectTransform entryRectTransform = entry.GetComponent<RectTransform>();

                SetCellData(leaderboardEntry, player);
                AdjustCellAppearance(leaderboardEntry, entryRectTransform, player);
            }
        }

        private void SetCellData(LeaderboardCell cell, PlayerData player)
        {
            cell.nameText.text = player.name;
            cell.scoreText.text = player.score.ToString();

            if (avatarCache.TryGetValue(player.avatar, out Sprite cachedAvatar))
            {
                cell.avatarImage.sprite = cachedAvatar;
                cell.loadingText.gameObject.SetActive(false);
                cell.avatarImage.gameObject.SetActive(true);
            }
            else
                LoadAvatar(cell, player.avatar);
        }

        private void LoadAvatar(LeaderboardCell cell, string avatarUrl)
        {
            Davinci
                .get()
                .load(avatarUrl)
                .setCached(true)
                .into(cell.avatarImage)
                .withStartAction(() =>
                {
                    cell.loadingText.gameObject.SetActive(true);
                    cell.avatarImage.gameObject.SetActive(false);
                })
                .withLoadedAction(() =>
                {
                    cell.loadingText.gameObject.SetActive(false);
                    cell.avatarImage.gameObject.SetActive(true);
                })
                .start();
        }

        private void AdjustCellAppearance(LeaderboardCell cell, RectTransform rectTransform, PlayerData player)
        {
            Color playerColor = cellDesign.GetColorForType(player.type);
            cell.background.color = playerColor;

            float newHeight = cellDesign.GetHeightForType(player.type);
            rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, newHeight);
            gridLayoutGroup.cellSize = new Vector2(gridLayoutGroup.cellSize.x, newHeight);
        }
    }
}