using System.Collections;
using UnityEngine;
using TMPro;
using System.Linq;
using UnityEngine.UI;
/// <summary>
/// Class to handle UI interactions
/// </summary>
public class UtilsUI : MonoBehaviour
{
    private TMP_Text _textField;
    public bool clicked = false;
    public GameObject configScreen;
    public GameObject gameScreen;
    public GameObject buttonAddSides; 
    public GameObject buttonRemoveSides;
    public GameObject buttonAddCount;
    public GameObject buttonSubstractCount;
    public Texture tex;
    public GameObject toggleYellow;
    public GameObject delete;
    public GameObject currentDices;
    public static bool DicesCleared = false;
    public GameObject cam;
    public GameObject buttonZoomIn;
    public GameObject buttonZoomOut;
    private static ArrayList _diceList = new ArrayList();

    void Start(){
        _textField = GetComponent<TMP_Text>();
        
    }

    public static string GetSelectedColor(Toggle toggle){
        Debug.Log(toggle.group.ActiveToggles().Count());
        return toggle.group.ActiveToggles().FirstOrDefault().name;
 
    }
    /// <summary>
    /// lowers the Y-Position of the camera
    /// </summary>
    public void CameraZoomIn()
    {
        var yPos = cam.transform.position.y-10;
        cam.transform.position = new Vector3(0, yPos, 0);
        buttonZoomOut.SetActive(true);
        if (yPos == 5)
        {
            buttonZoomIn.SetActive(false);
        }
    }
    /// <summary>
    /// increases the Y-Position of the camera 
    /// </summary>
    public void CameraZoomOut()
    {
        var yPos = cam.transform.position.y +10;
        cam.transform.position = new Vector3(0, yPos, 0);
        buttonZoomIn.SetActive(true);
        if (yPos == 65)
        {
            buttonZoomOut.SetActive(false);
        }
    }
    /// <summary>
    /// Increases amount of Sides displayed in label, called when pressing ButtonAddSide
    /// </summary>
    public void UpdateText(){
        _textField = GetComponent<TMP_Text>();
        switch(_textField.text){
            case "4":
                _textField.text = "6";
                buttonRemoveSides.SetActive(true);
                break;
            case "6":
                _textField.text = "8";
                break;
            case "8":
                _textField.text = "10";
                break;
            case "10":
                _textField.text = "12";
                break;
             case "12":
                _textField.text = "20";
                buttonAddSides.SetActive(false);
                break;
        }
    }
    /// <summary>
    /// lowers number of Sides displayed, called upon pressing ButtonRemoveSide
    /// </summary>
    public void RemoveSides(){
        _textField = GetComponent<TMP_Text>();
        switch(_textField.text){
            case "20":
                _textField.text = "12";
                buttonAddSides.SetActive(true);
                break;
            case "12":
                _textField.text = "10";
                break;
            case "10":
                _textField.text = "8";
                break;
            case "8":
                _textField.text = "6";
                break;
             case "6":
                _textField.text = "4";
                buttonRemoveSides.SetActive(false);
                break;
        }
    }
    /// <summary>
    /// switches to ConfigScreen, called upon pressing DiceConfigButton
    /// </summary>
    public void SwitchToConfig()
    {
        gameScreen.SetActive(false);
        configScreen.SetActive(true);
    }
    /// <summary>
    /// increases displayed count of Dices, called upon pressing ButtonAddCount
    /// </summary>
    public void AddCount(){
        _textField = GetComponent<TMP_Text>();
        int count;
        int.TryParse(_textField.text,out count); 
        if(count == 1){
            buttonSubstractCount.SetActive(true);
        }
        else if(count == 19){
            buttonAddCount.SetActive(false);
        }
        count++;
        _textField.text = count.ToString();
    }
    /// <summary>
    /// decreases displayed count of Dices, called upon pressing ButtonRemoveCount
    /// </summary>
    public void SubstractCount(){
        _textField = GetComponent<TMP_Text>();
        int count;
        int.TryParse(_textField.text,out count);
        if(count == 2){
            buttonSubstractCount.SetActive(false);
        }else if(count == 20){
            buttonAddCount.SetActive(true);
        }
        count--;
        _textField.text = count.ToString();
    }
    /// <summary>
    /// switches to Game UI
    /// </summary>
    public void DisableCanvas(){
        configScreen.SetActive(false);
        gameScreen.SetActive(true);
    }

    
    /// <summary>
    /// Registers all toggles of the Group
    /// </summary>
    /// <param name="group">toggle group in which the toggles should be registered </param>
    /// <returns></returns>
    private ToggleGroup RegisterAllToggles(ToggleGroup group)
    {
        var toggleList = FindObjectsOfType<Toggle>(true);
        foreach (var toggle in toggleList)
        {
            group.RegisterToggle(toggle);
        }
        return group;
    }
    /// <summary>
    /// Destroys dice GameObjects and clears the initial dice List, called upon pressing Delete button
    /// </summary>
    public void RemoveDices()
    {
        int counter = 0;
        foreach (Dice diceGroups in _diceList)
        {
            counter = counter + diceGroups.count;
        }

        for (int i = 0; i < counter; i++)
        {
            Destroy(GameObject.Find(i.ToString()));
        }

        DicesCleared = true;
        _diceList.Clear();
        // reset the Text in the UI
        GameObject.Find("currentDices").GetComponent<TMP_Text>().text = "";
        delete.SetActive(false);
        
    }
    /// <summary>
    /// creates Dice Object with parameters count, amount of sides and color and adds them to the initial dice List
    /// called upon pressing ButtonAddDice
    /// </summary>
    public void UpdateDiceList()
    {
        // get current count and sides from labels
        TMP_Text textFieldCount = GameObject.Find("labelDiceCount").GetComponent<TMP_Text>();
        TMP_Text textFieldSides = GameObject.Find("labelSidesDice").GetComponent<TMP_Text>();
        Debug.Log(GameObject.Find("labelDiceCount").GetComponent<RectTransform>().anchoredPosition);
        int count;
        int.TryParse(textFieldCount.text, out count);
        int sides;
        int.TryParse(textFieldSides.text, out sides);
        Toggle tog = toggleYellow.GetComponent<Toggle>();
        ToggleGroup toggleGroup = RegisterAllToggles(tog.group);
        // create new Dice object
        Dice dice = new Dice(sides, count, toggleGroup.ActiveToggles().FirstOrDefault().name, null);
        _diceList.Add(dice);
        // display created Dices in the UI
        currentDices.GetComponent<TMP_Text>().text =currentDices.GetComponent<TMP_Text>().text+ dice.count + "x D" + dice.sides + " " + dice.color +"\n";
        delete.SetActive(true);
    }
    /// <summary>
    /// Return list of initial dices
    /// </summary>
    /// <returns>list of dices</returns>
    public static ArrayList GetDices(){
        return _diceList;
    }

    

}
