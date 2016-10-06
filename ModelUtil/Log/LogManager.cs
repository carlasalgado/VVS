using Microsoft.Practices.EnterpriseLibrary.Logging;
using System;
using System.Diagnostics;



namespace Es.Udc.DotNet.ModelUtil.Log
{
    public sealed class LogManager
    {

        private const string DefaultGeneralCategory = "General";
        private const MessageType DefaultMessageType = MessageType.Error;

        private static LogWriter logWriter;

        static LogManager() { 
            
            LogWriterFactory logWriterFactory = new LogWriterFactory();  // loads configuration data
            logWriter = logWriterFactory.Create();
            
        }

        /// <summary>
        /// Records the message. Uses the default GeneralCategory and MessageType
        /// </summary>
        /// <param name="message">The error message.</param>
        public static void RecordMessage(String message)
        {
            RecordMessage(message, DefaultMessageType, DefaultGeneralCategory);
        }

        /// <summary>
        /// Records a message of a specefic messageType. Uses the default GeneralCategory
        /// </summary>
        /// <param name="message">The error message.</param>
        /// <param name="messageType">The error message type.</param>
        public static void RecordMessage(String message, MessageType messageType)
        {
            RecordMessage(message, messageType, DefaultGeneralCategory);
        }

        /// <summary>
        /// Records a message. 
        /// </summary>
        /// <param name="message">The error message.</param>
        /// <param name="messageType">The error message type.</param>
        /// <param name="categoryType">The error category.</param>
        public static void RecordMessage(String message, MessageType messageType, string category)
        {

            if (logWriter.IsLoggingEnabled()) 
            {
                Console.WriteLine("[" + DateTime.Now.ToString() + "] " 
                    + "(" + messageType.ToString() + ") : " + message);

                // Provided by MS Enterprise Library
                LogEntry entry = new LogEntry();

                entry.Message = message;
                entry.Severity = (TraceEventType)messageType;
                entry.Categories.Add(category);

                logWriter.Write(entry);
            }

        }

        #region Test code. Uncomment for testing


        /* NOTE: Before executing the following code, the project must be 
        /   changed from class library to console application at the project
        /   properties window.
        */
        //public static void Main(String[] args)
        //{
        //    LogManager.RecordMessage("Test Message 1 - ERROR Message", MessageType.Error);
        //    LogManager.RecordMessage("Test Message 2 - WARNING Message", MessageType.Warning);
        //    LogManager.RecordMessage("Test Message 3 - INFO Message", MessageType.Info);

        //    Console.ReadLine();
        //}

        #endregion
    }
}
