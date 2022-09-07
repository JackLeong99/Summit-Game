using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class DialogueBox : MonoBehaviour
{
    
    
    //public static DialogueBox instance;
    public Dialogue dialogue;
    public DialogueTrigger dialogueTrigger;

    public int currentDialogueIndex;
    public string[] currentDialogueLines;


    //Conditions for selecting which text to display

        //determine what type of dialogue should be played - 0 = firstspawn, 1 = spawn, 2 = respawn, 3 = other
        //public int whichType;


        public int numOfDeaths;
        public bool isGlitched;
        

        //conditions to test if meeting boss for first time
        public bool seenIwazaru;
        public bool seenReaper;
        public bool seenArcher;
        public bool seenMage;
        public bool seenGameMaster;


        //conditions based on boss deaths
            public int deathsToIwazaru;
            public int deathsToReaper;
            public int deathsToArcher;
            public int deathsToMage;
            public int deathsToGameMaster;

            //determine what the last boss player died to was 
            //0 for no deaths yet, 1 = Iwazaru, 2 = reaper, 3 = archer, 4 = mage, 5 = game master
            public int lastKilledBy;


        

        //
    //

    // [System.Serializable]
    // public class visualElements
    // {
    //     public GameObject narrativeCanvas;
    //     public Text dialogueText1;
    // }
    // public GameObject narrativeCanvas {get { return visualElements.narrativeCanvas;}}
    // public Text dialogueText1 { get { return visualElements.dialogueText1;}}

    void Start()
    {
        //instance = this;
        meetIwazaru();

    }
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            meetIwazaru();
        }
    }

    //public string[,] messageOptions;

    // Start is called before the first frame update
    public void meetIwazaru()
    {
        currentDialogueLines = new string[10];
        currentDialogueIndex = 0;

        //First meeting
        if(!seenIwazaru)
        {
            int tempDecider = randomSelector(1, 4);
            if(tempDecider == 1)
            {
                currentDialogueLines[0] = ("This is the first dialogue set of 3 which can occur");
                currentDialogueIndex += 1;
                currentDialogueLines[1] = ("first time Meeting Iwazaru 1");
                currentDialogueIndex += 1;
                currentDialogueLines[2] = ("first time Meeting Iwazaru 2");
                currentDialogueIndex += 1;
                currentDialogueLines[3] = ("first time Meeting Iwazaru 3");
                currentDialogueIndex += 1;
                currentDialogueLines[4] = ("first time Meeting Iwazaru 4");
                
            }
            else if(tempDecider == 1)
            {
                currentDialogueLines[0] = ("This is the second dialogue set of 3 which can occur");
                currentDialogueIndex += 1;
                currentDialogueLines[1] = ("first time Meeting Iwazaru 1");
                currentDialogueIndex += 1;
                currentDialogueLines[2] = ("first time Meeting Iwazaru 2");
                currentDialogueIndex += 1;
                currentDialogueLines[3] = ("first time Meeting Iwazaru 3");
                currentDialogueIndex += 1;
                currentDialogueLines[4] = ("first time Meeting Iwazaru 4");

            }
            else
            {
                currentDialogueLines[0] = ("This is the third dialogue set of 3 which can occur");
                currentDialogueIndex += 1;
                currentDialogueLines[1] = ("first time Meeting Iwazaru 1");
                currentDialogueIndex += 1;
                currentDialogueLines[2] = ("first time Meeting Iwazaru 2");
                currentDialogueIndex += 1;
                currentDialogueLines[3] = ("first time Meeting Iwazaru 3");
                currentDialogueIndex += 1;
                currentDialogueLines[4] = ("first time Meeting Iwazaru 4");
                
            }
            

            seenIwazaru = true;
        }

        else if(deathsToIwazaru == 0)
        {
            int tempDecider = randomSelector(1, 3);
            if(tempDecider == 1)
            {
                currentDialogueLines[0] = ("Iwazaru, Not glitched, never killed, 0 death, line 1");
                        
            }
            else
            {
                currentDialogueLines[0] = ("Iwazaru, Not glitched, never killed, 0 death, line 2");
                        
            }
        }

        // else if(isGlitched)
        // {
        //     int tempDecider = randomSelector(1, 4);
        //     if(tempDecider == 1)
        //     {
        //         currentDialogueLines[0] = ("glitched, line 1");
        //         
            
        //     }
        //     else if(tempDecider == 2)
        //     {
        //         currentDialogueLines[0] = ("glitched, line 2");
        //         
        //     }
        //     else
        //     {
        //         currentDialogueLines[0] = ("glitched, line 3");
        //         
        //     }
        // }
        // else{
        //     if(lastKilledBy == 1)
        //     {
        //         if(deathsToIwazaru == 1)
        //         {
        //             int tempDecider = randomSelector(1, 3);
        //             if(tempDecider == 1)
        //             {
        //                 currentDialogueLines[0] = ("Not glitched, last killed by Iwazaru, 1 death, line 1");
        //                 
        //             }
        //             else
        //             {
        //                 currentDialogueLines[0] = ("Not glitched, last killed by Iwazaru, 1 death, line 2");
        //                 
        //             }

        //         }

        //         else if(deathsToIwazaru == 2)
        //         {
        //             int tempDecider = randomSelector(1, 3);
        //             if(tempDecider == 1)
        //             {
        //                 currentDialogueLines[0] = ("Not glitched, last killed by Iwazaru, 2 deaths, line 1");
        //                 
        //             }
        //             else
        //             {
        //                 currentDialogueLines[0] = ("Not glitched, last killed by Iwazaru, 2 deaths, line 2");
        //                 
        //             }
        //         }
        //         //more than 2 deaths to Iwazaru
        //         else
        //         {
        //             int tempDecider = randomSelector(1, 4);
        //             if(tempDecider == 1)
        //             {
        //                 currentDialogueLines[0] = ("Not glitched, last killed by Iwazaru, > 2 deaths, line 1");
        //                 
        //             }
        //             else if(tempDecider == 2)
        //             {
        //                 currentDialogueLines[0] = ("Not glitched, last killed by Iwazaru, > 2 deaths, line 2");
        //                 
        //             }
        //             else
        //             {
        //                 currentDialogueLines[0] = ("Not glitched, last killed by Iwazaru, > 2 deaths, line 3");
        //                 
        //             }
        //         }
        //     }
        //     else
        //     {
        //         if(deathsToIwazaru == 0)
        //         {
        //             int tempDecider = randomSelector(1, 4);
        //             if(tempDecider == 1)
        //             {
        //                 currentDialogueLines[0] = ("Not glitched, last killed by Other, 0 deaths, line 1");
        //                 
        //             }
        //             else if(tempDecider == 2)
        //             {
        //                 currentDialogueLines[0] = ("Not glitched, last killed by Other, 0 deaths, line 2");
        //                 
        //             }
        //             else
        //             {
        //                 currentDialogueLines[0] = ("Not glitched, last killed by Other, 0 deaths, line 3");
        //                 
        //             }

                    
        //         }
        //         else if(deathsToIwazaru == 1)
        //         {
        //             int tempDecider = randomSelector(1, 3);
        //             if(tempDecider == 1)
        //             {
        //                 currentDialogueLines[0] = ("Not glitched, last killed by Other, 1 death, line 1");
        //                 
        //             }
        //             else
        //             {
        //                 currentDialogueLines[0] = ("Not glitched, last killed by Other, 1 death, line 2");
        //                 
        //             }

        //         }

        //         else if(deathsToIwazaru == 2)
        //         {
        //             int tempDecider = randomSelector(1, 3);
        //             if(tempDecider == 1)
        //             {
        //                 currentDialogueLines[0] = ("Not glitched, last killed by Other, 2 deaths, line 1");
        //                 
        //             }
        //             else
        //             {
        //                 currentDialogueLines[0] = ("Not glitched, last killed by Other, 2 deaths, line 2");
        //                 
        //             }
        //         }
        //         //more than 2 deaths to Iwazaru
        //         else
        //         {
        //             int tempDecider = randomSelector(1, 4);
        //             if(tempDecider == 1)
        //             {
        //                 currentDialogueLines[0] = ("Not glitched, last killed by Other, > 2 deaths, line 1");
        //                 
        //             }
        //             else if(tempDecider == 2)
        //             {
        //                 currentDialogueLines[0] = ("Not glitched, last killed by Other, > 2 deaths, line 2");
        //                 
        //             }
        //             else
        //             {
        //                 currentDialogueLines[0] = ("Not glitched, last killed by Other, > 2 deaths, line 3");
        //                 
        //             }
        //         }
        //     }
            
        // }

        displayText();
    }

    public void death()
    {

    }
    // public void meetReaper()
    // {
    //     if(isGlitched)
    //     {
    //         int tempDecider = randomSelector(1, 4);
    //         if(tempDecider == 1)
    //         {
    //             currentDialogueLines[0] = ("Reaper, glitched, line 1");
    //             
            
    //         }
    //         else if(tempDecider == 2)
    //         {
    //             currentDialogueLines[0] = ("Reaper, glitched, line 2");
    //             
    //         }
    //         else
    //         {
    //             currentDialogueLines[0] = ("Reaper, glitched, line 3");
    //             
    //         }
    //     }
    //     else{
    //         if(lastKilledBy == 1)
    //         {
    //             if(deathsToReaper == 1)
    //             {
    //                 int tempDecider = randomSelector(1, 3);
    //                 if(tempDecider == 1)
    //                 {
    //                     currentDialogueLines[0] = ("Reaper, Not glitched, last killed by Reaper, 1 death, line 1");
    //                     
    //                 }
    //                 else
    //                 {
    //                     currentDialogueLines[0] = ("Reaper, Not glitched, last killed by Reaper, 1 death, line 2");
    //                     
    //                 }

    //             }

    //             else if(deathsToReaper == 2)
    //             {
    //                 int tempDecider = randomSelector(1, 3);
    //                 if(tempDecider == 1)
    //                 {
    //                     currentDialogueLines[0] = ("Reaper, Not glitched, last killed by Reaper, 2 deaths, line 1");
    //                     
    //                 }
    //                 else
    //                 {
    //                     currentDialogueLines[0] = ("Reaper, Not glitched, last killed by Reaper, 2 deaths, line 2");
    //                     
    //                 }
    //             }
    //             //more than 2 deaths to Reaper
    //             else
    //             {
    //                 int tempDecider = randomSelector(1, 4);
    //                 if(tempDecider == 1)
    //                 {
    //                     currentDialogueLines[0] = ("Reaper, Not glitched, last killed by Reaper, > 2 deaths, line 1");
    //                     
    //                 }
    //                 else if(tempDecider == 2)
    //                 {
    //                     currentDialogueLines[0] = ("Reaper, Not glitched, last killed by Reaper, > 2 deaths, line 2");
    //                     
    //                 }
    //                 else
    //                 {
    //                     currentDialogueLines[0] = ("Reaper, Not glitched, last killed by Reaper, > 2 deaths, line 3");
    //                     
    //                 }
    //             }
    //         }
    //         else
    //         {
    //             if(deathsToReaper == 0)
    //             {
    //                 int tempDecider = randomSelector(1, 4);
    //                 if(tempDecider == 1)
    //                 {
    //                     currentDialogueLines[0] = ("Reaper, Not glitched, last killed by Other, 0 deaths, line 1");
    //                     
    //                 }
    //                 else if(tempDecider == 2)
    //                 {
    //                     currentDialogueLines[0] = ("Reaper, Not glitched, last killed by Other, 0 deaths, line 2");
    //                     
    //                 }
    //                 else
    //                 {
    //                     currentDialogueLines[0] = ("Reaper, Not glitched, last killed by Other, 0 deaths, line 3");
    //                     
    //                 }

                    
    //             }
    //             else if(deathsToReaper == 1)
    //             {
    //                 int tempDecider = randomSelector(1, 3);
    //                 if(tempDecider == 1)
    //                 {
    //                     currentDialogueLines[0] = ("Reaper, Not glitched, last killed by Other, 1 death, line 1");
    //                 }
    //                 else
    //                 {
    //                     currentDialogueLines[0] = ("Reaper, Not glitched, last killed by Other, 1 death, line 2");
    //                 }

    //             }

    //             else if(deathsToReaper == 2)
    //             {
    //                 int tempDecider = randomSelector(1, 3);
    //                 if(tempDecider == 1)
    //                 {
    //                     currentDialogueLines[0] = ("Reaper, Not glitched, last killed by Other, 2 deaths, line 1");
    //                 }
    //                 else
    //                 {
    //                     currentDialogueLines[0] = ("Reaper, Not glitched, last killed by Other, 2 deaths, line 2");
    //                 }
    //             }
    //             //more than 2 deaths to Reaper
    //             else
    //             {
    //                 int tempDecider = randomSelector(1, 4);
    //                 if(tempDecider == 1)
    //                 {
    //                     currentDialogueLines[0] = ("Reaper, Not glitched, last killed by Other, > 2 deaths, line 1");
    //                 }
    //                 else if(tempDecider == 2)
    //                 {
    //                     currentDialogueLines[0] = ("Reaper, Not glitched, last killed by Other, > 2 deaths, line 2");
    //                 }
    //                 else
    //                 {
    //                     currentDialogueLines[0] = ("Reaper, Not glitched, last killed by Other, > 2 deaths, line 3");
    //                 }
    //             }
    //         }
            
    //     }
        
    //     displayText();
    // }

    // public void meetArcher()
    // {
    //     if(isGlitched)
    //     {
    //         int tempDecider = randomSelector(1, 4);
    //         if(tempDecider == 1)
    //         {
    //             currentDialogueLines[0] = ("Archer, glitched, line 1");
            
    //         }
    //         else if(tempDecider == 2)
    //         {
    //             currentDialogueLines[0] = ("Archer, glitched, line 2");
    //         }
    //         else
    //         {
    //             currentDialogueLines[0] = ("Archer, glitched, line 3");
    //         }
    //     }
    //     else{
    //         if(lastKilledBy == 1)
    //         {
    //             if(deathsToArcher == 1)
    //             {
    //                 int tempDecider = randomSelector(1, 3);
    //                 if(tempDecider == 1)
    //                 {
    //                     currentDialogueLines[0] = ("Archer, Not glitched, last killed by Archer, 1 death, line 1");
    //                 }
    //                 else
    //                 {
    //                     currentDialogueLines[0] = ("Archer, Not glitched, last killed by Archer, 1 death, line 2");
    //                 }

    //             }

    //             else if(deathsToArcher == 2)
    //             {
    //                 int tempDecider = randomSelector(1, 3);
    //                 if(tempDecider == 1)
    //                 {
    //                     currentDialogueLines[0] = ("Archer, Not glitched, last killed by Archer, 2 deaths, line 1");
    //                 }
    //                 else
    //                 {
    //                     currentDialogueLines[0] = ("Archer, Not glitched, last killed by Archer, 2 deaths, line 2");
    //                 }
    //             }
    //             //more than 2 deaths to Archer
    //             else
    //             {
    //                 int tempDecider = randomSelector(1, 4);
    //                 if(tempDecider == 1)
    //                 {
    //                     currentDialogueLines[0] = ("Archer, Not glitched, last killed by Archer, > 2 deaths, line 1");
    //                 }
    //                 else if(tempDecider == 2)
    //                 {
    //                     currentDialogueLines[0] = ("Archer, Not glitched, last killed by Archer, > 2 deaths, line 2");
    //                 }
    //                 else
    //                 {
    //                     currentDialogueLines[0] = ("Archer, Not glitched, last killed by Archer, > 2 deaths, line 3");
    //                 }
    //             }
    //         }
    //         else
    //         {
    //             if(deathsToArcher == 0)
    //             {
    //                 int tempDecider = randomSelector(1, 4);
    //                 if(tempDecider == 1)
    //                 {
    //                     currentDialogueLines[0] = ("Archer, Not glitched, last killed by Other, 0 deaths, line 1");
    //                 }
    //                 else if(tempDecider == 2)
    //                 {
    //                     currentDialogueLines[0] = ("Archer, Not glitched, last killed by Other, 0 deaths, line 2");
    //                 }
    //                 else
    //                 {
    //                     currentDialogueLines[0] = ("Archer, Not glitched, last killed by Other, 0 deaths, line 3");
    //                 }

                    
    //             }
    //             else if(deathsToArcher == 1)
    //             {
    //                 int tempDecider = randomSelector(1, 3);
    //                 if(tempDecider == 1)
    //                 {
    //                     currentDialogueLines[0] = ("Archer, Not glitched, last killed by Other, 1 death, line 1");
    //                 }
    //                 else
    //                 {
    //                     currentDialogueLines[0] = ("Archer, Not glitched, last killed by Other, 1 death, line 2");
    //                 }

    //             }

    //             else if(deathsToArcher == 2)
    //             {
    //                 int tempDecider = randomSelector(1, 3);
    //                 if(tempDecider == 1)
    //                 {
    //                     currentDialogueLines[0] = ("Archer, Not glitched, last killed by Other, 2 deaths, line 1");
    //                 }
    //                 else
    //                 {
    //                     currentDialogueLines[0] = ("Archer, Not glitched, last killed by Other, 2 deaths, line 2");
    //                 }
    //             }
    //             //more than 2 deaths to Archer
    //             else
    //             {
    //                 int tempDecider = randomSelector(1, 4);
    //                 if(tempDecider == 1)
    //                 {
    //                     currentDialogueLines[0] = ("Not glitched, last killed by Other, > 2 deaths, line 1");
    //                 }
    //                 else if(tempDecider == 2)
    //                 {
    //                     currentDialogueLines[0] = ("Archer, Not glitched, last killed by Other, > 2 deaths, line 2");
    //                 }
    //                 else
    //                 {
    //                     currentDialogueLines[0] = ("Archer, Not glitched, last killed by Other, > 2 deaths, line 3");
    //                 }
    //             }
    //         }
            
    //     }

    //     displayText();
        
    // }

    // public void meetMage()
    // {
    //     if(isGlitched)
    //     {
    //         int tempDecider = randomSelector(1, 4);
    //         if(tempDecider == 1)
    //         {
    //             currentDialogueLines[0] = ("Mage, glitched, line 1");
            
    //         }
    //         else if(tempDecider == 2)
    //         {
    //             currentDialogueLines[0] = ("Mage, glitched, line 2");
    //         }
    //         else
    //         {
    //             currentDialogueLines[0] = ("Mage, glitched, line 3");
    //         }
    //     }
    //     else{
    //         if(lastKilledBy == 1)
    //         {
    //             if(deathsToMage == 1)
    //             {
    //                 int tempDecider = randomSelector(1, 3);
    //                 if(tempDecider == 1)
    //                 {
    //                     currentDialogueLines[0] = ("Mage, Not glitched, last killed by Mage, 1 death, line 1");
    //                 }
    //                 else
    //                 {
    //                     currentDialogueLines[0] = ("Mage, Not glitched, last killed by Mage, 1 death, line 2");
    //                 }

    //             }

    //             else if(deathsToMage == 2)
    //             {
    //                 int tempDecider = randomSelector(1, 3);
    //                 if(tempDecider == 1)
    //                 {
    //                     currentDialogueLines[0] = ("Mage, Not glitched, last killed by Mage, 2 deaths, line 1");
    //                 }
    //                 else
    //                 {
    //                     currentDialogueLines[0] = ("Mage, Not glitched, last killed by Mage, 2 deaths, line 2");
    //                 }
    //             }
    //             //more than 2 deaths to Mage
    //             else
    //             {
    //                 int tempDecider = randomSelector(1, 4);
    //                 if(tempDecider == 1)
    //                 {
    //                     currentDialogueLines[0] = ("Mage, Not glitched, last killed by Mage, > 2 deaths, line 1");
    //                 }
    //                 else if(tempDecider == 2)
    //                 {
    //                     currentDialogueLines[0] = ("Mage, Not glitched, last killed by Mage, > 2 deaths, line 2");
    //                 }
    //                 else
    //                 {
    //                     currentDialogueLines[0] = ("Mage, Not glitched, last killed by Mage, > 2 deaths, line 3");
    //                 }
    //             }
    //         }
    //         else
    //         {
    //             if(deathsToMage == 0)
    //             {
    //                 int tempDecider = randomSelector(1, 4);
    //                 if(tempDecider == 1)
    //                 {
    //                     currentDialogueLines[0] = ("Mage, Not glitched, last killed by Other, 0 deaths, line 1");
    //                 }
    //                 else if(tempDecider == 2)
    //                 {
    //                     currentDialogueLines[0] = ("Mage, Not glitched, last killed by Other, 0 deaths, line 2");
    //                 }
    //                 else
    //                 {
    //                     currentDialogueLines[0] = ("Mage, Not glitched, last killed by Other, 0 deaths, line 3");
    //                 }

                    
    //             }
    //             else if(deathsToMage == 1)
    //             {
    //                 int tempDecider = randomSelector(1, 3);
    //                 if(tempDecider == 1)
    //                 {
    //                     currentDialogueLines[0] = ("Mage, Not glitched, last killed by Other, 1 death, line 1");
    //                 }
    //                 else
    //                 {
    //                     currentDialogueLines[0] = ("Mage, Not glitched, last killed by Other, 1 death, line 2");
    //                 }

    //             }

    //             else if(deathsToMage == 2)
    //             {
    //                 int tempDecider = randomSelector(1, 3);
    //                 if(tempDecider == 1)
    //                 {
    //                     currentDialogueLines[0] = ("Mage, Not glitched, last killed by Other, 2 deaths, line 1");
    //                 }
    //                 else
    //                 {
    //                     currentDialogueLines[0] = ("Mage, Not glitched, last killed by Other, 2 deaths, line 2");
    //                 }
    //             }
    //             //more than 2 deaths to Mage
    //             else
    //             {
    //                 int tempDecider = randomSelector(1, 4);
    //                 if(tempDecider == 1)
    //                 {
    //                     currentDialogueLines[0] = ("Not glitched, last killed by Other, > 2 deaths, line 1");
    //                 }
    //                 else if(tempDecider == 2)
    //                 {
    //                     currentDialogueLines[0] = ("Mage, Not glitched, last killed by Other, > 2 deaths, line 2");
    //                 }
    //                 else
    //                 {
    //                     currentDialogueLines[0] = ("Mage, Not glitched, last killed by Other, > 2 deaths, line 3");
    //                 }
    //             }
    //         }
            
    //     }

    //     displayText();

    // }
    
    // public void meetGameMaster()
    // {
    //     if(isGlitched)
    //     {
    //         int tempDecider = randomSelector(1, 4);
    //         if(tempDecider == 1)
    //         {
    //             currentDialogueLines[0] = ("GameMaster, glitched, line 1");
            
    //         }
    //         else if(tempDecider == 2)
    //         {
    //             currentDialogueLines[0] = ("GameMaster, glitched, line 2");
    //         }
    //         else
    //         {
    //             currentDialogueLines[0] = ("GameMaster, glitched, line 3");
    //         }
    //     }
    //     else{
    //         if(lastKilledBy == 1)
    //         {
    //             if(deathsToGameMaster == 1)
    //             {
    //                 int tempDecider = randomSelector(1, 3);
    //                 if(tempDecider == 1)
    //                 {
    //                     currentDialogueLines[0] = ("GameMaster, Not glitched, last killed by GameMaster, 1 death, line 1");
    //                 }
    //                 else
    //                 {
    //                     currentDialogueLines[0] = ("GameMaster, Not glitched, last killed by GameMaster, 1 death, line 2");
    //                 }

    //             }

    //             else if(deathsToGameMaster == 2)
    //             {
    //                 int tempDecider = randomSelector(1, 3);
    //                 if(tempDecider == 1)
    //                 {
    //                     currentDialogueLines[0] = ("GameMaster, Not glitched, last killed by GameMaster, 2 deaths, line 1");
    //                 }
    //                 else
    //                 {
    //                     currentDialogueLines[0] = ("GameMaster, Not glitched, last killed by GameMaster, 2 deaths, line 2");
    //                 }
    //             }
    //             //more than 2 deaths to GameMaster
    //             else
    //             {
    //                 int tempDecider = randomSelector(1, 4);
    //                 if(tempDecider == 1)
    //                 {
    //                     currentDialogueLines[0] = ("GameMaster, Not glitched, last killed by GameMaster, > 2 deaths, line 1");
    //                 }
    //                 else if(tempDecider == 2)
    //                 {
    //                     currentDialogueLines[0] = ("GameMaster, Not glitched, last killed by GameMaster, > 2 deaths, line 2");
    //                 }
    //                 else
    //                 {
    //                     currentDialogueLines[0] = ("GameMaster, Not glitched, last killed by GameMaster, > 2 deaths, line 3");
    //                 }
    //             }
    //         }
    //         else
    //         {
    //             if(deathsToGameMaster == 0)
    //             {
    //                 int tempDecider = randomSelector(1, 4);
    //                 if(tempDecider == 1)
    //                 {
    //                     currentDialogueLines[0] = ("GameMaster, Not glitched, last killed by Other, 0 deaths, line 1");
    //                 }
    //                 else if(tempDecider == 2)
    //                 {
    //                     currentDialogueLines[0] = ("GameMaster, Not glitched, last killed by Other, 0 deaths, line 2");
    //                 }
    //                 else
    //                 {
    //                     currentDialogueLines[0] = ("GameMaster, Not glitched, last killed by Other, 0 deaths, line 3");
    //                 }

                    
    //             }
    //             else if(deathsToGameMaster == 1)
    //             {
    //                 int tempDecider = randomSelector(1, 3);
    //                 if(tempDecider == 1)
    //                 {
    //                     currentDialogueLines[0] = ("GameMaster, Not glitched, last killed by Other, 1 death, line 1");
    //                 }
    //                 else
    //                 {
    //                     currentDialogueLines[0] = ("GameMaster, Not glitched, last killed by Other, 1 death, line 2");
    //                 }

    //             }

    //             else if(deathsToGameMaster == 2)
    //             {
    //                 int tempDecider = randomSelector(1, 3);
    //                 if(tempDecider == 1)
    //                 {
    //                     currentDialogueLines[0] = ("GameMaster, Not glitched, last killed by Other, 2 deaths, line 1");
    //                 }
    //                 else
    //                 {
    //                     currentDialogueLines[0] = ("GameMaster, Not glitched, last killed by Other, 2 deaths, line 2");
    //                 }
    //             }
    //             //more than 2 deaths to GameMaster
    //             else
    //             {
    //                 int tempDecider = randomSelector(1, 4);
    //                 if(tempDecider == 1)
    //                 {
    //                     currentDialogueLines[0] = ("Not glitched, last killed by Other, > 2 deaths, line 1");
    //                 }
    //                 else if(tempDecider == 2)
    //                 {
    //                     currentDialogueLines[0] = ("GameMaster, Not glitched, last killed by Other, > 2 deaths, line 2");
    //                 }
    //                 else
    //                 {
    //                     currentDialogueLines[0] = ("GameMaster, Not glitched, last killed by Other, > 2 deaths, line 3");
    //                 }
    //             }
    //         }
            
    //     }

    //     displayText();
        
    // }


    public void displayText()
    {
        string[] copy = new string[currentDialogueIndex+1];
        for(int i = 0; i <= currentDialogueIndex; i++)
        {
            //dialogueTrigger.dialogue.sentences[i] = currentDialogueLines[i];
            copy[i] = currentDialogueLines[i];
            //dialogue.sentences[i] = currentDialogueLines[i];
        }
        //dialogueTrigger.TriggerDialogue();
        dialogue.sentences = copy;
        dialogueTrigger.setDialogue(dialogue.sentences);
    }
    // public void displayText()
    // {
    //     currentDialogueLines = resize();
    //     //Dialogue[] copy = new Dialogue[currentDialogueIndex];
    //     for(int i = 0; i < currentDialogueLines.Length; i++)
    //     {
    //         dialogueTrigger.dialogue.sentences[i] = currentDialogueLines[i];
    //       //  copy[i] = currentDialogueLines[i];
    //     }
    //     dialogueTrigger.TriggerDialogue();
    //     //dialogueTrigger.setDialogue(copy);
    // }
    // private string[] resize()
    // {
    //     string[] temp = new string[currentDialogueIndex-1];

    //     for(int i = 0; i < temp.Length; i++)
    //     {
    //         temp[i] = currentDialogueLines[i];
    //     }
    //     return temp;
    // }


