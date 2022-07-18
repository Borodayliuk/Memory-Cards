using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{
    [SerializeField] private GameObject _cardPrefab;
    [SerializeField] private Sprite[] _images;
    [SerializeField] private GameObject _scoreText;
    [SerializeField] private GameObject _timeText;
    [SerializeField] private GameObject _levelText;
    [SerializeField] private float _shakeDur, _shake;
    [SerializeField] private GameObject _pause;


    private int _level = 0;
    private int _countCards;
    private float _time;
    private int _score = 0;


    private GridS _grid;

    private MemoryCard _firstRevealed;
    public MemoryCard _secondRevealed;
    private bool _levelStart;

    private GameObject[,] _cards;
    private int _gridY;
    private int _gridX;

    private bool _isPaused;

    
    void Start()
    {
        _time = 10;
        _grid = GetComponent<GridS>();
        StartCoroutine(NextLevel());
        _pause.GetComponent<Button>().onClick.AddListener(Pause);
        
    }
    private void Update()
    {
        _scoreText.GetComponent<TextMeshProUGUI>().text = ("Score: " + _score);
        _timeText.GetComponent<TextMeshProUGUI>().text = ("Time: " + _time.ToString("0.0"));
        _levelText.GetComponent<TextMeshProUGUI>().text = ("Level: " + _level);
        if (_time > 0) {
            if (_levelStart)
            {
                _time -= Time.deltaTime;
            }
        }else
        {
            StartCoroutine(GameOver());
        }
    }

    public void GenerateCards()
    {
        int[] numbers = GenerateRandomNumbers();
        numbers = _ShuffleArrey(numbers);
        int index = 0;
        for (int x = 0; x < _gridX; x++)
        {
            for (int y = 0; y < _gridY; y++)
            {
                GameObject card = Instantiate(_cardPrefab);
                
                int id = numbers[index];
               
                
                card.GetComponent<MemoryCard>().SetCard(id, _images[id]);
               
                card.GetComponent<MemoryCard>().controller = this;
                _cards[x, y] = card;
                index++;
            }
        }
    }
    public int[] GenerateRandomNumbers()
    {
        int[] numbers = new int[_countCards];
        print(numbers.Length);
        int cardNumber = 0;
        for (int j = 0; j < _countCards; j+=2)
        {
            if (cardNumber < _images.Length)
            {
                    numbers[j] = cardNumber;
                    numbers[j + 1] = cardNumber;
                    cardNumber++;
            }
            else
            {
                cardNumber = 0;
                numbers[j] = cardNumber;
                numbers[j + 1] = cardNumber;
            }
            

        }
        for (int i = 0; i < _countCards; i++)
        {
            print(numbers[i]);
        }


        return numbers;
    }
    
    public void CardRevealed(MemoryCard card)
    {
        if (_firstRevealed == null)
        {
            _firstRevealed = card;
        }
        else if (_secondRevealed == null)
        {
           _secondRevealed = card;

            StartCoroutine(CheckMathc());
        }
    }
    private int[] _ShuffleArrey(int[] numbers)
    {
        int[] newArray = numbers.Clone() as int[];
        for (int i = 0; i < newArray.Length; i++)
        {
            int tmp = newArray[i];
            int r = Random.Range(i, numbers.Length);
            newArray[i] = newArray[r];
            newArray[r] = tmp;
        }
        return newArray;
    }

    private IEnumerator CheckMathc()
    {
        
        if (_firstRevealed.id == _secondRevealed.id)
        {
            _score++;
            yield return new WaitForSeconds(0.5f);
            _firstRevealed.DestroyCard();
            _secondRevealed.DestroyCard();
            if (_countCards - 2 > 0)
            {
                _time += 2;
                _countCards -= 2;
            }
            else
            {
                _levelStart = false;
                StartCoroutine(NextLevel());
            }
        }
        else
        {
            Camera.main.DOShakePosition(_shakeDur, _shake);
            yield return new WaitForSeconds(0.5f);
            _firstRevealed.Unreveal();
            _secondRevealed.Unreveal();
            _firstRevealed = null;
            _secondRevealed = null;
        }
        
        
    }
    private IEnumerator NextLevel()
    {
        _levelStart = false;
        yield return new WaitForSeconds(0.7f);
        _cards = null;
        _level++;
        if (_level != 1)
        {
            _time += 6;
        }
        if (_level < 15)
        {
            _countCards = _level * 2;
            if (_level == 7)
            {
                _countCards = 12;
            }
        }
        else
        {
            _countCards = 30;
        }
        int sqrt = (int)Mathf.Sqrt(_countCards);
        if (_countCards % sqrt == 0)
        {
            _gridY = sqrt;
            _gridX = _countCards / 2;
        }
        else
        {
            while (_countCards % sqrt != 0)
            {
                sqrt--;
            }
            _gridY = sqrt;
            _gridX = _countCards / _gridY;
        }
        _gridX = _countCards / _gridY;
        _cards = new GameObject[_gridX, _gridY];

        GenerateCards();
        _grid.GenerationGrid(_cards, new Vector3Int(_gridX, _gridY, 1));
        
        _levelStart = true;
    }
    private void Pause()
    {
        if (!_isPaused)
        {
            _isPaused = true;
            Time.timeScale = 0;
            _pause.GetComponent<ButtonVisual>().NextSprite();
        }else
        {
            _isPaused = false;
            Time.timeScale = 1;
            _pause.GetComponent<ButtonVisual>().NextSprite();
        }
    }
    IEnumerator GameOver()
    {
        yield return new WaitForSeconds(0);
        SceneManager.LoadScene("SampleScene");
    }
    public bool IsPaused()
    {
        return _isPaused;
    }
    
    



}
