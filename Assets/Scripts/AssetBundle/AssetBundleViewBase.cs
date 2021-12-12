using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class AssetBundleViewBase : MonoBehaviour
{
   private const string UrlAssetBundleSpritesStandard = "https://drive.google.com/uc?export=download&id=1lE1EJk1OoTjjlcPUYwrZos4QuUtthJJU";
   private const string UrlAssetBundleSpritesLZ4 = "https://drive.google.com/uc?export=download&id=1cK2rvvcQHgIq0W-AUdIjQ8dLq2eUWojv";
   private const string UrlAssetBundleSpritesNoCompression = "https://drive.google.com/uc?export=download&id=1jnTAThKtyUokPiqg5FN7ZsLq9OiljWYq";

   private float _startLoadTime;

   [SerializeField]
   private DataSpriteBundle[] _dataSpriteBundles;

   private AssetBundle _spritesAssetBundle;

   protected IEnumerator DownloadAndSetAssetBundle()
   {
       yield return GetSpritesAssetBundle();

       if (_spritesAssetBundle == null)
       {
           Debug.LogError($"AssetBundle {_dataSpriteBundles} failed to load");
           yield break;
       }

       SetDownloadAssets();
       yield return null;
   }
  
   private IEnumerator GetSpritesAssetBundle()
   {
       var request = UnityWebRequestAssetBundle.GetAssetBundle(UrlAssetBundleSpritesStandard);

       _startLoadTime = Time.time;
      
       yield return request.SendWebRequest();
      
       while (!request.isDone)
           yield return null;

       StateRequest(request, ref _spritesAssetBundle);
   }

   private void StateRequest(UnityWebRequest request, ref AssetBundle assetBundle)
   {
       if (request.error == null)
       {
           assetBundle = DownloadHandlerAssetBundle.GetContent(request);
           Debug.Log($"TimeLoad {Time.time - _startLoadTime}");
       }
       else
       {
           Debug.Log(request.error);
       }
   }
  
   private void SetDownloadAssets()
   {
       foreach (var data in _dataSpriteBundles)
           data.Image.sprite = _spritesAssetBundle.LoadAsset<Sprite>(data.NameAssetBundle);
   }
}

