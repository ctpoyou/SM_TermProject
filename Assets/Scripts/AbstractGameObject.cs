using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SoftwareModeling
{
    abstract public class AbstractGameObject : MonoBehaviour
    {
        abstract protected void Awake();
        abstract protected void Start();
        abstract protected void Update();
    }
}