using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

public class SpritesLoader : MonoBehaviour 
{
    [SerializeField] private ImageToLoad[] _loadableImages;
    [SerializeField] private Button _loadSpritesButton;

    private void OnEnable()
    {
        _loadSpritesButton.onClick.AddListener(OnLoadSprites);
    }

    private void OnDisable()
    {
        _loadSpritesButton.onClick.RemoveListener(OnLoadSprites);
    }

    public void OnLoadSprites() 
    {
        foreach (var loadableImage in _loadableImages)
        {
            StartCoroutine(LoadSprite(loadableImage));
        }
    }

    IEnumerator LoadSprite(ImageToLoad loadableImage) 
    {
        var loadSpriteTask = Addressables.LoadAssetAsync<Sprite>(loadableImage.SpritePath);
        yield return loadSpriteTask;

        if (loadSpriteTask.Status == AsyncOperationStatus.Succeeded)
        {
            loadableImage.FillableImage.sprite = loadSpriteTask.Result;
        }
        else
        {
            Debug.LogError($"Failed to load sprite at path: {loadableImage.SpritePath}");
        }

    }

    private void OnDestroy()
    {
        foreach (var loadableImage in _loadableImages)
        {
            if (loadableImage.FillableImage.sprite != null)
            {
                Addressables.Release(loadableImage.FillableImage.sprite);
            }
        }
    }
}

[Serializable]
public struct ImageToLoad
{
    public Image FillableImage;
    public string SpritePath;
}
