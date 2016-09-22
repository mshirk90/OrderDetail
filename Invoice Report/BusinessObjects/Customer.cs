using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Data;
using DatabaseHelper;
using System.Web.Security;

namespace BusinessObjects
{
    public class Customer : HeaderData
    {
        // old password cannot be the same as the new password
        #region Private Members
        private String _FirstName = String.Empty;
        private String _LastName = String.Empty;
        private String _Email = String.Empty;
        private String _Password = String.Empty;
        private Boolean _EmailSent = false;
        private BrokenRuleList _BrokenRules = new BrokenRuleList();
        private Boolean _IsPasswordPending = false;
        #endregion

        #region Public Properties
        public String FirstName
        {
            get { return _FirstName; }
            set
            {
                if (_FirstName != value)
                {
                    _FirstName = value;
                    base.IsDirty = true;
                    Boolean Savable = IsSavable();
                    SavableEventArgs e = new SavableEventArgs(Savable);
                    RaiseEvent(e);
                }
            }
        }
        public String LastName
        {
            get { return _LastName; }
            set
            {
                if (_LastName != value)
                {
                    _LastName = value;
                    base.IsDirty = true;
                    Boolean Savable = IsSavable();
                    SavableEventArgs e = new SavableEventArgs(Savable);
                    RaiseEvent(e);
                }
            }
        }
        public String FullName
        {
            get { return String.Concat(_FirstName, " ", _LastName); }
        }
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
        public Boolean EmailSent
        {
            get { return _EmailSent; }
            set { _EmailSent = value; }
        }
        public BrokenRuleList BrokenRules
        {
            get { return _BrokenRules; }
        }
        public Boolean IsPasswordPending
        {
            get { return _IsPasswordPending; }
            set
            {
                if (_IsPasswordPending != value)
                {
                    _IsPasswordPending = value;
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
                database.Command.CommandText = "tblCustomerINSERT";
                database.Command.Parameters.Add("@FirstName", SqlDbType.VarChar).Value = _FirstName;
                database.Command.Parameters.Add("@LastName", SqlDbType.VarChar).Value = _LastName;
                database.Command.Parameters.Add("@Email", SqlDbType.VarChar).Value = _Email;
                database.Command.Parameters.Add("@Password", SqlDbType.VarChar).Value = _Password;

                // Provides the empty buckets
                base.Initialize(database, Guid.Empty);
                database.ExecuteNonQuery();

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
                database.Command.CommandText = "tblCustomerUPDATE";
                database.Command.Parameters.Add("@FirstName", SqlDbType.VarChar).Value = _FirstName;
                database.Command.Parameters.Add("@LastName", SqlDbType.VarChar).Value = _LastName;
                database.Command.Parameters.Add("@Email", SqlDbType.VarChar).Value = _Email;
                database.Command.Parameters.Add("@Password", SqlDbType.VarChar).Value = _Password;
                database.Command.Parameters.Add("@IsPasswordPending", SqlDbType.Bit).Value = _IsPasswordPending;

                // Provides the empty buckets
                base.Initialize(database, base.Id);
                database.ExecuteNonQuery();

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
                database.Command.CommandText = "tblCustomerDELETE";

                // Provides the empty buckets
                base.Initialize(database, base.Id);
                database.ExecuteNonQuery();

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
            _BrokenRules.List.Clear();
            Boolean result = true;

            if (_FirstName == null || _FirstName.Trim() == String.Empty)
            {
                result = false;
                BrokenRule rule = new BrokenRule("First Name cannot be empty.");
                _BrokenRules.List.Add(rule);
            }
            if (_LastName == null || _LastName.Trim() == String.Empty)
            {
                result = false;
                BrokenRule rule = new BrokenRule("Last Name cannot be empty.");
                _BrokenRules.List.Add(rule);
            }
            if (_Email == null || _Email.Trim() == String.Empty)
            {
                result = false;
                BrokenRule rule = new BrokenRule("Email cannot be empty");
                _BrokenRules.List.Add(rule);
            }
            if (_Password == null || _Password.Trim() == String.Empty)
            {
                result = false;
                BrokenRule rule = new BrokenRule("Password cannot be empty.");
                _BrokenRules.List.Add(rule);
            }
            if (IsStrongPassword(_Password) == false)
            {
                result = false;
                BrokenRule rule = new BrokenRule("Weak password.");
                _BrokenRules.List.Add(rule);
            }
            if (_FirstName == null || _FirstName.Length > 20)
            {
                result = false;
                BrokenRule rule = new BrokenRule("First Name must be less than 20 characters.");
                _BrokenRules.List.Add(rule);
            }
            if (_LastName == null || _LastName.Length > 20)
            {
                result = false;
                BrokenRule rule = new BrokenRule("Last Name must be less than 20 characters.");
                _BrokenRules.List.Add(rule);
            }

            if (_Password == null || _Password.Length > 20)
            {
                result = false;
                BrokenRule rule = new BrokenRule("Password must be less than 20 characters.");
                _BrokenRules.List.Add(rule);
            }

            String pattern = @"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?";
            Regex expression = new Regex(pattern);
            if(expression.IsMatch(_Email) == false)
            {
                result = false;
                BrokenRule rule = new BrokenRule("Invalid email format.");
                _BrokenRules.List.Add(rule);
            }

            return result;
        }
        public Boolean IsStrongPassword(String password)
        {
            HashSet<char> specialCharacters = new HashSet<char>() { '%', '$', '#' };
            Boolean result = false;

            int conditionsCount = 0;
            if (password.Any(char.IsLower))
                conditionsCount++;
            if (password.Any(char.IsUpper))
                conditionsCount++;
            if (password.Any(char.IsDigit))
                conditionsCount++;
            if (password.Any(specialCharacters.Contains))
                conditionsCount++;

            if (conditionsCount >= 3)
            {
                //valid password
                result = true;
            }

            return result;
        }
        #endregion

        #region Public Methods
        public Customer GetById(Guid id)
        {
            Database database = new Database("Customer");
            DataTable dt = new DataTable();
            database.Command.CommandType = System.Data.CommandType.StoredProcedure;
            database.Command.CommandText = "tblCustomerGetById";
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
        public void InitializeBusinessData(DataRow dr)
        {
            _FirstName = dr["FirstName"].ToString();
            _LastName = dr["LastName"].ToString();
            _Email = dr["Email"].ToString();
            _Password = dr["Password"].ToString();
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
        public Customer Save()
        {
            Boolean result = true;
            Database database = new Database("Customer");

            // begin transaction

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

            // end transaction
            // commit
            // rollback
            


            return this;
        }
        public Customer Login(String email, String password)
        {
            Database database = new Database("Customer");
            DataTable dt = new DataTable();
            database.Command.CommandType = System.Data.CommandType.StoredProcedure;
            database.Command.CommandText = "tblCustomerLogin";
            database.Command.Parameters.Add("@Email", SqlDbType.VarChar).Value = email;
            database.Command.Parameters.Add("@Password", SqlDbType.VarChar).Value = password;

            dt = database.ExecuteQuery();
            if (dt != null && dt.Rows.Count == 1)
            {
                DataRow dr = dt.Rows[0];
                base.Initialize(dr);
                InitializeBusinessData(dr);
                base.IsNew = false;
                base.IsDirty = false;
                return this;
            }
            else
            {
                return null; // typically a good idea to have only one entry and one exit per method
            }
        }
        public Customer Register(String firstName, String lastName, String email)
        {
            // Generate a new 12-character password with 1 non-alphanumeric character
            String password = Membership.GeneratePassword(12, 1);
            try
            {
                Database database = new Database("Customer");
                database.Command.Parameters.Clear();
                database.Command.CommandType = CommandType.StoredProcedure;
                database.Command.CommandText = "tblCustomerINSERT";
                database.Command.Parameters.Add("@FirstName", SqlDbType.VarChar).Value = firstName;
                database.Command.Parameters.Add("@LastName", SqlDbType.VarChar).Value = lastName;
                database.Command.Parameters.Add("@Email", SqlDbType.VarChar).Value = email;
                database.Command.Parameters.Add("@Password", SqlDbType.VarChar).Value = password;

                _FirstName = firstName;
                _LastName = lastName;
                _Email = email;
                _Password = password;

                base.IsDirty = true;

                if (this.IsSavable() == true)
                {
                    // Provides the empty buckets
                    base.Initialize(database, Guid.Empty);
                    database.ExecuteNonQuery();

                    // Unloads the full buckets into the object
                    base.Initialize(database.Command);
                    base.IsNew = false;
                }
                else
                {
                    throw new Exception("Invalid Register Data.");
                }
            }
            catch (Exception e)
            {
                throw;
            }

            return this;
        }
        public Customer Exists(String email)
        {
            Database database = new Database("Customer");
            DataTable dt = new DataTable();
            database.Command.CommandType = System.Data.CommandType.StoredProcedure;
            database.Command.CommandText = "tblCustomerEXISTS";
            database.Command.Parameters.Add("@Email", SqlDbType.VarChar).Value = email;

            dt = database.ExecuteQuery();
            if (dt != null && dt.Rows.Count == 1)
            {
                DataRow dr = dt.Rows[0];
                base.Initialize(dr);
                InitializeBusinessData(dr);
                base.IsNew = false;
                base.IsDirty = false;
                return this;
            }
            else
            {
                return null; // typically a good idea to have only one entry and one exit per method
            }
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
