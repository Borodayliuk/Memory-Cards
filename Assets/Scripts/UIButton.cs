using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIButton : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnMouseUp()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
