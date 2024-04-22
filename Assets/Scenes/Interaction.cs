using UnityEngine;
using LLMUnity;
using UnityEngine.UI;
using TMPro;
using System.Numerics;

namespace LLMUnitySamples
{
    public class Interaction : MonoBehaviour
    {
        public LLM llm;
        public TMP_InputField playerText;
        public TMP_Text AIText;
        public GameObject Roslyn;
        public Button CompileButton;

        [SerializeField]
        [TextArea(40, 12)]
        private string codeEmbedding;

        [SerializeField]
        private string codeReplacement;

        private float startTime = 0.0f;

        void WarmupCompleted()
        {
            Debug.Log("The AI is warm");
        }
        void Start()
        {
            playerText.onSubmit.AddListener(onInputFieldSubmit);
            playerText.Select();
            _ = llm.Warmup(WarmupCompleted);
        }

        void onInputFieldSubmit(string message)
        {
            playerText.interactable = false;
            AIText.text = "...";
            _ = llm.Chat(message, SetAIText, AIReplyComplete);
            startTime = Time.time;
        }

        public void SetAIText(string text)
        {
            AIText.text = text;
            Debug.Log("AI reply: " + text);
        }

        private string GenerateRoslynCode()
        {
            Debug.Log("Generating Roslyn code...");
            string code = codeEmbedding;

            code = code.Replace(codeReplacement, AIText.text);

            Debug.Log("Code generated: " + code);
            return code;
        }

        public void AIReplyComplete()
        {
            playerText.interactable = true;
            playerText.Select();
            playerText.text = "";
            Debug.Log("AI reply complete");

            string roslynCode = GenerateRoslynCode();
            Debug.Log("Sending reply to Roslyn Compiler...");
            // Instantiate RoslynCompilerCode and execute RunCode method
            GameObject roslyn_tmp = Instantiate(Roslyn);
            roslyn_tmp.GetComponent<RoslynCompilerCode>().RunCode(roslynCode);
            Debug.Log("Current script took " + (Time.time - startTime) + " seconds to execute.");
            
            startTime = 0.0f;
            // Reset timer
            Debug.Log("Roslyn Compiler run complete.");
            //Compile Button to debug
            CompileButton.onClick.AddListener(() => roslyn_tmp.GetComponent<RoslynCompilerCode>().RunCode());
        }   

        public void CancelRequests()
        {
            llm.CancelRequests();
            AIReplyComplete();
        }

        public void ExitGame()
        {
            Debug.Log("Exit button clicked");
            Application.Quit();
        }
    }
}
