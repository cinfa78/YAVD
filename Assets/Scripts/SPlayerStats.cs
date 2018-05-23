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
}
