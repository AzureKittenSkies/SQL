using UnityEngine;

public class PlayerCustomisation
{
    public int id { get; set; }
    public string name { get; set; }
    public Texture2D iconName { get; set; }
}

public class PlayerCustomisationLoadout : PlayerCustomisation
{
    public int hatID { get; set; }
    public int faceID { get; set; }
    public int bodyID { get; set; }
    public int shirtID { get; set; }
    public int pantsID { get; set; }




    public PlayerCustomisationLoadout(int playerID, string playerName, int playerHatID, int playerFaceID, int playerBodyID, 
        int playerShirtID, int playerPantsID, string playerIconName)
    {
        name = playerName;
        id = playerID;
        iconName = Resources.Load("Prefabs/" + playerIconName) as Texture2D;

        hatID = playerHatID;
        faceID = playerFaceID;
        bodyID = playerBodyID;
        shirtID = playerShirtID;
        pantsID = playerPantsID;

    }



}