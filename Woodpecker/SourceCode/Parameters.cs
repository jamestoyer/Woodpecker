namespace Toyertechnologies.Woodpecker.SourceCode
{
   /// <summary>
   /// Abstract class containing common source code parameters
   /// </summary>
   public abstract class Parameters
   {
       /// <summary>
       /// Gets or sets the source location of the source code
       /// </summary>
       public string SourceLocation { get; set; }

       /// <summary>
       /// Gets or sets the destination location of the source code
       /// </summary>
       public string DestinationLocation { get; set; }

       /// <summary>
       /// Determines whether this instance and a specified object, which must also be a Parameters object, have the same value.
       /// </summary>
       /// <param name="obj">The Parameters to compare to this instance.</param>
       /// <returns>true if obj is a Parameters and its value is the same as this instance; otherwise, false.</returns>
       public override bool Equals(object obj)
       {
           var actual = obj as Parameters;
           return this.Equals(actual);
       }

       /// <summary>
       /// Determines whether this instance and another specified Parameters object have the same value.
       /// </summary>
       /// <param name="other">The Parameters to compare to this instance.</param>
       /// <returns>true if other is a Parameters and its value is the same as this instance; otherwise, false.</returns>
       public bool Equals(Parameters other)
       {
           if (other == null)
           {
               return false;
           }
           else
           {
               return
                   this.DestinationLocation == other.DestinationLocation &&
                   this.SourceLocation == other.SourceLocation;
           }
       }

       /// <summary>
       /// Returns the hash code for this SearchResultBase.
       /// </summary>
       /// <returns>A hash code for the current SearchResultBase</returns>
       public override int GetHashCode()
       {
           return string.Join("_", this.SourceLocation, this.DestinationLocation).GetHashCode();
       }
   }
}
