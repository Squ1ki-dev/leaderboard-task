using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using SimplePopupManager;

public class PopupButton : MonoBehaviour
{
    [SerializeField] private Button closeBtn;
    [SerializeField] private string popupName = "";

    private PopupService _popupService;

    [Inject]
    public void Construct(PopupService popupService)
    {
        _popupService = popupService;
    }

    private void Start() => closeBtn.onClick.AddListener(Close);

    private void Close() => _popupService.ClosePopup(popupName);
}
