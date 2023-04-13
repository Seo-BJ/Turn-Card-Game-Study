using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SaveSlot : MonoBehaviour
{
    // This is a static variable telling us which slot to use.
    public static int Slot = 0;
    // The scene we should load when a player has selected a slot.
    public const string LoadScene = "Scene1";

    void Start()
    {
        // This ensures that the OnClick method is called when the button is pressed.
        this.GetComponent<Button>().onClick.AddListener(OnClick);
    }

    void OnClick()
    {
        // Set the selected slot.
        // We use the sibling index as our slot number.
        Slot = transform.GetSiblingIndex();
        // Now load the first scene.
        SceneManager.LoadScene(LoadScene);
    }
}
