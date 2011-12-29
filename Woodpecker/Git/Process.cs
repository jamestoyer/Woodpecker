namespace Toyertechnologies.Woodpecker.Git
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class Process : Processes.ProcessBase
    {
        /// <summary>
        /// Initializes a new instance of the Process class.
        /// </summary>
        /// <param name="processLocation">The location of process to be executed</param>
        /// <param name="processArguments">The arguments that will be passed to the process</param>
        /// <param name="workingDirectory">The directory the process will execute within</param>
        public Process(string processLocation, string processArguments, string workingDirectory)
            : base(processLocation, processArguments, workingDirectory)
        {            
        }
    }
}
