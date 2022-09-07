using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
//using System.String.Format;



public class ArrowSelector : MonoBehaviour
{
    public Dictionary<string, Color> list = new Dictionary<string, Color>()
    {
        { "fire", Color.red },
        { "ice", Color.blue },
        { "volley", Color.green},
        { "AoE", Color.yellow },
    };



    public int currentArrowIndex;
    public int[] selectedArrows;
    public int quiverSize;

    void Start()
    {
        
        loadArrows();
    }
    
    public void loadArrows()
    {
        //Do actions - pause / stun state transfer.

        selectedArrows = new int[quiverSize];
        for(int i = 0; i < selectedArrows.Length; i++)
        {
            //range values are placeholder for now- this represents 3 different arrow types
            // (1, 2, 3) are the possible returns
            selectedArrows[i] = Random.Range(1, 5);
        }
        currentArrowIndex = 3;
        for( int i = 0; i < selectedArrows.Length; i++ )
        {
            Debug.Log( selectedArrows[i] );
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown("c")) 
        {
            Debug.Log(selectedArrows[currentArrowIndex]);
        }
        if (Input.GetKeyDown("o"))
        {
            //0 represents used
            if(currentArrowIndex >= 0)
            {
                selectedArrows[currentArrowIndex] = 0;
                currentArrowIndex = currentArrowIndex - 1;
                for( int i = 0; i < selectedArrows.Length; i++ )
                {
                    Debug.Log( selectedArrows[i] );

                    gameObject.GetComponent<Renderer>().material.color = list.ElementAt(selectedArrows[i]).Value;
                    list.ElementAt(selectedArrows[i]);
                }
            }
            else
            {
                //Do some things instead - more behaviours to be added.
                loadArrows();
            }
        }
    }
}

