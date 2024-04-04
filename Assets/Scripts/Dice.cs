using UnityEngine;
/// <summary>
/// Class to define diceObjects
/// </summary>
public class Dice : MonoBehaviour
{
    
    public int sides;
    public int count;
    public string color;
    public Texture texture;
    private GameObject _gameObject;

    public bool selected = false;
   /// <summary>
   /// Constructor
   /// </summary>
   /// <param name="numberSides">Number of Sides</param>
   /// <param name="numberOfDices">Number of Dices to spawn</param>
   /// <param name="colorOfDice">Name of Texture</param>
   /// <param name="diceObject">GameObject of the Dice</param>
    public Dice(int numberSides, int numberOfDices, string colorOfDice, GameObject diceObject)
    {
        sides = numberSides;
        count = numberOfDices;
        color = colorOfDice;
        _gameObject = diceObject;
    }

    /// <summary>
    /// Sets the texture of the dice
    /// </summary>
    public void SetColor()
    {
        //get Texture from Resources
        Texture tex = Resources.Load("D"+sides+" "+color) as Texture;
        Renderer renderer = _gameObject.GetComponent<Renderer>();
        renderer.material.EnableKeyword ("_NORMALMAP");
        renderer.material.EnableKeyword ("_METALLICGLOSSMAP");
        renderer.material.SetTexture("_MainTex",tex);
        texture = tex;
    }
    /// <summary>
    /// selects the dice
    /// </summary>
    private void OnMouseDown()
    {
        selected = !selected;
        
    }
    public void SetSelected(bool value)
    {
        selected = value;
    }
    /// <summary>
    /// Returns the dices GameObject
    /// </summary>
    /// <returns>GameObject</returns>
    public GameObject GetGameObject(){
        return _gameObject;
    }
    /// <summary>
    /// Sets the dices GameObject
    /// </summary>
    /// <param name="obj">GameObject to be set</param>
    public void SetGameObject(GameObject obj){
        _gameObject = obj;
        Debug.Log("Name: " + _gameObject.name);
    }
    /// <summary>
    /// Calculates the result of the roll for the individual dice, depending on the amount of sides
    /// </summary>
    /// <param name="transform"></param>
    /// <returns></returns>
    public int GetValue(Transform transform){
        switch(sides){
            case 20:
                return CalculateResult20Sided(transform);
            case 12:
                return CalculateResult12Sided(transform);
            case 10:
                return CalculateResult10Sided(transform);
            case 8:
                return CalculateResult8Sided(transform);
            case 6:
                Debug.Log("Calculating Result for 6 Sided");
                return CalculateResult6Sided(transform);
            case 4:
                return CalculateResult4Sided(transform);
            default:
                return 0;
        }
    }
    /// <summary>
    /// Calculates the result of a six sided dice
    /// </summary>
    /// <param name="transform">Transform of the dice</param>
    /// <returns>result</returns>
    private int CalculateResult6Sided(Transform transform){
        float maxDot = -Mathf.Infinity;
        int result = 0;
        int sideResult = 0;
        Vector3[] normals = GetNormals6Sided(transform);
        for (int i = 0; i < 6; i++)
        {
            //side that faces upwards has the highest dot product with the Vector(0,1,0)
            float dot = Vector3.Dot(Vector3.up, normals[i]);
            
            if (dot > maxDot)
            {
                maxDot = dot;
                sideResult = i + 1;
            }
        }
        result = sideResult;
        return result;
    }  
    /// <summary>
    /// Calculates the result of a four sided dice
    /// </summary>
    /// <param name="transform"></param>
    /// <returns></returns>
     private int CalculateResult4Sided(Transform transform){
        float maxDot = -Mathf.Infinity;
        int result = 0;
        int sideResult = 0;
        Vector3[] normals = GetNormals4Sided(transform);
        for (int i = 0; i < 4; i++)
        {
            float dot = Vector3.Dot(Vector3.up, normals[i]);
            
            if (dot > maxDot)
            {
                maxDot = dot;
                sideResult = i + 1;
            }
        }
        result = sideResult;
        return result;
    } 
    /// <summary>
    /// Calculates the result of a ten sided dice
    /// </summary>
    /// <param name="transform"></param>
    /// <returns></returns>
    private int CalculateResult10Sided(Transform transform){
        float maxDot = -Mathf.Infinity;
        int result = 0;
        int sideResult = 0;
        Vector3[] normals = GetNormals10Sided(transform);
        for (int i = 0; i < 10; i++)
        {
            float dot = Vector3.Dot(Vector3.up, normals[i]);
            
            if (dot > maxDot)
            {
                maxDot = dot;
                sideResult = i;
            }
        }
        result = sideResult;
        return result;
    } 
    /// <summary>
    /// Calculates the result of an 8 sided dice
    /// </summary>
    /// <param name="transform"></param>
    /// <returns></returns>
    private int CalculateResult8Sided(Transform transform){
        float maxDot = -Mathf.Infinity;
        int result = 0;
        int sideResult = 0;
        Vector3[] normals = GetNormals8SidedF2(transform);
        for (int i = 0; i < 8; i++)
        {
            float dot = Vector3.Dot(Vector3.up, normals[i]);
            
            if (dot > maxDot)
            {
                maxDot = dot;
                sideResult = i + 1;
            }
        }
        result = sideResult;
        return result;
    } 
    /// <summary>
    /// Calculates the result of a twelve sided dice
    /// </summary>
    /// <param name="transform"></param>
    /// <returns></returns>
    private int CalculateResult12Sided(Transform transform){
        float maxDot = -Mathf.Infinity;
        int result = 0;
        int sideResult = 0;
        Vector3[] normals = GetNormals12Sided(transform);
        for (int i = 0; i < 12; i++)
        {
            float dot = Vector3.Dot(Vector3.up, normals[i]);
            
            if (dot > maxDot)
            {
                maxDot = dot;
                sideResult = i + 1;
            }
        }
        result = sideResult;
        return result;
    } 
    /// <summary>
    /// Calculates the result of a 20 sided dice
    /// </summary>
    /// <param name="transform"></param>
    /// <returns></returns>
    private int CalculateResult20Sided(Transform transform){
        float maxDot = -Mathf.Infinity;
        int result = 0;
        int sideResult = 0;
        Vector3[] normals = GetNormals20SidedF(transform);
        for (int i = 0; i < 20; i++)
        {
            float dot = Vector3.Dot(Vector3.up, normals[i]);
            
            if (dot > maxDot)
            {
                maxDot = dot;
                sideResult = i + 1;
            }
        }
        result = sideResult;
        return result;
    }
    /// <summary>
    /// Gets the normals for a 6 sided dice
    /// each Vector represents one side of the dice
    /// </summary>
    /// <param name="transform">transform of the dice</param>
    /// <returns>Normals</returns>
    private Vector3[] GetNormals6Sided(Transform transform){
        Vector3[] normals = new Vector3[]{
        transform.forward,       // Side 1
        transform.up,         // Side 2
        -transform.right,       // Side 3
        transform.right,      // Side 4
        -transform.up,       // Side 5
        -transform.forward,    // Side 6
        };
        return normals;
    }
    /// <summary>
    /// Gets the normals for a 4 sided dice
    /// each Vector represents one side of the dice
    /// </summary>
    /// <param name="transform">transform of the dice</param>
    /// <returns>normals</returns>
    private Vector3[] GetNormals4Sided(Transform transform){
        Vector3[] normals = new Vector3[]{
        
        transform.up,         // Side 1
        transform.forward + transform.right, // side 2
        -transform.forward,        //Side 3       
        -transform.up + transform.forward
        };
        return normals;
    }

