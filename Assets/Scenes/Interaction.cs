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

        public void AIReplyComplete()
        {
            playerText.interactable = true;
            playerText.Select();
            playerText.text = "";
            Debug.Log("AI reply complete");

            Debug.Log("Sending reply to Roslyn Compiler...");
            // Instantiate RoslynCompilerCode and execute RunCode method
            GameObject roslyn_tmp = Instantiate(Roslyn);
            roslyn_tmp.GetComponent<RoslynCompilerCode>().RunCode(AIText.text);
            Debug.Log("Current script took " + (Time.time - startTime) + " seconds to execute.");
            startTime = 0.0f;
            Debug.Log("Roslyn Compiler run complete.");
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
