using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Prioritizer.Shared;

namespace PrioritizerService
{
    public enum Configurations
    {
        LoginAttempts,
        LockTimeOut,
        CookiesTimeOut,
        DaysToChangePassword,
        SmtpClientHostName,
        SmtpNeedCredentials,
        EnableSsl,
        SmtpPassword,
        SmtpUsername,
        SmtpClientPortNumber,
        DBTimeZoneID,
        SystemMailAddressFrom,
        SystemMailAddressFrom_DisplayName,
        MaxFileContent,
        MaxPerScanFee,
        MaxFlatFee,
        DBVersion,
        ConfigGetterInterval,
        PrivilegeGetterInterval,
        MinutesToKeepPrivileges,
        SimprefPassword,
        SegmentsFilePath,
        DayInMonthToExecSegmentsImport,
        HourToExecSegmentsImport,
        StarvationPeriodOfSegmentsProcess,
        SegmentProcessInterval,
        MissingImportFileAlertBuffer,
        SegmentFileLineFields,
        TelstraSegmentFieldPosition,
        MosaicSegmentFieldPosition,
        SegmentFileMinimumLines,
        SegmentsToIgnore,
        tempDirCleanupTimerInterval,
        IPAddressInterval,
        GroupRecords_Screen,
        GroupRecords_Csv,
        HitLogRecords_Csv,
        HitLogRecords_Screen,
        CsvMaxRows,
        GS1Address,
        SupportAddress,
        MaxUseragentProcessed,
        MaxDeviceIdentifierProcessed,
        MaxHitLogs2Resolve,
        HitLogLocationResolveInterval,
        EnableHitLogLocationResolve,
        MaxMindLicenseKey,
        DaysBack2ResolveLocation,
        LastScansToDisplay,
        BestURLsToDisplay,
        RulesGetterInterval,
        AddressesRequireIMEI,
        SimprefAddress,
        QAAddress,
        DevAddress,
        LogRulesManager,
        Default_Language
    }
    public static class ConfigurationData
    {
        /// <summary>
        /// Returns the configuration value from DB. If not exist returns null.
        /// </summary>
        /// <param name="configuration">The enum member which presents the configuration name</param>
        /// <returns></returns>
        public static string GetConfigurationValue(Configurations configuration)
        {
            string value = null;
            try
            {
                //using (DBInigmaDataContext db = new DBInigmaDataContext(ConnectionHelper.GetConnectionString()))
                //{
                //    var config = db.usp_GetConfiguration(configuration.ToString()).SingleOrDefault();
                //    if (config != null)
                //        value = config.Value;
                //}
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return value;
        }

        ///// <summary>
        ///// Returns stored procedure results of all exist configuration records
        ///// </summary>
        ///// <returns></returns>
        //public static usp_GetConfigurationsAllResult[] GetConfigurationsAll()
        //{
        //    using (DBInigmaDataContext db = new DBInigmaDataContext(ConnectionHelper.GetConnectionString()))
        //    {
        //        return db.usp_GetConfigurationsAll().ToArray();
        //    }
        //}

        //public static void UpdateConfigurationValue(Configurations configuration, string newValue)
        //{
        //    using (DBInigmaDataContext db = new DBInigmaDataContext(ConnectionHelper.GetConnectionString()))
        //    {
        //        db.usp_UpdateConfigurationValue(configuration.ToString(), newValue);
        //    }
        //}
    }
  
    /// <summary>
    /// Handles data of configuration values
    /// </summary>
    public static  class ConfigValues
    {
        //Delegate of methods who do parsing.
        delegate T ParseDelegate<T>(string str);

        /// <summary>
        /// Returns the value of wanted configuration data.
        /// </summary>
        /// <param name="config">The wanted configuration data</param>
        /// <returns></returns>
        public static T GetValue<T>(Configurations config) where T:struct
        {
            try
            {
                string value = "value1";// ConfigurationData.GetConfigurationValue((int)config);

                if(value == null)//no value found in DB
                    throw new Exception("Configuration value for " + config.ToString() + " was not implemented") ;

                //return ParseNullable<double>(value, double.Parse);
                return Parser.ToType<T>(value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region Private Methods

        /// <summary>
        /// Returns a parsing value of a givven string.
        /// </summary>
        /// <typeparam name="T">Type of returned value</typeparam>
        /// <param name="str">string to parse</param>
        /// <param name="parse">Parsing method</param>
        /// <returns></returns>
        private static Nullable<T> ParseNullable<T>(string str, ParseDelegate<T> parse) where T : struct
        {
            if (string.IsNullOrEmpty(str))
                return null;
            return parse(str);
        }

        #endregion
    }
}
