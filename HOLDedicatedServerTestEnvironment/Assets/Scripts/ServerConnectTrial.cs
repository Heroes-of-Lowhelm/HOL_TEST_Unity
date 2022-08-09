using UnityEngine;
using System.Collections;
using static UnityEngine.Networking.UnityWebRequest;
using TMPro;

/*public class ServerConnectTrial : MonoBehaviour
{
    public TMP_InputField information;
    public string url;

    const string t_loading = "Loading...";

    public void OnGetDataClick()
    {
        StartCoroutine(GetData_c());
    }
    IEnumerator GetData_c()
    {
        information.text = t_loading;
        using (var request = Get(url))
        {
            yield return request.SendWebRequest();
            if (request.result == Result.ConnectionError || request.result == Result.ProtocolError)
            {
                information.text = request.error;
            }
            else
            {
                information.text = request.downloadHandler.text;
            }
        }
    }

}*/