    private Vector3[] GetNormals8Sided(Transform transform){
        Vector3[] normals = new Vector3[]{
        transform.up + transform.forward, //side 1
        -transform.forward -transform.right, //Side 2
        transform.forward - transform.right, // side 3
        transform.up - transform.forward, // side 4
        transform.forward - transform.up, //side 5
        transform.right - transform.forward, // side 6
        transform.forward + transform.right, //side 7
        -transform.up -transform.forward // side 8
        
        };
        return normals;
    }
    /// <summary>
    /// Gets the normals for a 10 sided dice
    /// each Vector represents one side of the dice
    /// </summary>
    /// <param name="transform">transform of the dice</param>
    /// <returns>normals</returns>
    private Vector3[] GetNormals10Sided(Transform transform){
        Vector3[] normals = new Vector3[]{
            transform.up - transform.right, //Side 0
            -transform.up - transform.forward, //Side 1
            transform.up -transform.forward, // Side 2
            -transform.up -transform.right + transform.forward, // Side 3
            transform.right + transform.up + transform.forward, // Side 4
            -transform.up - transform.right - transform.forward, // Side 5
            transform.up + transform.right - transform.forward, // Side 6
            -transform.up + transform.forward, // Side 7
            transform.up + transform.forward, // Side 8
            -transform.up + transform.right // Side 9
        };
        return normals;
    }
    /// <summary>
    /// Gets the normals for a 12 sided dice
    /// each Vector represents one side of the dice
    /// </summary>
    /// <param name="transform">transform of the dice</param>
    /// <returns>normals</returns>
    private Vector3[] GetNormals12Sided(Transform transform){
        Vector3[] normals = new Vector3[]{
            transform.TransformDirection(new Vector3(-30,50,95)), // Side 1
            transform.TransformDirection(new Vector3(99,-50,0)), // Side 2
            transform.TransformDirection(new Vector3(0,112,0)), // Side 3
            transform.TransformDirection(new Vector3(-80,-50,-59)), // Side 4
            transform.TransformDirection(new Vector3(30,-50,95)), // Side 5
            transform.TransformDirection(new Vector3(80,50,-59)), // Side 6
            transform.TransformDirection(new Vector3(-80,-50,59)), // Side 7
            transform.TransformDirection(new Vector3(-30,50,-95)), // Side 8
            transform.TransformDirection(new Vector3(80,50,59)), // Side 9
            transform.TransformDirection(new Vector3(0,-112,0)), // Side 10
            transform.TransformDirection(new Vector3(-80,50,59)), // Side 11
            transform.TransformDirection(new Vector3(30,-50,-95)), // Side 12
        };
        return normals;
    }

