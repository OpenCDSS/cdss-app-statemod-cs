using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics;

using DWR.StateMod;
using RTi.Util.IO;

namespace cdss.statemod.app 
{

    using Message = RTi.Util.Message.Message;

    public class StateModMain
    {
        /*
        * Program name and version
        */
        public static String PROGRAM_NAME = "StateMod (Java)";
        public static String PROGRAM_VERSION = "0.1.0 (2019-03-14)";

        /*
         * StateMod response file, which is the starting point for reading a StateMod dataset.
         */
        private static String responseFile = null;

        /**
         * StateMod dataset object that manages all the data files in a dataset.
         */
        private static StateMod_DataSet dataset = null;

        /**
         * Run mode.
         */
        //private static StateModRunModeType runMode = null;
        private static StateModRunModeType runMode;


        /**
         * Working directory, which is the location of the response file.
         * There is no reason to run StateMod very far if a response file is not given.
         */
        private static String workingDir = null;

        public static void Main(string[] args)
        {

            string routine = "StateModMain.Main";

            try
            {
                // Set program name and version
                IOUtil.setProgramData(PROGRAM_NAME, PROGRAM_VERSION, args);

                // Set the initial working directory based on users's starting location
                // - this is used to determine the absolute path for the response file
                setWorkingDirInitial();

                // Determine the response file and working directory so that the log file can be opened.
                // - do this by parsing the command line arguments and detecting response file
                try
                {
                    bool initialChecks = true;
                    parseArgs(args, initialChecks);
                }
                catch (Exception e2)
                {
                    Message.printWarning(1, routine, "Error parsing command line.  Exiting.");
                    Message.printWarning(3, routine, e2);
                    quitProgram(1);
                }

                // If a response file was not specified, print the usage and exit
                if (responseFile == null)
                {
                    Console.WriteLine("");
                    Console.WriteLine("No response file was specified.");
                    Console.WriteLine("");
                    printUsage();
                    quitProgram(1);
                }

                // If a response file was specified but does not exist, print an error and exit
                //File f = new File(responseFile);
                if (!File.Exists(responseFile))
                {
                    Console.WriteLine("");
                    Console.WriteLine("Response file \"" + responseFile + "\" does not exist.");
                    Console.WriteLine("");
                    printUsage();
                    quitProgram(1);
                }

                // Initialize logging
                initializeLogging();

                // Now parse the command line arguments
                // - the response file is determined first so that the working directory is determined
                // - and then other actions are taken
                try
                {
                    bool initialChecks = false;
                    parseArgs(args, initialChecks);
                }
                catch (Exception e2)
                {
                    Message.printWarning(1, routine, "Error parsing command line.  Exiting.");
                    Message.printWarning(3, routine, e2);
                    quitProgram(1);
                }

                // If no run mode was requested, print an error
                if (runMode == null)
                {
                    Console.WriteLine("");
                    Console.WriteLine("No run mode was specified.");
                    Console.WriteLine("");
                    printUsage();
                    quitProgram(1);
                }
                else
                {
                    Console.WriteLine("Run mode is " + runMode);
                }

                // Error indicator
                bool error = false;

                // Open the dataset by reading the response file.
                // - try reading dataset files
                try
                {
                    bool readData = true; // Read the data files (except for time series)
                    bool readTimeSeries = true; // Read the time series files
                    bool useGUI = false; // No UI is defined
                    //JFrame parent = null; // A JFrame if UI is used, not implemented here
                    dataset = new StateMod_DataSet();
                    //TODO @jurentie 03/25/2019 - removed parent
                    dataset.readStateModFile(responseFile, readData, readTimeSeries, useGUI);//, parent);
                    Message.printStatus(1, routine, dataset.ToString());
                }
                catch (Exception e2)
                {
                    Message.printWarning(1, routine, "Error reading response file.  See the log file.");
                    Message.printWarning(3, routine, e2);
                }

                if (!error)
                {
                    // Run StateMod for the requested run mode, consistent with the original software but
                    // - will enhance this for additional command line options
                    try
                    {
                        //RunStateMod(dataset, runMode);
                    }
                    catch (Exception e2)
                    {
                        Message.printWarning(1, routine, "Error running StateMod.  See the log file.");
                        Message.printWarning(3, routine, e2);
                    }
                }
            }
            catch
            {
                quitProgram(1);
            }

            Console.WriteLine("Hit any key to exit console");
            Console.ReadKey();

        }

