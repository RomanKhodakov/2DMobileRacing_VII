using UnityEngine;
using UnityEngine.UI;

public class LoadWindowView : AssetBundleViewBase
{
    [SerializeField]
    private Button _loadAsssetsButton;

    private void Start()
    {
        _loadAsssetsButton.onClick.AddListener(LoadAsset);
    }

    private void OnDestroy()
    {
        _loadAsssetsButton.onClick.RemoveAllListeners();
    }

    private void LoadAsset()
    {
        _loadAsssetsButton.interactable = false;
        StartCoroutine(DownloadAndSetAssetBundle());
    }
}