    private Vector3[] GetNormals20Sided(Transform transform){
        Vector3[] normals = new Vector3[]{
            new Vector3(-23,-14,70), //Side 1
            new Vector3(-23,-14,-70), //Side 2
            new Vector3(14,60,44), //Side 3
            new Vector3(37,-60,-27), //Side 4
            new Vector3(-46,-60,0), //Side 5
            new Vector3(74,14,0), //Side 6
            new Vector3(-60,14,44), //Side 7
            new Vector3(14,60,-44), //Side 8
            new Vector3(60,-14,44), //Side 9
            new Vector3(-37,60,-27), //Side 10
            new Vector3(37,-60,27), //Side 11
            new Vector3(-60,14,-44), //Side 12
            new Vector3(-14,-60,44), //Side 13
            new Vector3(60,-14,-44), //Side 14
            new Vector3(-74,-14,0), //Side 15
            new Vector3(46,60,0), //Side 16
            new Vector3(-37,60,27), //Side 17
            new Vector3(-14,-60,-44), //Side 18
            new Vector3(23,14,71), //Side 19
            new Vector3(23,-14,-71) //Side 20
        };
        return normals;
    }
    /// <summary>
    /// Gets the normals for a 20 sided dice
    /// each Vector represents one side of the dice
    /// </summary>
    /// <param name="transform">transform of the dice</param>
    /// <returns>normals</returns>
    private Vector3[] GetNormals20SidedF(Transform transform){
        Vector3[] normals = new Vector3[]{
            
            transform.TransformDirection(new Vector3(23,-14,70)), //F
            transform.TransformDirection(new Vector3(23,-14,-70)), //Side 2F
            transform.TransformDirection(new Vector3(-14,60,44)), //Side 3F
            transform.TransformDirection(new Vector3(-37,-60,-27)), //Side 4F
            transform.TransformDirection(new Vector3(46,-60,0)), //Side 5F
            transform.TransformDirection(new Vector3(-74,14,0)), //Side 6F
            transform.TransformDirection(new Vector3(60,14,44)), //Side 7F
            transform.TransformDirection(new Vector3(-14,60,-44)), //Side 8F
            transform.TransformDirection(new Vector3(-60,-14,44)), //Side 9F
            transform.TransformDirection(new Vector3(37,60,-27)), //Side 10F
            transform.TransformDirection(new Vector3(-37,-60,27)), //Side 11F
            transform.TransformDirection(new Vector3(60,14,-44)), //Side 12
            transform.TransformDirection(new Vector3(14,-60,44)), //Side 13F
            transform.TransformDirection(new Vector3(-60,-14,-44)), //Side 14F
            transform.TransformDirection(new Vector3(74,-14,0)), //Side 15F
            transform.TransformDirection(new Vector3(-46,60,0)), //Side 16F
            transform.TransformDirection(new Vector3(37,60,27)), //Side 17F
            transform.TransformDirection(new Vector3(14,-60,-44)), //Side 18F
            transform.TransformDirection(new Vector3(-23,14,71)), //Side 19F
            transform.TransformDirection(new Vector3(-23,14,-71)) //Side 20F
        };
        return normals;
    }

