using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject _topScore;
    [SerializeField] private GameObject _play;
    [SerializeField] private GameObject _topPlayers;
    [SerializeField] private GameObject _shop;
    [SerializeField] private GameObject _sound;
    void Start()
    {
        _play.GetComponent<Button>().onClick.AddListener(Play);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void Play()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
