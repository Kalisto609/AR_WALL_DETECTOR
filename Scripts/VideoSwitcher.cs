using UnityEngine;

public class VideoSwitcher : MonoBehaviour
{
    public void PlayVideo1()
    {
        if (AddressableVideoLoader.Instance != null)
            AddressableVideoLoader.Instance.LoadVideo("earthquake_video");
        else
            Debug.LogWarning("Place content on wall first.");
    }

    public void PlayVideo2()
    {
        if (AddressableVideoLoader.Instance != null)
            AddressableVideoLoader.Instance.LoadVideo("video_2");
        else
            Debug.LogWarning("Place content on wall first.");
    }
}