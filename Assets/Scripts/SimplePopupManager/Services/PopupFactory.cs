using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace SimplePopupManager
{
    public class PopupFactory
    {
        private readonly Canvas canvas;
        private readonly IAssetLoader assetLoader;

        public PopupFactory(Canvas canvas, IAssetLoader assetLoader)
        {
            this.canvas = canvas;
            this.assetLoader = assetLoader;
        }

        public async Task<GameObject> CreatePopup(string name, object param)
        {
            GameObject popupObject = await assetLoader.LoadAssetAsync(name);

            if (popupObject != null)
            {
                popupObject.transform.SetParent(canvas.transform, false);

                popupObject.SetActive(false);
                IPopupInitialization[] popupInitComponents = popupObject.GetComponents<IPopupInitialization>();

                foreach (IPopupInitialization component in popupInitComponents)
                {
                    await component.Init(param);
                }

                popupObject.SetActive(true);
            }

            return popupObject;
        }
    }
}