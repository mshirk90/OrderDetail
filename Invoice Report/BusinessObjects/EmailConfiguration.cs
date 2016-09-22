using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using DatabaseHelper;
using System.Web.Security;

namespace BusinessObjects
{
    public class EmailConfiguration : HeaderData
    {
        #region Private Members
        private String _Email = String.Empty;
        private String _DisplayName = String.Empty;
        private String _Password = String.Empty;
        private String _Host = String.Empty;
        private Int32 _Port = 0;
        #endregion

        #region Public Properties
        public String Email
        {
            get { return _Email; }
            set
            {
                if (_Email != value)
                {
                    _Email = value;
                    base.IsDirty = true;
                    Boolean Savable = IsSavable();
                    SavableEventArgs e = new SavableEventArgs(Savable);
                    RaiseEvent(e);
                }
            }
        }
        public String DisplayName
        {
            get { return _DisplayName; }
            set
            {
                if (_DisplayName != value)
                {
                    _DisplayName = value;
                    base.IsDirty = true;
                    Boolean Savable = IsSavable();
                    SavableEventArgs e = new SavableEventArgs(Savable);
                    RaiseEvent(e);
                }
            }
        }
        public String Password
        {
            get { return _Password; }
            set
            {
                if (_Password != value)
                {
                    _Password = value;
                    base.IsDirty = true;
                    Boolean Savable = IsSavable();
                    SavableEventArgs e = new SavableEventArgs(Savable);
                    RaiseEvent(e);
                }
            }
        }
        public String Host
        {
            get { return _Host; }
            set
            {
                if (_Host != value)
                {
                    _Host = value;
                    base.IsDirty = true;
                    Boolean Savable = IsSavable();
                    SavableEventArgs e = new SavableEventArgs(Savable);
                    RaiseEvent(e);
                }
            }
        }
        public Int32 Port
        {
            get { return _Port; }
            set
            {
                if (_Port != value)
                {
                    _Port = value;
                    base.IsDirty = true;
                    Boolean Savable = IsSavable();
                    SavableEventArgs e = new SavableEventArgs(Savable);
                    RaiseEvent(e);
                }
            }
        }
        #endregion

        #region Private Methods
        private Boolean Insert(Database database)
        {
            Boolean result = true;

            try
            {
                database.Command.Parameters.Clear();
                database.Command.CommandType = CommandType.StoredProcedure;
                database.Command.CommandText = "tblEmailConfigINSERT";
                database.Command.Parameters.Add("@Email", SqlDbType.VarChar).Value = _Email;
                database.Command.Parameters.Add("@DisplayName", SqlDbType.VarChar).Value = _DisplayName;
                database.Command.Parameters.Add("@Password", SqlDbType.VarChar).Value = _Password;
                database.Command.Parameters.Add("@Host", SqlDbType.VarChar).Value = _Host;
                database.Command.Parameters.Add("@Port", SqlDbType.Int).Value = _Port;

                // Provides the empty buckets
                base.Initialize(database, Guid.Empty);
                database.ExecuteNonQueryWithTransaction();

                // Unloads the full buckets into the object
                base.Initialize(database.Command);
            }
            catch (Exception e)
            {
                result = false;
                throw;
            }

            return result;
        }
        private Boolean Update(Database database)
        {
            Boolean result = true;

            try
            {
                database.Command.Parameters.Clear();
                database.Command.CommandType = CommandType.StoredProcedure;
                database.Command.CommandText = "tblEmailConfigUPDATE";
                database.Command.Parameters.Add("@Email", SqlDbType.VarChar).Value = _Email;
                database.Command.Parameters.Add("@DisplayName", SqlDbType.VarChar).Value = _DisplayName;
                database.Command.Parameters.Add("@Password", SqlDbType.VarChar).Value = _Password;
                database.Command.Parameters.Add("@Host", SqlDbType.VarChar).Value = _Host;
                database.Command.Parameters.Add("@Port", SqlDbType.Int).Value = _Port;

                // Provides the empty buckets
                base.Initialize(database, base.Id);
                database.ExecuteNonQueryWithTransaction();

                // Unloads the full buckets into the object
                base.Initialize(database.Command);
            }
            catch (Exception e)
            {
                result = false;
                throw;
            }

            return result;
        }
        private Boolean Delete(Database database)
        {
            Boolean result = true;

            try
            {
                database.Command.Parameters.Clear();
                database.Command.CommandType = CommandType.StoredProcedure;
                database.Command.CommandText = "tblEmailConfigDELETE";

                // Provides the empty buckets
                base.Initialize(database, base.Id);
                database.ExecuteNonQueryWithTransaction();

                // Unloads the full buckets into the object
                base.Initialize(database.Command);
            }
            catch (Exception e)
            {
                result = false;
                throw;
            }

            return result;
        }
        private Boolean IsValid()
        {
            Boolean result = true;

            if (_Email == null || _Email.Trim() == String.Empty)
            {
                result = false;
            }
            if (_DisplayName == null || _DisplayName.Trim() == String.Empty)
            {
                result = false;
            }
            if (_Password == null || _Password.Trim() == String.Empty)
            {
                result = false;
            }
            if (_Host == null || _Host.Trim() == String.Empty)
            {
                result = false;
            }
            if (_Port == 0)
            {
                result = false;
            }


            if (_Email == null || _Email.Length > 20)
            {
                result = false;
            }
            if (_DisplayName == null || _DisplayName.Length > 20)
            {
                result = false;
            }
            if (_Host == null || _Host.Length > 20)
            {
                result = false;
            }
            if (_Port == 0 || _Port > 65535)
            {
                result = false;
            }

            return result;
        }
        #endregion

