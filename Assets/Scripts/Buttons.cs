using UnityEngine;
using UnityEngine.SceneManagement;
public class Buttons : MonoBehaviour
{

    public void L1Load()
    {
        SceneManager.LoadScene(1);
    }

    public void MenuLoad()
    {
        SceneManager.LoadScene(0);
    }
}
