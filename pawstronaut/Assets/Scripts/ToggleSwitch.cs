using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ToggleSwitch : MonoBehaviour
{
    [SerializeField] private GameObject left;
    [SerializeField] private GameObject right;

    private Player player;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    public void Toggle()
    {
        left.SetActive(!left.activeSelf);
        right.SetActive(!right.activeSelf);
        if (left.activeSelf)
        {
            player.followTouchActive = true;
        }
        else
        {
            player.followTouchActive = false;
        }
    }
}
