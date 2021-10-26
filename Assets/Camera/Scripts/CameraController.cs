using UnityEngine;



namespace Camera.Scripts {
    /// <summary>
    /// Used to control the camera in order for it to do things such as follow the player and abide by bounds
    /// </summary>
    public class CameraController : MonoBehaviour
    {
        /// <summary>
        /// The transform object of the camera (contains rotation, position and scale)
        /// </summary>
        public Transform cameraTransform;

        /// <summary>
        /// The transform object of the player (contains rotation, position and scale)
        /// </summary>
        public Transform playerTransform;

        /// <summary>
        /// The farthest left background used to get the min bounds
        /// </summary>
        public SpriteRenderer leftmostBackgroundRenderer;

        /// <summary>
        /// The rightmost background used to get the max bounds
        /// </summary>
        public SpriteRenderer rightmostBackgroundRenderer;

        /// <summary>
        /// The maximum x-coordinate of the background/play area used to create the boundaries.
        /// </summary>
        private float _maxX;

        /// <summary>
        /// The minimum x-coordinate of the background/play area used to create the boundaries.
        /// </summary>
        private float _minX;

        /// <summary>
        /// The current x-axis coordinate of the camera screen boundary to the right
        /// </summary>
        private float _currentCameraRightBound;

        /// <summary>
        /// The current x-axis coordinate of the camera screen boundary to the left
        /// </summary>
        private float _currentCameraLeftBound;

        /// <summary>
        /// Value of half of the size of the camera screen size
        /// </summary>
        private float _halfWidth;

        /// <summary>
        /// The x-axis of the middle of the camera view;
        /// </summary>
        private float _cameraXAxis;


        // Start is called once before the first frame
        private void Start()
        {
            _maxX = rightmostBackgroundRenderer.bounds.max.x -
                    0.1f; // the max value of the x-coordinate of the farthest background to the right (right edge of background) with a .01 discrepancy
            _minX = leftmostBackgroundRenderer.bounds.min.x +
                    0.1f; // the min value of the x-coordinate of the farthest background to the left (left edge of background) with a .01 discrepancy
            // Note: if you have an odd number of background images with the middle positioned at 0,0, you can just take the negative of your max to get your min, or just absolute value both your position and your max.

            var mainCamera = GetComponent<UnityEngine.Camera>(); // get the main camera object
            _halfWidth =
                mainCamera.orthographicSize *
                mainCamera.aspect; // Take half the size of the width of the cameras screen area.

        }

        // Update is called once per frame
        private void Update()
        {
            _cameraXAxis = cameraTransform.position.x; // x-axis of the camera

            _currentCameraRightBound =
                _halfWidth +
                _cameraXAxis; // Take half the height (orthographicSize) and multiply it by the aspect ratio in order to find half the width to use to find the bound in reference to the position of the camera (midpoint)
            _currentCameraLeftBound =
                _cameraXAxis - _halfWidth; // X-axis of the camera (middle position) minus half the width



            if (_currentCameraLeftBound >= _minX && _currentCameraRightBound <= _maxX) // check if camera is in bounds
            {
                cameraTransform.SetPositionAndRotation(new Vector3(playerTransform.position.x, 0, -10),
                    cameraTransform.rotation); // have the camera move with the player
            }

            if (_currentCameraLeftBound <= _minX &&
                playerTransform.position.x >=
                cameraTransform.position
                    .x) // If the camera is out of bounds, once the player steps to the middle of the screen the camera will start moving again into bounds.
                cameraTransform.SetPositionAndRotation(new Vector3(playerTransform.position.x, 0, -10),
                    cameraTransform.rotation);

            if (_currentCameraRightBound >= _maxX && playerTransform.position.x <= cameraTransform.position.x)
                cameraTransform.SetPositionAndRotation(new Vector3(playerTransform.position.x, 0, -10),
                    cameraTransform.rotation);
        }
    }
}
