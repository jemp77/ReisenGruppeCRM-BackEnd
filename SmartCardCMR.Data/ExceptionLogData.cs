using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using SmartCardCRM.Data.Entities;
using SmartCardCRM.Util;
using System;
using System.Linq;

namespace SmartCardCRM.Data
{
    public class ExceptionLogData<T> : BaseData
    {
        public ExceptionLogData(SmartCardCRMContext context)
        {
            _context = context;
        }

        
        public T Log(string componentName, Func<T> function)
        {
            try
            {
                return function.Invoke();
            }
            catch (Exception ex)
            {
                _context.ChangeTracker.Entries().Where(e => e.State == EntityState.Added ||
                    e.State == EntityState.Modified ||
                    e.State == EntityState.Deleted).ToList().ForEach(x => x.State = EntityState.Detached);
                LogException(componentName, string.Format("Source: {0}, Exception: {1}, Inner Exception: {2}", ex.Source, ex.Message, (ex.InnerException == null ? "" : ex.InnerException.Message)), ex.StackTrace);
                throw;
            }
        }

        public T LogWithTransaction(string componentName, Func<T> function)
        {
            var transaction = _context.Database.BeginTransaction();
            try
            {
                var result = function.Invoke();
                transaction.Commit();
                return result;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                _context.ChangeTracker.Entries().Where(e => e.State == EntityState.Added ||
                    e.State == EntityState.Modified ||
                    e.State == EntityState.Deleted).ToList().ForEach(x => x.State = EntityState.Detached);

                LogException(componentName, string.Format("Source: {0}, Exception: {1}, Inner Exception: {2}", ex.Source, ex.Message, (ex.InnerException == null ? "" : ex.InnerException.Message)), ex.StackTrace);
                throw;
            }
        }

        public void LogException(string componentName, string exceptionMessage, string traceLog)
        {
            var log = new ExceptionLog()
            {
                ComponentName = componentName,
                ExceptionMessage = exceptionMessage,
                ExceptionTraceLog = traceLog,
                ExceptionDate = CustomDateTime.Now
            };
            _context.ExceptionLog.Add(log);
            _context.SaveChanges();
        }
    }
}
