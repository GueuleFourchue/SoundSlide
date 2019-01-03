using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;


public class MenuScript : MonoBehaviour
{

    public CanvasGroup baseOptionsCanvas;
    public CanvasGroup inputOptionsCanvas;

    Transform menuPanel;

    Event keyEvent;

    Text buttonText;

    KeyCode newKey;



    bool waitingForKey;

    public bool inInputMenu = false;
    public Image globalBlackFade;
    private string previousText;


    void Start()

    {

        //Assign menuPanel to the Panel object in our Canvas

        //Make sure it's not active when the game starts

        menuPanel = transform.Find("Panel");

        //menuPanel.gameObject.SetActive(false);

        waitingForKey = false;
        inInputMenu = false;


        /*iterate through each child of the panel and check

         * the names of each one. Each if statement will

         * set each button's text component to display

         * the name of the key that is associated

         * with each command. Example: the ForwardKey

         * button will display "W" in the middle of it

         */

        for (int i = 0; i < menuPanel.childCount; i++)

        {
            if (menuPanel.GetChild(i).name == "LeftKey1")

                menuPanel.GetChild(i).GetComponentInChildren<Text>().text = InputsManager.IM.left1.ToString();

            else if (menuPanel.GetChild(i).name == "LeftKey2")

                menuPanel.GetChild(i).GetComponentInChildren<Text>().text = InputsManager.IM.left2.ToString();

            else if (menuPanel.GetChild(i).name == "RightKey1")

                menuPanel.GetChild(i).GetComponentInChildren<Text>().text = InputsManager.IM.right1.ToString();

            else if (menuPanel.GetChild(i).name == "RightKey2")

                menuPanel.GetChild(i).GetComponentInChildren<Text>().text = InputsManager.IM.right2.ToString();

        }

    }


    public void DisplayInputsMenu()
    {
        inInputMenu = true;

        baseOptionsCanvas.DOFade(0, 0.3f).OnComplete(() =>
        {
            baseOptionsCanvas.gameObject.SetActive(false);
            inputOptionsCanvas.gameObject.SetActive(true);
            inputOptionsCanvas.DOFade(1, 0.3f);
        });

    }
    public void BackToBaseMenu()
    {
        inInputMenu = false;

        inputOptionsCanvas.DOFade(0, 0.3f).OnComplete(() =>
        {
            inputOptionsCanvas.gameObject.SetActive(false);
            baseOptionsCanvas.gameObject.SetActive(true);
            baseOptionsCanvas.DOFade(1, 0.3f);
        });

    }


    void OnGUI()

    {

        /*keyEvent dictates what key our user presses

         * bt using Event.current to detect the current

         * event

         */

        keyEvent = Event.current;



        //Executes if a button gets pressed and

        //the user presses a key

        if (keyEvent.isKey && waitingForKey)

        {

            newKey = keyEvent.keyCode; //Assigns newKey to the key user presses

            waitingForKey = false;

        }

    }



    /*Buttons cannot call on Coroutines via OnClick().

     * Instead, we have it call StartAssignment, which will

     * call a coroutine in this script instead, only if we

     * are not already waiting for a key to be pressed.

     */

    public void StartAssignment(string keyName)
    {
        if (!waitingForKey)

            StartCoroutine(AssignKey(keyName));
    }



    //Assigns buttonText to the text component of

    //the button that was pressed

    public void SendText(Text text)
    {

        buttonText = text;

    }



    //Used for controlling the flow of our below Coroutine

    IEnumerator WaitForKey()
    {
        previousText = buttonText.text;

        buttonText.text = "...";
        while (!keyEvent.isKey)

            yield return null;

    }



    /*AssignKey takes a keyName as a parameter. The

     * keyName is checked in a switch statement. Each

     * case assigns the command that keyName represents

     * to the new key that the user presses, which is grabbed

     * in the OnGUI() function, above.

     */

    public IEnumerator AssignKey(string keyName)

    {

        waitingForKey = true;



        yield return WaitForKey(); //Executes endlessly until user presses a key

        if (newKey == KeyCode.Escape)
        {
            buttonText.text = previousText;
            yield break;
        }

        switch (keyName)

        {
            case "left1":

                InputsManager.IM.left1 = newKey; //set left to new keycode

                buttonText.text = InputsManager.IM.left1.ToString(); //set button text to new key

                PlayerPrefs.SetString("leftKey1", InputsManager.IM.left1.ToString()); //save new key to playerprefs

                break;

            case "left2":

                InputsManager.IM.left2 = newKey; //set left to new keycode

                buttonText.text = InputsManager.IM.left2.ToString(); //set button text to new key

                PlayerPrefs.SetString("leftKey2", InputsManager.IM.left2.ToString()); //save new key to playerprefs

                break;

            case "right1":

                InputsManager.IM.right1 = newKey; //set right to new keycode

                buttonText.text = InputsManager.IM.right1.ToString(); //set button text to new key

                PlayerPrefs.SetString("rightKey1", InputsManager.IM.right1.ToString()); //save new key to playerprefs

                break;

            case "right2":

                InputsManager.IM.right2 = newKey; //set right to new keycode

                buttonText.text = InputsManager.IM.right2.ToString(); //set button text to new key

                PlayerPrefs.SetString("rightKey2", InputsManager.IM.right2.ToString()); //save new key to playerprefs

                break;
        }



        yield return null;

    }

    public void LoadTutorial()
    {
        StartCoroutine(LoadTutoRoutine());
    }
    IEnumerator LoadTutoRoutine()
    {
        globalBlackFade.gameObject.SetActive(true);
        globalBlackFade.DOFade(1, 1);
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("TUTO");
    }
}