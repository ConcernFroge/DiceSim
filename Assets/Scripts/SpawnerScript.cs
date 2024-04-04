using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using UnityEngine.Diagnostics;
using UnityEngine.UI;
using Random = System.Random;
/// <summary>
/// Class to spawn and roll dices
/// </summary>
public class SpawnerScript : MonoBehaviour
{
    public GameObject dice4;
    public GameObject dice6;
    public GameObject dice8;
    public GameObject dice10;
    public GameObject dice12;
    public GameObject dice20;
    public int diceNamingCounter;
    public bool rollIsDone = false;
    public GameObject resultField;
    public List<Dice> fullDiceList = new List<Dice>();
    public Texture tex;
    public GameObject configScreen;
    private static Random rnd;


    public void Start()
    {
        rnd = new Random();
    }

    // Update is called once per frame
    private void Update()
    {
        //if Dices have been rolled and the configuration screen is not being displayed, calculate the result and display it
        if(rollIsDone && !configScreen.activeSelf){
            string resultText = "Selected Dices:";
            
            foreach(Dice dice in fullDiceList){
                if (dice.GetGameObject().GetComponent<Dice>().selected)
                {
                    int result = dice.GetValue(dice.GetGameObject().transform);
                    resultText = resultText + " "+ result;
                }
     
            }
            resultField.GetComponent<TMP_Text>().text = resultText;
 
        }
        
    }

    public void FixedUpdate()
    {
        if (rollIsDone) return;
        if (fullDiceList.All(dice => dice.GetGameObject().GetComponent<Rigidbody>().velocity.magnitude == 0))
        {
            rollIsDone = true;
        }
    }
    /// <summary>
    /// Clears all remaining non selected Dices and rolls them again
    /// moves selected Dices to the side and scales them down
    /// </summary>
    private void ClearDices(){
        ArrayList diceList = UtilsUI.GetDices();
        foreach (Dice dice in fullDiceList){
            if (!dice.GetGameObject().GetComponent<Dice>().selected)
            {
                //Check if dice has 4 or 12 sides as these models need a different scale than the rest
                if (dice.sides != 12 && dice.sides != 4)
                {
                    dice.GetGameObject().transform.localScale = new Vector3(2, 2, 2);
                }else if (dice.sides == 4)
                {
                    dice.GetGameObject().transform.localScale = new Vector3(3, 3, 3);
                }
                else
                {
                    dice.GetGameObject().transform.localScale = new Vector3(1.4f, 1.4f, 1.4f);
                }
                
                RollDices(dice.GetGameObject());
            }
            else
            {
                int zPos;
                int.TryParse(dice.GetGameObject().name, out zPos);
                //Check if dice has 4 or 12 sides as these models need a different scale than the rest
                if (dice.sides != 12 && dice.sides != 4)
                {
                    dice.GetGameObject().transform.localScale = new Vector3(1, 1, 1);
                }else if (dice.sides == 4)
                {
                    dice.GetGameObject().transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
                }
                else
                {
                    dice.GetGameObject().transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
                }
                dice.GetGameObject().transform.position = new Vector3(-23.5f, 1, zPos*2-13);
            }
        }
    }
    /// <summary>
    /// Physically rolls the dices with a random rotation and velocity
    /// </summary>
    /// <param name="diceObject">dice to be rolled</param>
    private void RollDices(GameObject diceObject)
    {
        
        diceObject.transform.position = new Vector3(20,5,rnd.Next(-1,1));
        diceObject.transform.Rotate(rnd.Next(0, 360), rnd.Next(0, 360), rnd.Next(0, 360)); 
        Vector3 m_NewForce = new Vector3(rnd.Next(-15,0), rnd.Next(-5,0), rnd.Next(-5,5));
        diceObject.GetComponent<Rigidbody>().AddForce(m_NewForce, ForceMode.Impulse);
    }

    
    /// <summary>
    /// Called upon pressing spawn button, instantiates dices with their corresponding 3D model
    /// </summary>
    public void SpawnDices(){
    //check if old dices were destroyed
        if (UtilsUI.DicesCleared)
        {
            fullDiceList.Clear();
            UtilsUI.DicesCleared = false;
        }
        ClearDices();
        
        ArrayList diceList = UtilsUI.GetDices();
        rollIsDone = false;
        diceNamingCounter = 0;
        //determine which prefab to use
        foreach (Dice dice in diceList ){
            switch(dice.sides){
                case 20:
                    InstantiateDices(dice.count, dice20, dice);
                    break;
                case 12:
                    InstantiateDices(dice.count, dice12, dice);
                    break;
                case 10:
                    InstantiateDices(dice.count, dice10, dice);
                    break;
                case 8:
                    InstantiateDices(dice.count, dice8, dice);
                    break;
                case 6:
                    InstantiateDices(dice.count, dice6, dice);
                    break;
                case 4:
                    InstantiateDices(dice.count, dice4, dice);
                    break;
        }
        }
        
    }

    /// <summary>
    /// instantiates the Dice GameObjects
    /// </summary>
    /// <param name="count"> Number of Dices to instantiate</param>
    /// <param name="diceModel">3d Model</param>
    /// <param name="dice">Dice to be created</param>
    private void InstantiateDices(int count, GameObject diceModel, Dice dice){
        for (int i = 0; i < count; i++)
        {
            GameObject diceObject = GameObject.Find(diceNamingCounter.ToString());
            //if dice already exists skip instantiating 
            if (!diceObject)
            {
                diceObject = Instantiate(diceModel, new Vector3(0, 5, 0), Quaternion.identity);
                diceObject.name = diceNamingCounter.ToString();
                dice.SetGameObject(diceObject);
                dice.SetColor();
                Dice newDice = new Dice(dice.sides, dice.count, dice.color, diceObject);
                RollDices(diceObject);
                fullDiceList.Add(newDice);
            }
            diceNamingCounter++;
       
        }
    }
}
