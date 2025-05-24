using System.Collections.Generic;
using Naninovel;
using TMPro;
using UnityEngine;

public class QuestLog : MonoBehaviour
{
    [SerializeField]
    Transform _questLogPanel;

    [SerializeField]
    GameObject _questLogItemPrefab;

    readonly List<string> _questMessages = new()
    {
        "Получено от Скарлет. Что-то происходит в старой башне.",
        "Найти Викторию. Она знает больше.",
        "Пройти испытание Виктории.",
        "Найти амулет в старом святилище.",
        "Вернуться и решить, кому его отдать."
    };

    int _currentQuestIndex = 0;

    public void UpdateQuestLog()
    {
        var vars = Engine.GetService<ICustomVariableManager>();
        var step = vars.GetVariableValue("QuestStep").AsInvariantInt();
        if (step - 1 == _currentQuestIndex && _currentQuestIndex < _questMessages.Count && _questMessages[_currentQuestIndex] != "")
        {
            var message = Instantiate(_questLogItemPrefab, _questLogPanel);
            message.GetComponentInChildren<TextMeshProUGUI>().text = _questMessages[_currentQuestIndex];
            _currentQuestIndex++;
        }
    }
}
