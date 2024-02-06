using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalProvider : MonoBehaviour
{
    [SerializeField] private bool _canProvideNormal;
    public bool CanProvideNormal { get { return _canProvideNormal; } }
}
