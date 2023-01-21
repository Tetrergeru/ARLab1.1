using TMPro;
using System;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class FaceControls : MonoBehaviour
{
    public Victorine Victorine;

    private Direction _previousDirection = Direction.None;

    void Update()
    {
        var face = FindObjectOfType<ARFaceMeshVisualizer>();
        if (face == null)
        {
            return;
        }

        var dir = LookingTo(face.gameObject.transform.forward);

        if (dir != Direction.None && dir != _previousDirection)
        {
            switch ((dir, _previousDirection))
            {
                case ((Direction.Up, Direction.Down)):
                case ((Direction.Down, Direction.Up)):
                    Victorine.Answer(true);
                    break;
                case ((Direction.Left, Direction.Right)):
                case ((Direction.Right, Direction.Left)):
                    Victorine.Answer(false);
                    break;
            }
            _previousDirection = dir;
        }
    }

    Direction LookingTo(Vector3 dir)
    {
        var eps = 0.15;

        var x = dir.x;
        var y = dir.y;
        if (y < -eps)
        {
            return Direction.Up;
        }
        if (y > eps)
        {
            return Direction.Down;
        }
        if (x < -eps)
        {
            return Direction.Left;
        }
        if (x > eps)
        {
            return Direction.Right;
        }

        return Direction.None;
    }
}

enum Direction
{
    None,
    Up,
    Down,
    Right,
    Left,
}