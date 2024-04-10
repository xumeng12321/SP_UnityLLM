using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace LLMUnity
{ 
    [CreateAssetMenu(fileName = "LLMMethods", menuName = "LLMMethods", order = 1)]
    public class LLMMethods : ScriptableObject
    {
        
        public string prompt;

        
        public Code[] codes;
    }

    [Serializable]
    public struct Code
    {
        [TextArea(10, 40)]
        public string code;
    }

}
