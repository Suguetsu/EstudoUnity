
using UnityEngine;
using UnityEngine.UI;

// reponsável por gerenciar a hud do jogo
public class HudManager : MonoBehaviour
{
    public static event System.Action<int> SetFood;

    // Start is called before the first frame update
    [Header("Live Settings")]
    [SerializeField] private GameObject[] livesIcon;
    [SerializeField] private GameObject outLine;


    [Header("Score Settings")]
    [SerializeField] private GameObject[] buttons;
    [SerializeField] Text score;
    [SerializeField] Text level;

    [Header("Start Screen Settings")]  // Área responsável por gerenciar a tela de inicialização.
    [SerializeField] Camera StartCam;
    [SerializeField] Camera mainCam;
    [SerializeField] GameObject homeScreen;
    [SerializeField] GameObject playerScreen;
    [SerializeField] GameObject[] allHudGame;

    [Header("Tutorial Config")]
    [SerializeField] GameObject[] tutorialControls;

    [SerializeField] private int countTuto;



    private int count = 0;

    private int id;

    private GameManager _gm;

    private void OnEnable()
    {

        for (int i = 0; i < buttons.Length; i++)
        {
            if (i != 0)
                buttons[i].gameObject.SetActive(false);
        }
    }


    void Start()
    {
        _gm = FindObjectOfType(typeof(GameManager)) as GameManager;

        Invetory.SendFruitToHud += Changebutton;
        InputButton.Outline += OutLineBtn;

        StarGameHud();


    }

    private void OnDestroy()
    {
        Invetory.SendFruitToHud -= Changebutton;
        InputButton.Outline -= OutLineBtn;
    }


    public void Update()
    {
        if (_gm.PauseGame()) return;

        if (Input.GetKeyDown(KeyCode.Keypad0) && buttons.Length > 0)
        {
            ChangeButtonInput(0);
            SetFood?.Invoke(0);
        }
        else if (Input.GetKeyDown(KeyCode.Keypad1) && buttons.Length > 1)
        {
            ChangeButtonInput(1);
            SetFood?.Invoke(1);
        }
        else if (Input.GetKeyDown(KeyCode.Keypad2) && buttons.Length > 2)
        {
            ChangeButtonInput(2);
            SetFood?.Invoke(2);
        }
        else if (Input.GetKeyDown(KeyCode.Keypad3) && buttons.Length > 3)
        {
            ChangeButtonInput(3);
            SetFood?.Invoke(3);
        }


    }
    public void ActiveGo(GameObject go)
    {
        go.gameObject.SetActive(!go.activeInHierarchy);
    }  // liga e desliga os controles visuais

    public void Changebutton(int id, Sprite icon)  // responável por mudar a imagem dos sprites
    {
        buttons[id].gameObject.SetActive(true);
        buttons[id].GetComponent<InputButton>().SetInput(id);
        buttons[id].transform.GetChild(0).GetComponent<Image>().sprite = icon;


        OutLineBtn(buttons[id].transform);
    }

    public void ChangeButtonInput(int idBtn)
    {
        OutLineBtn(buttons[idBtn].transform);
    }
    public void SetScoreLevel(int _score, int _lvl)
    {
        score.text = _score.ToString();
        level.text = _lvl.ToString();

    }

    public void SetIconLife(int _)
    {

        if (_ > 0)
            livesIcon[count].SetActive(false);
        else
        {

            for (int i = _; i < livesIcon.Length; i++)
            {


                //livesIcon[i].SetActive(true);

                Debug.Log("Religue os icones de vida aqui");

            }

            count = 0;
        }

        if (count < livesIcon.Length)
            count += _;

        // Debug.Log(count + "COUNT");
    }

    public void OutLineBtn(Transform trs)
    {
        // outline config
        outLine.transform.SetParent(trs.transform);
        outLine.transform.gameObject.SetActive(true);
        outLine.transform.localPosition = Vector3.zero;
        outLine.transform.SetAsFirstSibling();
    }

    public void HomeGameScreen()
    {

        if (_gm.GetStatusGame() == GameState.homeScreen)
        {
            mainCam.gameObject.SetActive(false);
            StartCam.gameObject.SetActive(true);
            homeScreen.gameObject.SetActive(StartCam.gameObject.activeInHierarchy);
            playerScreen.SetActive(homeScreen.gameObject.activeInHierarchy);

            for (int i = 0; i < allHudGame.Length; i++)
            {
                allHudGame[i].gameObject.SetActive(false);
            }


        }
        else
        {
            mainCam.gameObject.SetActive(true);
            StartCam.gameObject.SetActive(false);
            homeScreen.gameObject.SetActive(StartCam.gameObject.activeInHierarchy);
            playerScreen.SetActive(homeScreen.gameObject.activeInHierarchy);

            for (int i = 0; i < allHudGame.Length; i++)
            {
                allHudGame[i].gameObject.SetActive(true);
            }
        }
    }


    public void StarGameHud()
    {


        if (countTuto <= 1)
        {
            if (countTuto == 0)
            {
                tutorialControls[0].gameObject.SetActive(true);
                HomeGameScreen();



            }
            else if (countTuto == 1)
            {

                tutorialControls[0].gameObject.SetActive(false);
                tutorialControls[1].gameObject.SetActive(true);

            }




            countTuto++;


        }
        else
        {
            tutorialControls[0].gameObject.SetActive(false);
            tutorialControls[1].gameObject.SetActive(false);
            tutorialControls[2].gameObject.SetActive(false);

            _gm.SetStatusGame(GameState.game);
            countTuto = 2;

            HomeGameScreen();
            _gm.StartGame();





        }






    }

    public void ActiveDesactive(GameObject btn) => btn.gameObject.SetActive(!btn.gameObject.activeInHierarchy);


}
