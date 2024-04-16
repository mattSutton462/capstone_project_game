using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIData : MonoBehaviour
{
    //public List<Transform> targets = new List<Transform>();
    //public Collider2D[] obstacles = new Collider2D[0];

    //public Transform currentTarget;

    //public int GetTargetsCount() => targets.Count;
    public List<Transform> targets = null;
    public Collider2D[] obstacles = null;

    public Transform currentTarget;

    public int GetTargetsCount() => targets == null ? 0 : targets.Count;
}
