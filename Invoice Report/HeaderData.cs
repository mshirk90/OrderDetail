using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using DatabaseHelper;
using System.Data;

namespace BusinessObjects
{
    public class HeaderData : Event
    {
        #region Private Members
        private Guid _Id = Guid.Empty;
        private Int32 _Version = 0;
        private DateTime _LastUpdated = DateTime.MaxValue;
        private Boolean _Deleted = false;

        private Boolean _IsNew = true;
        private Boolean _IsDirty = true;
        #endregion

        #region Public Properties
        public Guid Id
        {
            get { return _Id; }
            set { _Id = value; }
        }

        public Int32 Version
        {
            get { return _Version; }
            set { _Version = value; }
        }

        public DateTime LastUpdated
        {
            get { return _LastUpdated; }
            set { _LastUpdated = value; }
        }

        public Boolean Deleted
        {
            get { return _Deleted; }
            set
            {
                _Deleted = value;
                _IsDirty = true;
            }
        }

        public Boolean IsNew
        {
            get { return _IsNew; }
            set { _IsNew = value; }
        }

        public Boolean IsDirty
        {
            get { return _IsDirty; }
            set { _IsDirty = value; }
        }

        #endregion

        #region Private Methods

        #endregion

        #region Public Methods

        public void Initialize(Database database, Guid id)
        {
            SqlParameter parm = new SqlParameter();
            parm.ParameterName = "@Id";
            parm.Direction = ParameterDirection.InputOutput;
            parm.SqlDbType = SqlDbType.UniqueIdentifier;
            parm.Value = id;
            database.Command.Parameters.Add(parm);

            parm = new SqlParameter();
            parm.ParameterName = "@Version";
            parm.Direction = ParameterDirection.Output;
            parm.SqlDbType = SqlDbType.Int;
            parm.Value = 0;
            database.Command.Parameters.Add(parm);

            parm = new SqlParameter();
            parm.ParameterName = "@LastUpdated";
            parm.Direction = ParameterDirection.Output;
            parm.SqlDbType = SqlDbType.DateTime;
            parm.Value = DateTime.MaxValue;
            database.Command.Parameters.Add(parm);

            parm = new SqlParameter();
            parm.ParameterName = "@Deleted";
            parm.Direction = ParameterDirection.Output;
            parm.SqlDbType = SqlDbType.Bit;
            parm.Value = 0;
            database.Command.Parameters.Add(parm);
        }

        public void Initialize(SqlCommand cmd)
        {
            _Id = (Guid)cmd.Parameters["@ID"].Value;
            _Version = (Int32)cmd.Parameters["@Version"].Value;
            _LastUpdated = (DateTime)cmd.Parameters["@LastUpdated"].Value;
            _Deleted = (Boolean)cmd.Parameters["@Deleted"].Value;
        }

        public void Initialize(DataRow dr)
        {
            _Id = (Guid)dr["Id"];
            _Version = (Int32)dr["Version"];
            _LastUpdated = (DateTime)dr["LastUpdated"];
            _Deleted = (Boolean)dr["Deleted"];
        }
        #endregion

        #region Public Events

        #endregion

        #region Public Event Handlers

        #endregion

        #region Construction

        #endregion

    }
}