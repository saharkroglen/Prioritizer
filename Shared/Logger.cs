using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;

namespace Shared
{
    public class Logger
    {

        #region Singleton

        private static Logger _instance = null;
        ILog _logProvider;
        private Logger() { }

        public static ILog Instance
        {
            get
            {
                if (_instance == null)
                {
                    Logger instance = new Logger();
                    instance.Initialize();
                    _instance = instance;
                }
                return _instance._logProvider;
            }
        }

        #endregion

        private void Initialize()
        {
            _logProvider = LogManager.GetLogger("prioritizerLogger");
        }

    }
}
