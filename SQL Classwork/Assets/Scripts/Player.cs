using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public string[] playerCustomList;
    public bool loaded;
    public Vector2 scr;
    public Dictionary<int, PlayerCustomisationLoadout> playerCustom = new Dictionary<int, PlayerCustomisationLoadout>();

    // Use this for initialization
    void Start()
    {
        StartCoroutine(LoadPlayerData());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator LoadPlayerData()
    {
        WWW playerDataURL = new WWW("http://localhost/sqlsystem/itemdata.php");
        yield return playerDataURL;
        string textDataString = playerDataURL.text;
        string[] appearance = textDataString.Split('#');
        playerCustomList = new string[appearance.Length - 1];
        for (int i = 0; i < playerCustomList.Length; i++)
        {
            string[] playerCustomSplit = playerCustomList[i].Split('|');

            PlayerCustomisationLoadout playerAppearance = new PlayerCustomisationLoadout(int.Parse(playerCustomSplit[0]), playerCustomSplit[1],
               int.Parse(playerCustomSplit[2]), int.Parse(playerCustomSplit[3]), int.Parse(playerCustomSplit[4]),
               int.Parse(playerCustomSplit[5]), int.Parse(playerCustomSplit[6]), playerCustomSplit[7]);

            playerCustom.Add(playerAppearance.id, playerAppearance);
            Debug.Log(playerAppearance.name);
        }

        loaded = true;

    }

}

