using UnityEngine;
using UnityEngine.SceneManagement;


namespace Menu.Scripts {
    /// <summary>
    /// Contains a collection of functions used to control the actions of the menu buttons
    /// </summary>
    public class MainMenu : MonoBehaviour
    {
        /// <summary>
        /// Called when the play button is pressed
        /// Used to load into our game scene ("Game")
        /// </summary>
        public void PlayGame()
        {
            SceneManager
                .LoadScene("Game"); // Loads the scene named "Game", which contains the actual game. This can also be done by indexing by Build Settings
            // Note: In order to load a scene, the scene must be added in our build settings (File -> Build Settings -> Add Open Scenes)
        }

        // Note: We do not need an Options function as deactivating objects in unity can be done from the editor, as the function is inherited by default

        /// <summary>
        /// Called when the Quit Button is pressed
        /// Used to quit the application
        /// </summary>
        public void QuitGame()
        {
            Application.Quit(); // Tells the application to QUIT
        }
    }
}
