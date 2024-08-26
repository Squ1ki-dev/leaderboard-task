using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using SimplePopupManager;

public class LoadPopup : MonoBehaviour
{
    [SerializeField] private string popupName = "";
    [SerializeField] private Button openBtn;

    private PopupService _popupService;

    [Inject]
    public void Construct(PopupService popupService)
    {
        _popupService = popupService;
    }

    private void Start() => openBtn.onClick.AddListener(Open);
    private void Open() => _popupService.OpenPopup(popupName, null);
}
