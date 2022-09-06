// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class DialogueTest : MonoBehaviour
// {

//     DialogueBox dialogue;
//     int index = 0;
//     // Start is called before the first frame update
//     void Start()
//     { 
//         dialogue = DialogueBox.instance;
//     }

//     public string[] s = new string()
//     {
//         "Text1",
//         "Text2",
//         "Text3"
//     };

//     // Update is called once per frame
//     void Update()
//     {
//         if(input.GetKeyDown(KeyCode.space))
//         {
//             if(!dialogue.isDialogueActive || dialogue.waitingForUserInput)
//             {
//                 if(index >= s.length)
//                 {
//                     return;
//                 }
//                 Say(s[index]);
//                 index++;
//             }
//         }
//     }

//     void Say(string s)
//     {
//         //string[] parts = s.Split(' : ');
//         dialogue.displayText();

//     }
// }
