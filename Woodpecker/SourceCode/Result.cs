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
   }
}
