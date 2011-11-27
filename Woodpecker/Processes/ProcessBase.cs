namespace Toyertechnologies.Woodpecker.Processes
{
   using System;
   using System.Diagnostics;
   using System.Text;

   /// <summary>
   /// TODO: Update summary.
   /// </summary>
   public abstract class ProcessBase
   {
       private Process process;
       private ProcessStartInfo startInfo;
       private StringBuilder messageBuilder;

       public ProcessBase(string processLocation, string processArguments, string workingDirectory)
       {
           this.messageBuilder = new StringBuilder();
           this.process = new Process();
           this.ProcessLocation = processLocation;
           this.ProcessArguments = processArguments;
           this.WorkingDirectory = workingDirectory;
           this.ProcessConstruction = new ConstructProcessDelegate(this.ConstructProcess);
           this.StartInfoConstruction = new ConstructStartInfoDelegate(this.ConstructStartInfo);
       }

       public delegate Process ConstructProcessDelegate(ProcessStartInfo startInformation);
       public delegate ProcessStartInfo ConstructStartInfoDelegate(string processArguments, string workingDirectory);
       public event DataReceivedEventHandler CustomMessageHandler;
       public event EventHandler ExitedHandler;

       public int ExitCode { get; set; }
       public string ProcessLocation { get; private set; }
       public string ProcessArguments { get; private set; }
       public string WorkingDirectory { get; private set; }
       public ConstructProcessDelegate ProcessConstruction { get; set; }
       public ConstructStartInfoDelegate StartInfoConstruction { get; set; }
       public string Messages
       {
           get
           {
               return this.messageBuilder.ToString();
           }
       }

       protected void AssembleProcess()
       {
           this.startInfo = this.StartInfoConstruction(this.ProcessArguments, this.WorkingDirectory);
           this.process = this.ProcessConstruction(this.startInfo);
           this.process.ErrorDataReceived += this.MessageHandler;
           this.process.Exited += this.ProcessExited;
           this.process.OutputDataReceived += this.MessageHandler;
       }

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
               
       private void MessageHandler(object sender, DataReceivedEventArgs e)
       {
           this.messageBuilder.Append(e.Data + Environment.NewLine);
           if (this.CustomMessageHandler != null)
           {
               this.CustomMessageHandler(sender, e);
           }
       }
               
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
