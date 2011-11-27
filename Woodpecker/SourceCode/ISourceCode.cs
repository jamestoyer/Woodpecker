namespace Toyertechnologies.Woodpecker.SourceCode
{
   /// <summary>
   /// Interface to represent manipulating source code 
   /// </summary>
   public interface ISourceCode
   {
       /// <summary>
       /// Create a new local copy of a projects source code
       /// </summary>
       /// <param name="inputParameters">Parameters required to create a new copy of the source code</param>
       /// <returns>Returns a Result containing details of the source code request outcome</returns>
       Result Clone(Parameters inputParameters);

       /// <summary>
       /// Get the latest changes for a projects source code
       /// </summary>
       /// <param name="inputParameters">Parameters required to retrieving the latest changes for the source code</param>
       /// <returns>Returns a Result containing details of the source code request outcome</returns>
       Result Pull(Parameters inputParameters);

       /// <summary>
       /// Push local source code to a remote location
       /// </summary>
       /// <param name="inputParameters">Parameters required to pushing the latest changes for the source code</param>
       /// <returns>Returns a Result containing details of the source code request outcome</returns>
       Result Push(Parameters inputParameters);
   }
}
