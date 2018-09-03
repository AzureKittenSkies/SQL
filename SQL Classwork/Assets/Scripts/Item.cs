using UnityEngine;

public class Item // generic class for items
{
    // all items need an ID, a Name, an Icon and a MeshName
    public int id { get; set; }
    public string name { get; set; }
    public Texture2D iconName { get; set; }
    public GameObject meshName { get; set; }
}

public class Weapon : Item
{
    public int clipSize { get; set; }
    public int damage { get; set; }
    public float fireRate { get; set; }
    public float weaponRange { get; set; }
    public string ammoType { get; set; }

    public Weapon(int weaponId, string weaponName, int weaponClipSize, int weaponDamage, 
        float weaponFireRate, float weaponFireRange, string weaponAmmoType, string weaponIconName, 
        string weaponMeshName)
    {
        // item based set up
        name = weaponName;
        id = weaponId;
        iconName = Resources.Load("Icon/" + weaponIconName) as Texture2D; // pulling texture from folder
        meshName = Resources.Load("Prefabs/" + weaponMeshName) as GameObject; // pulling mesh from folder
        // weapon based set up
        clipSize = weaponClipSize;
        damage = weaponDamage;
        fireRate = weaponFireRate;
        weaponRange = weaponFireRange;
        ammoType = weaponAmmoType;
    }
}