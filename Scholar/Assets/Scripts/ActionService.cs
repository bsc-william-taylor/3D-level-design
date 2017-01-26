using UnityEngine.UI;
using UnityEngine;
using System.Collections;

namespace Assets.Scripts
{
    public class ActionService
    {
        public static string Text
        {
            get { return GameObject.Find("Action").GetComponent<Text>().text; }
            set { GameObject.Find("Action").GetComponent<Text>().text = value; }
        }

        public static void PostAction(MonoBehaviour mono, string description, int wait = 5)
        {
            mono.StartCoroutine(PostAction(description, wait));
        }

        private static IEnumerator PostAction(string description, int wait)
        {
            Text = description;

            if (wait != -1)
            {
                yield return new WaitForSeconds(wait);
                Text = string.Empty;
            }
        }
    }
}
