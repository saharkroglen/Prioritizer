using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Objects;
using Prioritizer.Shared.Model;

namespace PrioritizerService.Model
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();

    }
    public class PrioritizerUnitOfWork : IUnitOfWork, IDisposable
    {
        public prioritizerDBEntities Context { get; set; }

        public PrioritizerUnitOfWork(prioritizerDBEntities context)
        {

            Context = context;
            //sahar - sqlCE does not support commandTimeout which is different than zero. 
            //when move back to sql server un-remark this section!
            /*if (context.Connection != null && context.Connection.ConnectionTimeout >= 300)
            {
                Context.CommandTimeout = context.Connection.ConnectionTimeout;
            }
            else
            {
                Context.CommandTimeout = 300; //TODO - get from setting
            }*/

        }

        #region IUnitOfWork Members

        public void Commit()
        {
            Context.SaveChanges();
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~PrioritizerUnitOfWork()
        {
            Dispose(false);
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                //free managed resources
                if (Context != null)
                {
                    Context.Dispose();
                    Context = null;
                }
            }
            //no native resources to free
        }

        #endregion
    }

    
}