using System;
using System.Collections.Generic;
using System.Text;

namespace HIBP.Responses
{
    public class Breach
    {
        public string Name { get; set; }
        public string Title { get; set; }
        public string Domain { get; set; }
        public DateTime BreachDate { get; set; }
        public DateTime AddedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int PwnCount { get; set; }
        public string Description { get; set; }
        public List<string> DataClasses { get; set; }
        public bool IsVerified { get; set; }
        public bool IsSensitive { get; set; }
        public bool IsRetired { get; set; }
        public bool IsSpamList { get; set; }

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
            foreach(var dataClass in DataClasses)
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
