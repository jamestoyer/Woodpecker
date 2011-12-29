namespace Toyertechnologies.Woodpecker.SourceCode
{
    /// <summary>
    /// An abstract representation of a source code result
    /// </summary>
    public abstract class Result
    {
        /// <summary>
        /// Gets or sets a value indicating whether the success or the source code retrieval
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// Gets or sets the local output location source location
        /// </summary>
        public string OutputLocation { get; set; }

        /// <summary>
        /// Gets or sets the messages output from the source code provider
        /// </summary>
        public string Messages { get; set; }

        /// <summary>
        /// Determines whether this instance and a specified object, which must also be a Result object, have the same value.
        /// </summary>
        /// <param name="obj">The Result to compare to this instance.</param>
        /// <returns>true if obj is a Result and its value is the same as this instance; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            var actual = obj as Result;
            return this.Equals(actual);
        }

        /// <summary>
        /// Determines whether this instance and another specified Result object have the same value.
        /// </summary>
        /// <param name="other">The Result to compare to this instance.</param>
        /// <returns>true if other is a Result and its value is the same as this instance; otherwise, false.</returns>
        public bool Equals(Result other)
        {
            if (other == null)
            {
                return false;
            }
            else
            {
                return
                    this.IsSuccess == other.IsSuccess &&
                    this.OutputLocation == other.OutputLocation &&
                    this.Messages == other.Messages;
            }
        }

        /// <summary>
        /// Returns the hash code for this Result.
        /// </summary>
        /// <returns>A hash code for the current Result</returns>
        public override int GetHashCode()
        {
            return this.OutputLocation.GetHashCode();
        }
    }
}
