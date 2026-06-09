
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class AddressableVideoLoader : MonoBehaviour
{
    [SerializeField] private VideoPlayer videoPlayer;
    [SerializeField] private string videoAddress = "earthquake_video";

    public static AddressableVideoLoader Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        LoadVideo(videoAddress);
    }

    public void LoadVideo(string address)
    {
        videoPlayer.Stop();
        Addressables.LoadAssetAsync<VideoClip>(address).Completed += OnVideoLoaded;
    }

    private void OnVideoLoaded(AsyncOperationHandle<VideoClip> handle)
    {
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            Debug.Log("Video loaded: " + handle.Result.name);
            videoPlayer.clip = handle.Result;
            videoPlayer.Play();
        }
        else
        {
            Debug.LogError("Failed to load video: " + handle.OperationException);
        }
    }
}