namespace HIBP.Responses
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// The breach class.
    /// </summary>
    public class Breach
    {
        private readonly string name;
        private readonly string title;
        private readonly string domain;
        private readonly DateTime breachDate;
        private readonly DateTime addedDate;
        private readonly DateTime modifiedDate;
        private readonly int pwnCount;
        private readonly string description;
        private readonly List<string> dataClasses;
        private readonly bool isVerified;
        private readonly bool isSensitive;
        private readonly bool isRetired;
        private readonly bool isSpamList;

        /// <summary>
        /// Initializes a new instance of the <see cref="Breach"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="title">The title.</param>
        /// <param name="domain">The domain.</param>
        /// <param name="breachDate">The breach date.</param>
        /// <param name="addedDate">The added date.</param>
        /// <param name="modifiedDate">The modified date.</param>
        /// <param name="pwnCount">The PWN count.</param>
        /// <param name="description">The description.</param>
        /// <param name="dataClasses">The data classes.</param>
        /// <param name="isVerified">if set to <c>true</c> [is verified].</param>
        /// <param name="isSensitive">if set to <c>true</c> [is sensitive].</param>
        /// <param name="isRetired">if set to <c>true</c> [is retired].</param>
        /// <param name="isSpamList">if set to <c>true</c> [is spam list].</param>
        public Breach(string name, string title, string domain, DateTime breachDate, DateTime addedDate, DateTime modifiedDate, int pwnCount, string description, List<string> dataClasses, bool isVerified, bool isSensitive, bool isRetired, bool isSpamList)
        {
            this.name = name;
            this.title = title;
            this.domain = domain;
            this.breachDate = breachDate;
            this.addedDate = addedDate;
            this.modifiedDate = modifiedDate;
            this.pwnCount = pwnCount;
            this.description = description;
            this.dataClasses = dataClasses;
            this.isVerified = isVerified;
            this.isSensitive = isSensitive;
            this.isRetired = isRetired;
            this.isSpamList = isSpamList;
        }

        public string Name => this.name;

        public string Title => this.title;

        public string Domain => this.domain;

        public DateTime BreachDate => this.breachDate;

        public DateTime AddedDate => this.addedDate;

        public DateTime ModifiedDate => this.modifiedDate;

        public int PwnCount => this.pwnCount;

        public string Description => this.description;

        public List<string> DataClasses => this.dataClasses;

        public bool IsVerified => this.isVerified;

        public bool IsSensitive => this.isSensitive;

        public bool IsRetired => this.isRetired;

        public bool IsSpamList => this.isSpamList;

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"Name: {this.Name}");
            sb.AppendLine($"Title: {this.Title}");
            sb.AppendLine($"Domain: {this.Domain}");
            sb.AppendLine($"BreachDate: {this.BreachDate:o}");
            sb.AppendLine($"AddedDate: {this.AddedDate:o}");
            sb.AppendLine($"ModifiedDate: {this.ModifiedDate:o}");
            sb.AppendLine($"PwnCount: {this.PwnCount}");
            sb.AppendLine($"Description: {this.Description}");
            sb.AppendLine("DataClasses:");
            foreach (var dataClass in this.DataClasses)
            {
                sb.AppendLine($"    {dataClass}");
            }

            sb.AppendLine($"IsVerified: {this.IsVerified}");
            sb.AppendLine($"IsSensitive: {this.IsSensitive}");
            sb.AppendLine($"IsRetired: {this.IsRetired}");
            sb.AppendLine($"IsSpamList: {this.IsSpamList}");
            return sb.ToString();
        }
    }
}