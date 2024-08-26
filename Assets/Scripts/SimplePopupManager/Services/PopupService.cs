//© 2023 Sophun Games LTD. All rights reserved.
//This code and associated documentation are proprietary to Sophun Games LTD.
//Any use, reproduction, distribution, or release of this code or documentation without the express permission
//of Sophun Games LTD is strictly prohibited and could be subject to legal action.

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace SimplePopupManager
{
    public class PopupService : IPopupManagerService
    {
        private readonly Dictionary<string, GameObject> m_Popups = new();
        private readonly IAssetLoader assetLoader;
        private readonly PopupFactory popupFactory;

        public PopupService(IAssetLoader assetLoader, PopupFactory popupFactory)
        {
            this.assetLoader = assetLoader;
            this.popupFactory = popupFactory;
        }

        public async void OpenPopup(string name, object param)
        {
            if (m_Popups.ContainsKey(name))
            {
                Debug.LogError($"Popup with name {name} is already shown");
                return;
            }

            GameObject popupObject = await popupFactory.CreatePopup(name, param);

            if (popupObject != null)
                m_Popups.Add(name, popupObject);
        }

        public void ClosePopup(string name)
        {
            if (m_Popups.TryGetValue(name, out GameObject popupObject))
            {
                m_Popups.Remove(name);
                assetLoader.ReleaseAsset(popupObject);
            }
            else
            {
                Debug.LogError($"No popup found with name {name}");
            }
        }
    }
}