        /*
	     * Initialize logging. This uses the Message class, but SL4J should be implemented.
	     * - The workingDir should have been set from previous logic.
	     */
        private static void initializeLogging()
        {
            //Message.setDebugLevel(Message.TERM_OUTPUT, 0);
            //Message.setDebugLevel(Message.LOG_OUTPUT, 0);
            //Message.setStatusLevel(Message.TERM_OUTPUT, 2);
            //Message.setStatusLevel(Message.LOG_OUTPUT, 2);
            //Message.setWarningLevel(Message.TERM_OUTPUT, 2);
            //Message.setWarningLevel(Message.LOG_OUTPUT, 3);

            // Indicate that message levels should be shown in messages, to allow
            // for a filter when displaying messages...

            //Message.setPropValue("ShowMessageLevel=true");
            //Message.setPropValue("ShowMessageTag=true");

            // Open the log file as the name of the response file with ".log".
            if (responseFile != null)
            {
                string logFile = responseFile + ".sim.log";

                // Set LogFile for message
                Message.openLogFile(logFile);

                //try
                //{
                //    //Message.openLogFile(logFile);
                //}
                //catch (Exception e)
                //{ 
                //    //String nl = System.getProperty("line.separator");
                //    Console.WriteLine("\nUnable to open log file \"" + logFile + "\"" + "\n");
                //}
            }
        }

        /**
	    * Parse command line arguments.
	    * @param args command line arguments
	    * @param initialChecks if true, only parse arguments relevant to initialization.
	    * If false, parse arguments relevant to dataset and simulation.
	    */
        public static void parseArgs(string[] args, Boolean initialChecks)
        {
            // Loop through the arguments twice
            // - the first pass is concerned only with determining the response file and working directory
            //   so that the log file can be opened, and some trivial actions like printing usage and version
            // - the second pass processes all the other arguments
            int ipassToCheck = 0;
            if (initialChecks)
            {
                // Only check command line arguments that result in immediate action (usage, version) and
                // determine the response file and working directory
                ipassToCheck = 0;
            }
            else
            {
                // Else pass all other command line arguments
                // - currently nothing defined but when enabled will be able to set on the dataset object
                ipassToCheck = 1;
            }
            for (int ipass = 0; ipass < 2; ipass++)
            {
                for (int i = 0; i < args.Length; i++)
                {
                    if (args[i].Equals("-baseflows", StringComparison.InvariantCultureIgnoreCase) ||
                        args[i].Equals("--baseflows", StringComparison.InvariantCultureIgnoreCase))
                    {
                        //TODO @jurentie 03/26/2019 - Need to figure out how to use enumeration classes for SateModRunMode
                        runMode = StateModRunModeType.BASEFLOWS;
                    }
                    else if (args[i].Equals("-check", StringComparison.InvariantCultureIgnoreCase) ||
                        args[i].Equals("--check", StringComparison.InvariantCultureIgnoreCase))
                    {
                        //TODO @jurentie 03/26/2019 - Need to figure out how to use enumeration classes for SateModRunMode
                        runMode = StateModRunModeType.CHECK;
                    }
                    else if ((ipass == ipassToCheck) &&
                        (args[i].Equals("-h", StringComparison.InvariantCultureIgnoreCase) ||
                        args[i].Equals("--help", StringComparison.InvariantCultureIgnoreCase)))
                    {
                        // Print the version information
                        printUsage();
                        quitProgram(0);
                    }
                    else if ((args[i].Equals("-sim", StringComparison.InvariantCultureIgnoreCase) ||
                        args[i].Equals("--sim", StringComparison.InvariantCultureIgnoreCase)))
                    {
                        //TODO @jurentie 03/26/2019 - Need to figure out how to use enumeration classes for SateModRunMode
                        runMode = StateModRunModeType.SIMULATE;
                    }
                    else if (args[i].Equals ("-v", StringComparison.InvariantCultureIgnoreCase) || 
                        args[i].Equals("--version", StringComparison.InvariantCultureIgnoreCase))
                    {
                        // Print the version information
                        printVersion();
                        quitProgram(0);
                    }
                    else if (args[i].StartsWith("-"))
                    {
                        // Unrecognized option
                        Console.WriteLine("Unrecognized option \"" + args[i] + "\"");
                        printUsage();
                        quitProgram(1);
                    }
                    else if (ipass == ipassToCheck)
                    {
                        // The "response file" that contains a list of all StateMod input files - allow it
                        // to include .rsp or not on command line
                        setResponseFile(args[i]);
                    }
                }
            }
        }

        /*
	    * Print the program usage.  Print the the bare minimum.
	    */
        public static void printUsage()
        {
            //String nl = System.getProperty("line.separator");
            //Console.WriteLine(nl + 
            Console.WriteLine("\n" +
            "statemod-java [options] dataset.rsp" + "\n" + "\n" +
            "dataset.rsp             \"response file\" that provides a list of dataset input files." + "\n" +
            "-baseflow, --baseflow   Run the baseflow mode with standard options." + "\n" +
            "-h, --help              Print program usage" + "\n" +
            "-sim, --sim             Run the simulation with standard options." + "\n" +
            "-v, --version           Print program version." + "\n");
        }

