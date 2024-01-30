using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;

public class SceneManager : MonoBehaviour
{
    public TextMeshProUGUI message;
    List<string> messages = new List<String>();
    int current_message = 0;
    float display_duration = 3.141592f;
    float typing_frequency = 0.05f; 
    float fade_value = 0.1f;
    Color default_color;
    int refresh_frequency = 3; //seconds


    string FIRESTORE_URL = "https://firestore.googleapis.com/v1beta1/projects/nostagain/databases/(default)/documents/timeinabottle";

    // Start is called before the first frame update
    void Start()
    {
        default_color = new Color(message.color.r, message.color.g, message.color.b, 1.0f);
        messages.Add("I like Peanut butter balls");
        messages.Add("Foxbearneatxomputersisterbrothermother fish");
        messages.Add("The elephant was too heavy on my chest");
        messages.Add("Its not the past that was, but the past that could have been.");
        messages.Add("An exchange of nostalgia. Absorbing. Changing. \nRefracting.");
        StartCoroutine(FetchFirestore());
        StartCoroutine(ChangeMessage());


    }

    IEnumerator ChangeMessage() 
    {
        while (true)
        {
            // Fade previous one 

            // Type in new one 
            current_message = (current_message + 1) % messages.Count;
            message.text = "";
            message.color = default_color; 
            yield return TypeIn();
            yield return new WaitForSeconds(display_duration);
            yield return FadeOut();
        }
    }

    IEnumerator FadeOut()
    {
        while (message.color.a > 0)
        {
            message.color = new Color(message.color.r, message.color.g, message.color.b, message.color.a - fade_value);
            yield return new WaitForSeconds(0.1f);
        }

    }

    IEnumerator TypeIn()
    {

        while (message.text.Length < messages[current_message].Length)
        {
            message.text = messages[current_message].Substring(0, message.text.Length + 1);
            yield return new WaitForSeconds(typing_frequency);

        }
    }

    IEnumerator FetchFirestore()
    {
        while (true)
        {

            UnityWebRequest storage = UnityWebRequest.Get(FIRESTORE_URL);
            yield return storage.SendWebRequest();
            if (storage.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(storage.error);
            }

            JSONResponse response = JsonUtility.FromJson<JSONResponse>(storage.downloadHandler.text);
            if (response.documents.Count != messages.Count)
            {
                messages = ParseMessages(response.documents);
            }
            yield return new WaitForSeconds(refresh_frequency);
        }

    }

    List<string> ParseMessages(List<Entries> entries)
    {
        List<string> new_messages_array = new List<string>();

        foreach(Entries entry in entries)
        {
            new_messages_array.Add(entry.fields.message.stringValue);
        }

        return new_messages_array;
    }

}

[System.Serializable]
public class Message
{
    public string stringValue;
}

[System.Serializable]
public class Fields
{
    public Message message;
}

[System.Serializable]
public class Entries
{
    public string name;
    public Fields fields;
    public string createTime;
    public string updateTime;
}

[System.Serializable]
public class JSONResponse
{
    public List<Entries> documents;
}





