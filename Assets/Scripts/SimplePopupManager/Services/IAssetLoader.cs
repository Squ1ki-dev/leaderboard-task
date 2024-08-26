using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace SimplePopupManager
{
    public interface IAssetLoader
    {
        Task<GameObject> LoadAssetAsync(string assetName);
        void ReleaseAsset(GameObject asset);
    }
}