namespace Toyertechnologies.Woodpecker.Git
{
    /// <summary>
    /// An abstract representation of a source code result
    /// </summary>
    public class Result : SourceCode.Result
    {
        /// <summary>
        /// Gets or sets the exit code for the Git process
        /// </summary>
        public int ExitCode { get; set; }
        
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
                    this.ExitCode == other.ExitCode &&
                    base.Equals(other);
            }
        }
    }
}
