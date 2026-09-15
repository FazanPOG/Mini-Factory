using System.Collections.Generic;

namespace Analytics
{
    public class TestAnalyticProvider : IAnalyticProvider
    {
        private const string PARAMETERS_SEPARATOR = " ";

        private readonly List<string> _events = new List<string>();
        private readonly Dictionary<string, int> _eventCounts = new Dictionary<string, int>();

        public IReadOnlyList<string> Events => _events;
        public int EventsCount => _events.Count;

        public void GameStarted() => Track("GameStarted");

        public void MachineUnlocked(int machineId) => Track("MachineUnlocked", $"machineId:{machineId}");

        public void MachineUpgrade(int machineId, int level) => Track("MachineUpgrade", $"machineId:{machineId} level:{level}");

        public void BoostStarted(int machineId, float durationInSeconds) => Track("BoostStarted", $"machineId:{machineId} durationInSeconds:{durationInSeconds}");

        public void BoostFinished(int machineId) => Track("BoostFinished", $"machineId:{machineId}");

        public void OfflineIncomeApplied(int offlineTimeInSeconds, int income) => Track("OfflineIncomeApplied", $"offlineTimeInSeconds:{offlineTimeInSeconds} income:{income}");

        public void PurchaseSucceed(string productId) => Track("PurchaseSucceed", $"productId:{productId}");

        public void PurchaseFailed(string productId, string reason) => Track("PurchaseFailed", $"productId:{productId} reason:{reason}");

        public void Clear()
        {
            _events.Clear();
            _eventCounts.Clear();
        }

        public bool Contains(string eventName) => _eventCounts.ContainsKey(eventName);

        public int GetCount(string eventName) => _eventCounts.TryGetValue(eventName, out int count) ? count : 0;

        public string GetLastEvent() => _events.Count > 0 ? _events[_events.Count - 1] : null;

        public string GetLastEvent(string eventName)
        {
            for (int i = _events.Count - 1; i >= 0; i--)
            {
                if (IsEvent(_events[i], eventName))
                    return _events[i];
            }

            return null;
        }

        public List<string> GetEvents(string eventName)
        {
            var events = new List<string>();

            foreach (string trackedEvent in _events)
            {
                if (IsEvent(trackedEvent, eventName))
                    events.Add(trackedEvent);
            }

            return events;
        }

        private void Track(string eventName, string parameters = null)
        {
            _events.Add(string.IsNullOrEmpty(parameters) ? eventName : $"{eventName}{PARAMETERS_SEPARATOR}{parameters}");
            _eventCounts[eventName] = GetCount(eventName) + 1;
        }

        private static bool IsEvent(string trackedEvent, string eventName) =>
            trackedEvent.Equals(eventName) || trackedEvent.StartsWith($"{eventName}{PARAMETERS_SEPARATOR}");
    }
}