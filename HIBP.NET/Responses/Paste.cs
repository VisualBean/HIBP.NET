namespace HIBP.Responses
{
    using System;
    using System.Text;

    /// <summary>
    /// The 
    /// </summary>
    public class Paste
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Paste"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="title">The title.</param>
        /// <param name="date">The date.</param>
        /// <param name="emailCount">The email count.</param>
        /// <param name="source">The source.</param>
        public Paste(string id, string title, DateTime? date, int emailCount, string source)
        {
            this.Id = id;
            this.Title = title;
            this.Date = date;
            this.EmailCount = emailCount;
            this.Source = source;
        }

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public string Id { get; }

        /// <summary>
        /// Gets the title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        public string Title { get; }

        /// <summary>
        /// Gets the date.
        /// </summary>
        /// <value>
        /// The date.
        /// </value>
        public DateTime? Date { get; }

        /// <summary>
        /// Gets the email count.
        /// </summary>
        /// <value>
        /// The email count.
        /// </value>
        public int EmailCount { get; }

        /// <summary>
        /// Gets the source.
        /// </summary>
        /// <value>
        /// The source.
        /// </value>
        public string Source { get; }
    }
}