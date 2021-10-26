using UnityEngine;
using Random = System.Random;

namespace Asteroids {
    /// <summary>
    /// Responsible for controlling the spawning of asteroids in the game
    /// </summary>
    public class AsteroidSpawner : MonoBehaviour
    {
        public float minX; // The minimum x-bound of the asteroid spawn area
        public float maxX; // The maximum x-bound of the asteroid spawn area
        public float minY; // The minimum y-bound of the asteroid spawn area
        public float maxY; // The maximum x-bound of the asteroid spawn area
        public float minScale; // The minimum scale factor of the asteroid object
        public float maxScale; // The maximum scale factor of the asteroid object
        public GameObject asteroid; // The asteroid game object to spawn
        public float spawnRate; // The number of seconds before an asteroid is spawned

        private readonly Random _random = new Random(); // random object used to generate random numbers. Made readonly as it does not need to be changed

        private void Start()
        {
            InvokeRepeating(nameof(SpawnObject), 1, spawnRate); // Calls the SpawnObject() method after 1 second every "spawnRate" seconds
        }

        private void SpawnObject()
        {
            var locationX = (float) ((_random.NextDouble() * (maxX - minX)) + minX); // generate random double precision number, convert to float then scale with bounds to get random locations
            var locationY = (float) ((_random.NextDouble() * (maxY - minY)) + minY);
            var scale = (float) ((_random.NextDouble() * (maxScale - minScale)) + minScale); // same here

            asteroid.transform.position = new Vector3(locationX, locationY, 0); // place the asteroid at the randomly generated position
            asteroid.transform.localScale = new Vector3(scale, scale, 1); // Scale the asteroid according to the randomly generated scale
            Instantiate(asteroid); // Spawn the asteroid in the game
        }
    }
}
