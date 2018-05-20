using System;
using System.Collections.Generic;
using System.Text;

namespace HIBP.Responses
{
    public class Breach
    {
        private readonly string _name;
        private readonly string _title;
        private readonly string _domain;
        private readonly DateTime _breachDate;
        private readonly DateTime _addedDate;
        private readonly DateTime _modifiedDate;
        private readonly int _pwnCount;
        private readonly string _description;
        private readonly List<string> _dataClasses;
        private readonly bool _isVerified;
        private readonly bool _isSensitive;
        private readonly bool _isRetired;
        private readonly bool _isSpamList;

        public Breach(string name, string title, string domain, DateTime breachDate, DateTime addedDate, DateTime modifiedDate, int pwnCount, string description, List<string> dataClasses, bool isVerified, bool isSensitive, bool isRetired, bool isSpamList)
        {
            _name = name;
            _title = title;
            _domain = domain;
            _breachDate = breachDate;
            _addedDate = addedDate;
            _modifiedDate = modifiedDate;
            _pwnCount = pwnCount;
            _description = description;
            _dataClasses = dataClasses;
            _isVerified = isVerified;
            _isSensitive = isSensitive;
            _isRetired = isRetired;
            _isSpamList = isSpamList;
        }

        public string Name => _name;
        public string Title => _title;
        public string Domain => _domain;
        public DateTime BreachDate => _breachDate;
        public DateTime AddedDate => _addedDate;
        public DateTime ModifiedDate => _modifiedDate;
        public int PwnCount => _pwnCount;
        public string Description => _description;
        public List<string> DataClasses => _dataClasses;
        public bool IsVerified => _isVerified;
        public bool IsSensitive => _isSensitive;
        public bool IsRetired => _isRetired;
        public bool IsSpamList => _isSpamList;

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"Name: {Name}");
            sb.AppendLine($"Title: {Title}");
            sb.AppendLine($"Domain: {Domain}");
            sb.AppendLine($"BreachDate: {BreachDate.ToString("o")}");
            sb.AppendLine($"AddedDate: {AddedDate.ToString("o")}");
            sb.AppendLine($"ModifiedDate: {ModifiedDate.ToString("o")}");
            sb.AppendLine($"PwnCount: {PwnCount}");
            sb.AppendLine($"Description: {Description}");
            sb.AppendLine("DataClasses:");
            foreach (var dataClass in DataClasses)
            {
                sb.AppendLine($"    {dataClass}");
            }
            sb.AppendLine($"IsVerified: {IsVerified}");
            sb.AppendLine($"IsSensitive: {IsSensitive}");
            sb.AppendLine($"IsRetired: {IsRetired}");
            sb.AppendLine($"IsSpamList: {IsSpamList}");
            return sb.ToString();
        }
    }
}
