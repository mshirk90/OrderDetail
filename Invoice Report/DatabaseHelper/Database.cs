using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using ConfigurationHelper;

namespace DatabaseHelper
{
    public class Database
    {
        #region Private Members
        private String _connectionName = String.Empty;
        private SqlConnection _cn = null;
        private SqlCommand _cmd = null;
        private SqlDataAdapter _da = null;
        private DataTable _dt = null;
        #endregion

        #region Public Properties
        public SqlCommand Command
        {
            get { return _cmd; }
        }
        #endregion

        #region Private Methods

        #endregion

        #region Public Methods
        public void BeginTransaction()
        {
            _cn.ConnectionString = Configuration.GetConnectionString(_connectionName);
            _cmd.Connection = _cn;
            _cn.Open();
            _cmd.Transaction = _cn.BeginTransaction();
        }

        public void EndTransaction()
        {
            _cmd.Transaction.Commit();
            _cn.Close();
        }

        public void RollBack()
        {
            _cmd.Transaction.Rollback();
            _cn.Close();
        }

        public SqlCommand ExecuteNonQueryWithTransaction()
        {
            _cmd.ExecuteNonQuery();

            return _cmd;
        }

        public SqlCommand ExecuteNonQuery()
        {
            _cn.ConnectionString = Configuration.GetConnectionString(_connectionName);
            _cmd.Connection = _cn;
            _cn.Open();

            _cmd.ExecuteNonQuery();
            _cn.Close();

            return _cmd;
        }

        public DataTable ExecuteQuery()
        {
            _cn.ConnectionString = Configuration.GetConnectionString(_connectionName);
            _cmd.Connection = _cn;
            _cn.Open();

            _da.SelectCommand = _cmd;
            _dt = new DataTable();
            _da.Fill(_dt);

            return _dt;
        }
        #endregion

        #region Public Events

        #endregion

        #region Public Event Handlers

        #endregion

        #region Construction
        public Database(String connectionName)
        {
            _connectionName = connectionName;
            _cn = new SqlConnection();
            _cmd = new SqlCommand();
            _da = new SqlDataAdapter();
        }
        #endregion

    }
}