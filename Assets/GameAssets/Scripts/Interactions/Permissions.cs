using UnityEngine;
using UnityEngine.SceneManagement;
#if PLATFORM_ANDROID
using UnityEngine.Android;
#endif

// Request permission to use camera and external storage
// If user denies, reload scene and ask again
// If user denies and requests not to be asked again, user will need to set permission manually
// [TBD] Explain purpose of permission, allow user to deny at startup and ask again when the permission is actually needed
public class Permissions : MonoBehaviour
{
    void Awake()
    {
        #if PLATFORM_ANDROID
        if (!Permission.HasUserAuthorizedPermission(Permission.Camera))
        {
            Permission.RequestUserPermission(Permission.Camera);
            //Debug.Log("Camera permission granted: " + Permission.HasUserAuthorizedPermission(Permission.Camera) + ", reloading scene...");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        if (!Permission.HasUserAuthorizedPermission(Permission.ExternalStorageWrite))
        {
            Permission.RequestUserPermission(Permission.ExternalStorageWrite);
            //Debug.Log("Storage permission granted: " + Permission.HasUserAuthorizedPermission(Permission.ExternalStorageWrite) + ", reloading scene...");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        #endif        
    }
}