        #region Public Methods
        public EmailConfiguration GetById(Guid id)
        {
            Database database = new Database("Customer");
            DataTable dt = new DataTable();
            database.Command.CommandType = System.Data.CommandType.StoredProcedure;
            database.Command.CommandText = "tblEmailConfigGetById";
            base.Initialize(database, base.Id);
            dt = database.ExecuteQuery();
            if (dt != null && dt.Rows.Count == 1)
            {
                DataRow dr = dt.Rows[0];
                base.Initialize(dr);
                InitializeBusinessData(dr);
                base.IsNew = false;
                base.IsDirty = false;
            }

            return this;
        }
        public EmailConfiguration GetByDisplayName(String displayName)
        {
            Database database = new Database("Customer");
            DataTable dt = new DataTable();
            database.Command.CommandType = CommandType.StoredProcedure;
            database.Command.CommandText = "tblEmailConfigGetByDisplayName";
            database.Command.Parameters.Add("@DisplayName", SqlDbType.VarChar).Value = displayName;
            dt = database.ExecuteQuery();
            if(dt != null && dt.Rows.Count == 1)
            {
                DataRow dr = dt.Rows[0];
                base.Initialize(dr);
                InitializeBusinessData(dr);
                base.IsNew = false;
                base.IsDirty = false;
            }

            return this;
        }
        public void InitializeBusinessData(DataRow dr)
        {
            _Email = dr["Email"].ToString();
            _DisplayName = dr["DisplayName"].ToString();
            _Password = dr["Password"].ToString();
            _Host = dr["Host"].ToString();
            _Port = (Int32)dr["Port"];
        }
        public Boolean IsSavable()
        {
            Boolean result = false;

            if ((base.IsDirty == true) && (IsValid() == true))
            {
                result = true;
            }

            return result;
        }
        public EmailConfiguration Save()
        {
            Boolean result = true;
            Database database = new Database("Customer");
            database.BeginTransaction();

            if (base.IsNew == true && IsSavable() == true)
            {
                result = Insert(database);
            }
            else if (base.Deleted == true && base.IsDirty)
            {
                result = Delete(database);
            }
            else if ((base.IsNew == false) && (IsValid() == true) && (IsDirty == true))
            {
                result = Update(database);
            }

            if (result == true)
            {
                base.IsDirty = false;
                base.IsNew = false;
            }

            // save the children
            // save the child

            // save the child
            // save the child
            // save the child

            // close connection
            if (result == true)
            {
                database.EndTransaction();
            }
            else
            {
                database.RollBack();
            }


            return this;
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