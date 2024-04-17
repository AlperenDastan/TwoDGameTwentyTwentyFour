using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Linq;

namespace TwoDGameTwentyTwentyFour
{
    // Singleton pattern implementation for a Logger that outputs to an XML file.
    public class Logger
    {
        // Static variable to hold the instance of the logger.
        private static Logger instance;
        // Path where the log file will be saved.
        private string logFilePath;
        // XML element that represents the entire log document.
        private XElement logFile;

        // Private constructor to prevent instantiation from outside and to initialize the log file.
        private Logger()
        {
            logFilePath = "gameLog.xml";
            InitializeLogFile();
        }

        // Public accessor to get the singleton instance of the logger.
        public static Logger Instance
        {
            get
            {
                // If the instance doesn't exist, create it.
                if (instance == null)
                {
                    instance = new Logger();
                }
                return instance;
            }
        }

        // Initializes the XML log file by either loading an existing file or creating a new one.
        private void InitializeLogFile()
        {
            // Check if the log file already exists.
            if (File.Exists(logFilePath))
            {
                // Load the existing XML log file.

                logFile = XElement.Load(logFilePath);
            }
            else
            {
                // Create a new XML file with the root element <Logs>.
                logFile = new XElement("Logs");
                // Save the new XML structure to the file path.
                logFile.Save(logFilePath); // LinQ 
            }
        }

        // Method to log a message to the XML file.
        public void Log(string message)
        {
            // Create an XML element <Log> with the current timestamp and message.
            XElement logEntry = new XElement("Log",
                new XElement("Timestamp", DateTime.Now.ToString("o")),
                new XElement("Message", message)
            );

            // Add the new log entry to the existing XML document.
            logFile.Add(logEntry); // LinQ
            // Save the updated XML document to the disk.
            logFile.Save(logFilePath);

            // Optional: Output the log message to the console for immediate feedback.
            Console.WriteLine($"Logged to XML: {message}");
        }
    }
}