
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.InteropServices;


// Codigo de redirecionamento Hyperlink.
public class LinkButton : MonoBehaviour
{
    // Start is called before the first frame update
    public void OpenLik(string urlBtn)
    {
                #if !UNITY_EDITOR
                openWindow(urlBtn);
                #endif
    }

    [DllImport("__Internal")]
    private static extern void openWindow(string url);
}
