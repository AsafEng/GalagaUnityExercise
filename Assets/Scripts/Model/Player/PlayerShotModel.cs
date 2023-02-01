using System.Collections.Generic;
using UnityEngine;

//Possible player's shot types
public enum PlayerShotType { Normal, Split };

[CreateAssetMenu(fileName = "New Player Shot Model", menuName = "ScriptableObjects/Player Shot Model", order = 3)]
public class PlayerShotModel : ScriptableObject
{
    //Speed of the projectile movement
    [SerializeField]
    private float speed = 5f;

    public float Speed { get { return speed; } }

    public List<IPlayerShot> Views = new List<IPlayerShot>();

    [SerializeField]
    public Vector2 StartForce;

    //The player's shot projectile type
    public PlayerShotType ShotType = PlayerShotType.Normal;
}
