using UnityEngine;

[CreateAssetMenu(menuName ="YAVD/Player Stats")]
public class SPlayerStats : ScriptableObject {

    public float hp;
    public int gold;
    public int roomNumber;
    public Vector3 position;
    public Vector3 facingDirection;
    public GameObject mesh;
    public GameObject sword;
    public string description;
    public void CopyTo(SPlayerStats destination)
    {
        destination.hp = hp;
        destination.gold = gold;
        destination.roomNumber = roomNumber;
        destination.position = position;
        destination.facingDirection = facingDirection;
        destination.mesh = mesh;
        destination.sword = sword;        
    }
}
