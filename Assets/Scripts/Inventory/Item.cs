using UnityEngine;


[CreateAssetMenu]
public class Item : ScriptableObject
{

    // Item Information
    [Header("Item Information")]
    public string itemName;
    public string itemDescription;
    public int itemID;
    public Sprite sprite;
    public int itemPrice;

    // What the Item effects
    [Header("Item Stats")]
    public int stamina; 
    public int agility;
    public int defence;
    public int health;
    public int attack;

}
