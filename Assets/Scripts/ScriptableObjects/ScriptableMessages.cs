using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Messages", menuName = "Messages", order = 1)]
public class ScriptableMessages : ScriptableObject {
    public List<Message> messages = new List<Message>();
}
