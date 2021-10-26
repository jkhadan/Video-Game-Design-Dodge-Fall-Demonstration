using System;
using UnityEngine;

/// <summary>
/// Used to control the behavior of the asteroids
/// </summary>
public class AsteroidControler : MonoBehaviour
{
    /// <summary>
    /// Called when the asteroid collides with something
    /// </summary>
    /// <param name="other">
    /// Object the asteroid is colliding with
    /// </param>
    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.collider.CompareTag("Floor")) // Check if the asteroid hits the floor/ground
            Destroy(gameObject); // destroy the asteroid
    }
}