//////////////////////////////////////////////////////////////////////////////////////////////////



//     public void displayText()
//     {
//         stopDialogue();
//         dActive = StartCoroutine(dialogueActive);

//     }

//     public void stopDialogue()
//     {
//         if(isDialogueActive)
//         {
//             StopCoroutine (dialogueActive);
//         }
//         dialogueActive = null;
//     }

//     public bool isDialogueActive    {get { return dialogueActive != null;}}
//     public bool waitingForUserInput = false;

//     Coroutine dialogueActive = null;

//     IEnumerator DialogueActive()
//     {
//         narrativeCanvas.SetActive(true);
//         dialogueText1.text = "";
//         string thisLine = currentDialogueLines[i];
//         waitingForUserInput = false;

//         //while(dialogueText1.text != speech)
//         while(dialogueText1.text != thisLine)
//         {
//             dialogueText1.text += thisLine[dialogueText1.text.Length-1];
//             yield return new WaitForEndOfFrame();
//         }

//         //text finished displaying
//         waitingForUserInput = true;
//         while(waitingForUserInput)
//         {
//             yield return new WaitForEndOfFrame();
//         }

//         stopDialogue();
//     }

//     // Update is called once per frame
//     void Update()
//     {
        
//     }

//     // public void talk()
//     // {
//     //     condition1 += 1;

//     //     // main text

//     //     if(condition1 == 2)
//     //     {
//     //         dialogueText1.text = "text1Here";

//     //     }

//     //     else if(conition1 == 3)
//     //     {
//     //         dialogueText1.text = "text2Here";
//     //     }
//     // }




    public int randomSelector(int a, int b)
    {
        return Random.Range(a, b);
    }
}
