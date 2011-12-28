namespace Toyertechnologies.Woodpecker.Processes
{
    using System;
    using System.Diagnostics;
    using System.Text;

    /// <summary>
    /// Base class to encapsulate the defaults for running a Process
    /// </summary>
    public abstract class ProcessBase
    {
        /// <summary>
        /// The Process created by this instance.
        /// </summary>
        private Process process;

        /// <summary>
        /// The ProcessStartInfo associated with this instance.
        /// </summary>
        private ProcessStartInfo startInfo;

        /// <summary>
        /// The StringBuilder of messages output for the process in the instance.
        /// </summary>
        private StringBuilder messageBuilder;

        /// <summary>
        /// The ConstructProcessDelegate for the process
        /// </summary>
        private ConstructProcessDelegate processConstruction;

        /// <summary>
        /// The ConstructStartInfoDelegate for the process
        /// </summary>
        private ConstructStartInfoDelegate startInfoConstruction;

        /// <summary>
        /// Initializes a new instance of the ProcessBase class.
        /// </summary>
        /// <param name="processLocation">The location of process to be executed</param>
        /// <param name="processArguments">The arguments that will be passed to the process</param>
        /// <param name="workingDirectory">The directory the process will execute within</param>
        public ProcessBase(string processLocation, string processArguments, string workingDirectory)
        {
            this.messageBuilder = new StringBuilder();
            this.process = new Process();
            this.ProcessLocation = processLocation;
            this.ProcessArguments = processArguments;
            this.WorkingDirectory = workingDirectory;
            this.processConstruction = new ConstructProcessDelegate(this.ConstructProcess);
            this.startInfoConstruction = new ConstructStartInfoDelegate(this.ConstructStartInfo);
        }

        /// <summary>
        /// Represents the method that will construct Process instance.
        /// </summary>
        /// <param name="startInformation">The start information the process will be constructed with</param>
        /// <returns>REturns a Process with the default values</returns>
        public delegate Process ConstructProcessDelegate(ProcessStartInfo startInformation);

        /// <summary>
        /// Represents the method that will construct the ProcessStartInfo for the Process instance
        /// </summary>
        /// <param name="processArguments">The arguments that will be passed to the process</param>
        /// <param name="workingDirectory">The directory the process will execute within</param>
        /// <returns>Returns a ProcessStartInfo containing the default start information for a process</returns>
        public delegate ProcessStartInfo ConstructStartInfoDelegate(string processArguments, string workingDirectory);

        /// <summary>
        /// Occurs when a message is output from the process
        /// </summary>
        public event DataReceivedEventHandler CustomMessageHandler;

        /// <summary>
        /// Occurs when the process exits.
        /// </summary>
        public event EventHandler ExitedHandler;

        /// <summary>
        /// Gets the exit code passed from the process
        /// </summary>
        public int ExitCode { get; private set; }

        /// <summary>
        /// Gets the location of process to be executed.
        /// </summary>
        public string ProcessLocation { get; private set; }

        /// <summary>
        /// Gets any arguments that will be passed to the process.
        /// </summary>
        public string ProcessArguments { get; private set; }

        /// <summary>
        /// Gets the directory the process will execute within.
        /// </summary>
        public string WorkingDirectory { get; private set; }

        /// <summary>
        /// Gets any messages returned when executing the process
        /// </summary>
        public string Messages
        {
            get
            {
                return this.messageBuilder.ToString();
            }
        }

        /// <summary>
        /// Overrides the default process creation method
        /// </summary>
        /// <param name="constructProcessMethod">The ConstructProcessDelegate implementation to replace the default with.</param>
        public void OverrideProcessConstruction(ConstructProcessDelegate constructProcessMethod)
        {
            this.processConstruction = constructProcessMethod;
        }

        /// <summary>
        /// Overrides the default start info creation method
        /// </summary>
        /// <param name="constructStartInfoMethod">The ConstructStartInfoDelegate implementation to replace the default with.</param>
        public void OverrideStartInfoConstruction(ConstructStartInfoDelegate constructStartInfoMethod)
        {
            this.startInfoConstruction = constructStartInfoMethod;
        }

        /// <summary>
        /// Run the process
        /// </summary>
        public sealed void Run()
        {
            // Assemble the process
            this.AssembleProcess();

            // Run the process
            process.Start();
            process.BeginErrorReadLine();
            process.BeginOutputReadLine();
            process.WaitForExit();
        }

        /// <summary>
        /// Sets up the process ready to be run. This must be run before running a process.
        /// </summary>
        protected void AssembleProcess()
        {
            this.startInfo = this.startInfoConstruction(this.ProcessArguments, this.WorkingDirectory);
            this.process = this.processConstruction(this.startInfo);
            this.process.ErrorDataReceived += this.MessageHandler;
            this.process.Exited += this.ProcessExited;
            this.process.OutputDataReceived += this.MessageHandler;
        }

        /// <summary>
        /// Default delegate implementation for constructing the process to be run.
        /// </summary>
        /// <param name="startInformation">The start information the process will be constructed with</param>
        /// <returns>REturns a Process with the default values</returns>
        private Process ConstructProcess(ProcessStartInfo startInformation)
        {
            // Run the process
            var newProcess = new Process()
            {
                EnableRaisingEvents = true,
                StartInfo = startInformation,
            };
            return newProcess;
        }

        /// <summary>
        /// Default delegate implementation for constructing the start information for a process
        /// </summary>
        /// <param name="processArguments">The arguments that will be passed to the process</param>
        /// <param name="workingDirectory">The directory the process will execute within</param>
        /// <returns>Returns a ProcessStartInfo containing the default start information for a process</returns>
        private ProcessStartInfo ConstructStartInfo(string processArguments, string workingDirectory)
        {
            // Generate the start process information
            var startInfo = new ProcessStartInfo(this.ProcessLocation, processArguments)
            {
                CreateNoWindow = true,
                ErrorDialog = false,
                LoadUserProfile = true,
                RedirectStandardError = true,
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                StandardErrorEncoding = new UTF8Encoding(),
                StandardOutputEncoding = new UTF8Encoding(),
                UseShellExecute = false,
                WindowStyle = ProcessWindowStyle.Normal,
                WorkingDirectory = workingDirectory,
            };

            return startInfo;
        }

        /// <summary>
        /// Method that handles messages output from the process.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">An EventArgs that contains no event data.</param>
        private void MessageHandler(object sender, DataReceivedEventArgs e)
        {
            this.messageBuilder.Append(e.Data + Environment.NewLine);
            if (this.CustomMessageHandler != null)
            {
                this.CustomMessageHandler(sender, e);
            }
        }

        /// <summary>
        /// Method that handles the process being exited
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">An EventArgs that contains no event data.</param>
        private void ProcessExited(object sender, EventArgs e)
        {
            var process = sender as Process;
            if (process != null)
            {
                process.WaitForExit();
                if (this.ExitedHandler != null)
                {
                    this.ExitedHandler(sender, e);
                }
            }
        }
    }
}