    private Vector3[] GetNormals20SidedF2(Transform transform){
        Vector3[] normals = new Vector3[]{
            
            transform.InverseTransformDirection(new Vector3(23,-14,70)), //F
            transform.InverseTransformDirection(new Vector3(23,-14,-70)), //Side 2F
            transform.InverseTransformDirection(new Vector3(37,60,-27)),//Side 3F2
            transform.InverseTransformDirection(new Vector3(-37,-60,-27)), //Side 4F
            transform.InverseTransformDirection(new Vector3(46,-60,0)), //Side 5F
            transform.InverseTransformDirection(new Vector3(-60,-14,44)), //Side 6F
            transform.InverseTransformDirection(new Vector3(60,14,44)), //Side 7F
            transform.InverseTransformDirection(new Vector3(-14,60,44)), //Side 8F
            
            transform.InverseTransformDirection(new Vector3(-74,14,0)), //Side 9F2
             
            
            transform.InverseTransformDirection(new Vector3(-14,60,-44)), //Side 10F
            transform.InverseTransformDirection(new Vector3(-37,-60,27)), //Side 11F
            transform.InverseTransformDirection(new Vector3(60,14,-44)), //Side 12F
            transform.InverseTransformDirection(new Vector3(14,-60,-44)),  //Side 13F2
            transform.InverseTransformDirection(new Vector3(-60,-14,-44)), //Side 14F
            transform.InverseTransformDirection(new Vector3(74,-14,0)), //Side 15F
            transform.InverseTransformDirection(new Vector3(-46,60,0)), //Side 16F
            transform.InverseTransformDirection(new Vector3(37,60,27)), //Side 17F
            transform.InverseTransformDirection(new Vector3(14,-60,44)),//Side 18F
            transform.InverseTransformDirection(new Vector3(-23,14,71)), //Side 19F
            transform.InverseTransformDirection(new Vector3(-23,14,-71)) //Side 20F
        };
        return normals;
    }
    /// <summary>
    /// Gets the normals for an 8 sided dice
    /// each Vector represents one side of the dice
    /// </summary>
    /// <param name="transform">transform of the dice</param>
    /// <returns>normals</returns>
     private Vector3[] GetNormals8SidedF2(Transform transform){
        Vector3[] normals = new Vector3[]{
            transform.TransformDirection(new Vector3(0,50,34)), // Side 1
            transform.TransformDirection(new Vector3(-50,0,-34)), // Side 2
            transform.TransformDirection(new Vector3(-50,0,34)), // Side 3
            transform.TransformDirection(new Vector3(0,50,-34)), // Side 4
            transform.TransformDirection(new Vector3(0,-50,34)), // Side 5
            transform.TransformDirection(new Vector3(50,0,-34)), // Side 6
            transform.TransformDirection(new Vector3(50,0,34)), // Side 7
            transform.TransformDirection(new Vector3(0,-50,-34)) // Side 8
                   
        };
        return normals;
    }
}
