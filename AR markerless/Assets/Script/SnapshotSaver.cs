using UnityEngine;
using System.Collections;
using System.IO;

public class SnapshotSaver : MonoBehaviour
{
    public void CaptureSnapshot()
    {
        StartCoroutine(TakeScreenshotAndSaveToGallery());
    }

    IEnumerator TakeScreenshotAndSaveToGallery()
    {
        yield return new WaitForEndOfFrame();

        Texture2D screenshot = ScreenCapture.CaptureScreenshotAsTexture();
        byte[] pngData = screenshot.EncodeToPNG();

        string filename = "AR_Snapshot_" + System.DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".png";

#if UNITY_ANDROID && !UNITY_EDITOR
        string mimeType = "image/png";

        using (AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        using (AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity"))
        using (AndroidJavaObject contentResolver = currentActivity.Call<AndroidJavaObject>("getContentResolver"))
        using (AndroidJavaClass mediaStoreImagesMedia = new AndroidJavaClass("android.provider.MediaStore$Images$Media"))
        using (AndroidJavaObject contentValues = new AndroidJavaObject("android.content.ContentValues"))
        {
            contentValues.Call<AndroidJavaObject>("put", "_display_name", filename);
            contentValues.Call<AndroidJavaObject>("put", "mime_type", mimeType);
            contentValues.Call<AndroidJavaObject>("put", "relative_path", "Pictures/ARSnapshots");

            AndroidJavaObject imageUri = contentResolver.Call<AndroidJavaObject>(
                "insert",
                mediaStoreImagesMedia.GetStatic<AndroidJavaObject>("EXTERNAL_CONTENT_URI"),
                contentValues
            );

            using (AndroidJavaObject outputStream = contentResolver.Call<AndroidJavaObject>("openOutputStream", imageUri))
            {
                AndroidJavaClass bufferStreamClass = new AndroidJavaClass("java.io.BufferedOutputStream");
                using (AndroidJavaObject bufferedStream = bufferStreamClass.Call<AndroidJavaObject>("<init>", outputStream))
                {
                    bufferedStream.Call("write", pngData);
                    bufferedStream.Call("flush");
                    bufferedStream.Call("close");
                }
            }
        }
#endif

        Destroy(screenshot);
        Debug.Log("Screenshot saved to gallery (Scoped Storage method)");
    }
}
