using UnityEngine;

[CreateAssetMenu(fileName = "New Player Model", menuName = "ScriptableObjects/Player Model", order = 1)]
public class PlayerModel : ScriptableObject
{
    //Health of the player
    [SerializeField]
    private int health = 3;

    //Speed of the player movement
    [SerializeField]
    private float speed = 5f;

    //Getters and setters
    public int Health { get { return health; } set { health = value; } }

    public float Speed { get { return speed; } }

    //Shot's pivot location
    public Vector3 PivotPosition;

    public bool Hittable = true;
}
