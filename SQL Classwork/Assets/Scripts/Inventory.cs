using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public string[] itemList; // string list of all out items from the database
    public bool loaded;
    public Vector2 scr;
    public Dictionary<int, Weapon> weapons = new Dictionary<int, Weapon>();

    // Use this for initialization
    void Start()
    {
        StartCoroutine(LoadItemData());
    }

    private void OnGUI()
    {
        scr.x = Screen.width / 16;
        scr.y = Screen.height / 9;

        if (!loaded)
        {
            GUI.Box(new Rect(scr.x * 0, scr.y * 0, scr.x * 16, scr.y), "Loading...");
        }
    }

    IEnumerator LoadItemData()
    {
        WWW itemDataURL = new WWW("http://localhost/sqlsystem/itemdata.php"); // run out itemdata php script
        yield return itemDataURL;
        string textDataString = itemDataURL.text; // pull all items through 1 string
        string[] items = textDataString.Split('#'); // split the one long string into strings for each item
        itemList = new string[items.Length - 1]; // set our itemlist to one less the length of items
        for (int i = 0; i < itemList.Length; i++)
        {
            string[] itemDataSplit = itemList[i].Split('|');
            // splitting string into parts, then creating a new weapon with the data
            Weapon weapon = new Weapon(int.Parse(itemDataSplit[0]), itemDataSplit[1], int.Parse(itemDataSplit[2]),
                int.Parse(itemDataSplit[3]), float.Parse(itemDataSplit[4]), float.Parse(itemDataSplit[5]),
                itemDataSplit[6], itemDataSplit[7], itemDataSplit[8]);
            // add the weapon to the dictionary using the id number
            weapons.Add(weapon.id, weapon);
            Debug.Log(weapons[i].name);
        }
        loaded = true;
    }
}
