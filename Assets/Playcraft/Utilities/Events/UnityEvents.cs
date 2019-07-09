using System;
using UnityEngine;
using UnityEngine.Events;

// Primitives
[Serializable] public class BoolEvent: UnityEvent<bool> { }
[Serializable] public class IntEvent : UnityEvent<int> { }
[Serializable] public class FloatEvent : UnityEvent<float> { }
[Serializable] public class Vector2Event : UnityEvent<Vector2> { }
[Serializable] public class Vector3Event : UnityEvent<Vector3> { }

// Multi-arguments
[Serializable] public class Vector3x2Event : UnityEvent<Vector3, Vector3> { }
[Serializable] public class Vector3IntEvent : UnityEvent<Vector3, int> { }
[Serializable] public class GameObjectVector3Event : UnityEvent<GameObject, Vector3> { }

// ADDED
[Serializable] public class SpriteEvent : UnityEvent<Sprite> { }