        /*
	    * Print the program version.
	    */
        public static void printVersion()
        {
            //String nl = System.getProperty("line.separator");.
            //Console.WriteLine(nl + PROGRAM_NAME + " version: " + PROGRAM_VERSION + nl + nl +
            Console.WriteLine("\n" + PROGRAM_NAME + " version: " + PROGRAM_VERSION + "\n" + "\n" +
            "StateMod Java is a part of Colorado's Decision Support Systems (CDSS)\n" +
            "Copyright (C) 1997-2019 Colorado Department of Natural Resources\n" +
            "\n" +
            "StateMod Java is free software:  you can redistribute it and/or modify\n" +
            "    it under the terms of the GNU General Public License as published by\n" +
            "    the Free Software Foundation, either version 3 of the License, or\n" +
            "    (at your option) any later version.\n" +
            "\n" +
            "StateMod Java is distributed in the hope that it will be useful,\n" +
            "    but WITHOUT ANY WARRANTY; without even the implied warranty of\n" +
            "    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the\n" +
            "    GNU General Public License for more details.\n" +
            "\n" +
            "You should have received a copy of the GNU General Public License\n" +
            "    along with StateMod Java.  If not, see <https://www.gnu.org/licenses/>.\n");
        }

        /*
	     * Run StateMod for the provided dataset based on the run mode from the command line.
	     * @param datasetToRun the dataset to run
	     * @param runMode the run mode to execute
	     */
        public static void runStateMod(StateMod_DataSet datasetToRun, StateModRunModeType runMode)
        {
            // Create a StateMod simulator
            StateModRunner stateModRunner = new StateModRunner(datasetToRun);
            //TODO @jurentie 03/26/2019 - Need to figure out how to use enumeration classes for SateModRunMode
            /*if (runMode == StateModRunModeType.BASEFLOWS)
            {
                stateModRunner.RunBaseflows();
            }
            else if (runMode == StateModRunModeType.CHECK)
            {
                stateModRunner.RunCheck();
            }
            else if (runMode == StateModRunModeType.SIMULATE)
            {
                stateModRunner.RunSimulation();
            }*/
        }

        /*
	    * Set the response file name.
	    * @param responseFileReq name of the response file.
	    * If an absolute path, use it.  If a relative path, convert to absolute path.
	    * If the file exists, use the path as specified.
	    * If the with ".rsp" exists, use that.
	    * Consequently the final value is full path that matches an existing file.
	    */
        private static void setResponseFile(String responseFileReq)
        {
            string routine = "StateModMain.SetResponseFile";
            // First convert the file to absolute path.
            string message = "Response file (from command line): " + responseFileReq;
            Message.printStatus(2, routine, message);
            string responseFileAbsolute = Path.GetFullPath(responseFileReq);
            message = "Response file (absolute path): " + responseFileAbsolute;
            Message.printStatus(2, routine, message);
            string path = responseFileAbsolute;
            FileInfo f = new FileInfo(responseFileAbsolute);
            if (f.Exists)
            {
                responseFile = responseFileAbsolute;
                // Reset the working directory to that of the response file, in case it changed from above logic
                workingDir = Directory.GetParent(responseFileAbsolute).ToString();
            }
            else
            {
                // Try adding the extension
                string responseFileAbsolute2 = responseFileAbsolute + ".rsp";
                if (f.Exists)
                {
                    message = "Response file (with .rsp appended): " + responseFileAbsolute2;
                    Message.printStatus(2, routine, message);
                    responseFile = responseFileAbsolute2;
                    // Reset the working directory to that of the response file, in case it changed from above logic
                    workingDir = Directory.GetParent(responseFileAbsolute2).ToString();
                }
            }
            // The response file may not exist, in which case it will be set to the initial null value.
            // This will trigger exiting the program from the main program.
        }

        /**
	    Set the working directory as the system "user.dir" property.
	    */
        private static void setWorkingDirInitial()
        {
            String routine = "StateModMain.setWorkingDirInitial";
            // The following DOES NOT have slash at the end of the working directory.
            //String workingDir = System.getProperty("user.dir");
            IOUtil.setProgramWorkingDir(workingDir);
            // Set the dialog because if the running in batch mode and interaction with the graph
            // occurs, this default for dialogs should be the home of the command file.
            //JGUIUtil.setLastFileDialogDirectory( working_dir );
            String message = "Setting working directory to user directory \"" + workingDir + "\".";
            Message.printStatus(1, routine, message);
            Console.WriteLine(message);
        }

        /**
	    Clean up and quit the program.
	    @param status Program exit status.
	    */
        public static void quitProgram(int status)
        {
            String routine = "StateModMain.quitProgram";

            //Message.printStatus(1, routine, "Exiting with status " + status + ".");

            Console.WriteLine("STOP " + status + "\n");
            Message.closeLogFile();
            //System.exit(status);
        }

    }
}
