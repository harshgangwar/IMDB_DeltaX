using DeltaX.EF.DeltaXDB;
using System;
using System.IO;

namespace DeltaX.EF.LoggingService
{
    public static class ErrorLogService
    {
        /// <summary>
        /// Logging the Error into txt file and also in DB table
        /// </summary>
        /// <param name="exc">Exception</param>
        /// <param name="actionName">Action Name</param>
        /// <param name="controllerName">Controller Name</param>
        /// <param name="loginUserId">Login UserId</param>
        public static void WriteError(Exception exc, string actionName = null, string controllerName = null, int loginUserId = 0)
        {
            WriteLogDb(exc, actionName, controllerName, loginUserId);
            LogException(exc);
        }

        #region Static Methods

        /// <summary>
        /// Log an Exception
        /// </summary>
        /// <param name="exc">Exception</param>
        private static void LogException(Exception exc)
        {
            //Default Path for logs to be stored
            var logFile = string.Format(@"logs\logger_{0}.txt", DateTime.Now.ToString("MM-dd-yyyy"));

            //Include logic for logging exceptions, Get the absolute path to the log file
            logFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, logFile);

            CreateLogFile(logFile);

            //Open the log file for append and write the log
            using (var streamWriter = new StreamWriter(logFile, true))
            {
                streamWriter.WriteLine("********** {0} **********", DateTime.Now);

                if (exc.InnerException != null)
                {
                    streamWriter.Write("Inner Exception Type: ");
                    streamWriter.WriteLine(exc.InnerException.GetType().ToString());
                    streamWriter.Write("Inner Exception: ");
                    streamWriter.WriteLine(exc.InnerException.Message);
                    streamWriter.Write("Inner Source: ");
                    streamWriter.WriteLine(exc.InnerException.Source);

                    if (exc.InnerException.StackTrace != null)
                    {
                        streamWriter.WriteLine("Inner Stack Trace: ");
                        streamWriter.WriteLine(exc.InnerException.StackTrace);
                    }
                }

                streamWriter.Write("Exception Type: ");
                streamWriter.WriteLine(exc.GetType().ToString());
                streamWriter.WriteLine("Exception: " + exc.Message);
                streamWriter.WriteLine("Source: " + string.Empty);
                streamWriter.WriteLine("Stack Trace: ");

                if (exc.StackTrace != null)
                {
                    streamWriter.WriteLine(exc.StackTrace);
                    streamWriter.WriteLine();
                }
            }
        }

        /// <summary>
        /// Method to create Log file
        /// </summary>
        /// <param name="fileName">FileName</param>
        private static void CreateLogFile(string fileName)
        {
            if (!File.Exists(fileName))
            {
                using (Stream stream = File.Create(fileName))
                {
                    TextWriter tw = new StreamWriter(stream);
                    tw.Close();
                }
                //File.Create(fileName, Constant.DefaultBufferSize, FileOptions.Asynchronous).Close();
            }
        }

        /// <summary>
        /// Method to write log into db
        /// </summary>
        /// <param name="exception">Exception</param>
        /// <param name="actionName">Action name/Method name</param>
        /// <param name="controllerName">controllerName/ClassName</param>
        /// <param name="loginUserId">LoginUserId</param>
        private static void WriteLogDb(Exception exception, string actionName, string controllerName, int loginUserId)
        {
            try
            {
                using (var actionMapUnit = new GenericUnitOfWork())
                {

                    // We can make a table for error log 
                    //var dbErrorlog = new DB_ERROR_LOG
                    //{
                        //ActionName = actionName,
                        //ContollerName = controllerName,
                        //ErrorMessage = exception.ToString(),
                        //LoginUserId = loginUserId,
                        //HostName = Dns.GetHostName(),
                        //CreateDate = DateTime.Now
                    //};
                    //actionMapUnit.DbErrorUnitOfWork.Insert(dbErrorlog);
                    //actionMapUnit.Save();
                }
            }
            catch (Exception ex)
            {
                LogException(ex);
                throw;
            }
        }

        #endregion

    }
}