using UnityEngine;

namespace JigsawPuzzle
{
    public class Logger : MonoBehaviour
    {
        public void PrintLog(string logMessage)
        {
            Debug.Log($"[Logger] ::: {logMessage}");
        }
    }
}
