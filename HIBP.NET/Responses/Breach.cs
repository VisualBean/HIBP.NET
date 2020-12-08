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
            this.Name = name;
            this.Title = title;
            this.Domain = domain;
            this.BreachDate = breachDate;
            this.AddedDate = addedDate;
            this.ModifiedDate = modifiedDate;
            this.PwnCount = pwnCount;
            this.Description = description;
            this.DataClasses = dataClasses;
            this.IsVerified = isVerified;
            this.IsSensitive = isSensitive;
            this.IsRetired = isRetired;
            this.IsSpamList = isSpamList;
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; }

        /// <summary>
        /// Gets the title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        public string Title { get; }

        /// <summary>
        /// Gets the domain.
        /// </summary>
        /// <value>
        /// The domain.
        /// </value>
        public string Domain { get; }

        /// <summary>
        /// Gets the breach date.
        /// </summary>
        /// <value>
        /// The breach date.
        /// </value>
        public DateTime BreachDate { get; }

        /// <summary>
        /// Gets the added date.
        /// </summary>
        /// <value>
        /// The added date.
        /// </value>
        public DateTime AddedDate { get; }

        /// <summary>
        /// Gets the modified date.
        /// </summary>
        /// <value>
        /// The modified date.
        /// </value>
        public DateTime ModifiedDate { get; }

        /// <summary>
        /// Gets the PWN count.
        /// </summary>
        /// <value>
        /// The PWN count.
        /// </value>
        public int PwnCount { get; }

        /// <summary>
        /// Gets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; }

        /// <summary>
        /// Gets the data classes.
        /// </summary>
        /// <value>
        /// The data classes.
        /// </value>
        public IEnumerable<string> DataClasses { get; }

        /// <summary>
        /// Gets a value indicating whether this instance is verified.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is verified; otherwise, <c>false</c>.
        /// </value>
        public bool IsVerified { get; }

        /// <summary>
        /// Gets a value indicating whether this instance is sensitive.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is sensitive; otherwise, <c>false</c>.
        /// </value>
        public bool IsSensitive { get; }

        /// <summary>
        /// Gets a value indicating whether this instance is retired.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is retired; otherwise, <c>false</c>.
        /// </value>
        public bool IsRetired { get; }

        /// <summary>
        /// Gets a value indicating whether this instance is spam list.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is spam list; otherwise, <c>false</c>.
        /// </value>
        public bool IsSpamList { get; }

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="string" /> that represents this instance.
        /// </returns>
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
                sb.AppendLine($" {dataClass}");
            }

            sb.AppendLine($"IsVerified: {this.IsVerified}");
            sb.AppendLine($"IsSensitive: {this.IsSensitive}");
            sb.AppendLine($"IsRetired: {this.IsRetired}");
            sb.AppendLine($"IsSpamList: {this.IsSpamList}");
            return sb.ToString();
        }
    }
}