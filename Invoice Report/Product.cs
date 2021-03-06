﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using DatabaseHelper;
using PhotoHelper;

namespace BusinessObjects
{
    public class Product : HeaderData
    {
        #region Private Members
        private String _Name = String.Empty;
        private String _Description = String.Empty;
        private Decimal _Price = 0;
        private Byte[] _Image = null;
        private Guid _CategoryID = Guid.Empty;
        private BrokenRuleList _BrokenRules = new BrokenRuleList();
        private String _FilePath = String.Empty;
        private String _RelativeFileName = String.Empty;
        #endregion

        #region Public Properties
        public String FilePath
        {
            get { return _FilePath; }
            set { _FilePath = value; }
        }
        public String RelativeFileName
        {
            get { return _RelativeFileName; }
            set { _RelativeFileName = value; }
        }
        public String Name
        {
            get { return _Name; }
            set
            {
                if (_Name != value)
                {
                    _Name = value;
                    base.IsDirty = true;
                    Boolean Savable = IsSavable();
                    SavableEventArgs e = new SavableEventArgs(Savable);
                    RaiseEvent(e);
                }
            }
        }
        public String Description
        {
            get { return _Description; }
            set
            {
                if (_Description != value)
                {
                    _Description = value;
                    base.IsDirty = true;
                    Boolean Savable = IsSavable();
                    SavableEventArgs e = new SavableEventArgs(Savable);
                    RaiseEvent(e);
                }
            }
        }
        public Decimal Price
        {
            get { return _Price; }
            set
            {
                if (_Price != value)
                {
                    _Price = value;
                    base.IsDirty = true;
                    Boolean Savable = IsSavable();
                    SavableEventArgs e = new SavableEventArgs(Savable);
                    RaiseEvent(e);
                }
            }
        }
        public Byte[] Image
        {
            get { return _Image; }
            set
            {
                if (_Image != value)
                {
                    _Image = value;
                    base.IsDirty = true;
                    Boolean Savable = IsSavable();
                    SavableEventArgs e = new SavableEventArgs(Savable);
                    RaiseEvent(e);
                }
            }
        }
        public Guid CategoryID
        {
            get { return _CategoryID; }
            set
            {
                if (_CategoryID != value)
                {
                    _CategoryID = value;
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
                database.Command.CommandText = "tblProductINSERT";
                database.Command.Parameters.Add("@CategoryID", SqlDbType.UniqueIdentifier).Value = _CategoryID;
                database.Command.Parameters.Add("@Name", SqlDbType.VarChar).Value = _Name;
                database.Command.Parameters.Add("@Description", SqlDbType.VarChar).Value = _Description;
                database.Command.Parameters.Add("@Price", SqlDbType.Decimal).Value = _Price;
                database.Command.Parameters.Add("@Image", SqlDbType.Image).Value = Photo.ImageToByteArray(_FilePath);

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

            System.IO.File.Delete(_FilePath);
            return result;
        }
        private Boolean Update(Database database)
        {
            Boolean result = true;

            try
            {
                database.Command.Parameters.Clear();
                database.Command.CommandType = CommandType.StoredProcedure;
                database.Command.CommandText = "tblProductUPDATE";
                database.Command.Parameters.Add("@CategoryID", SqlDbType.UniqueIdentifier).Value = _CategoryID;
                database.Command.Parameters.Add("@Name", SqlDbType.VarChar).Value = _Name;
                database.Command.Parameters.Add("@Description", SqlDbType.VarChar).Value = _Description;
                database.Command.Parameters.Add("@Price", SqlDbType.Decimal).Value = _Price;
                database.Command.Parameters.Add("@Image", SqlDbType.Image).Value = _Image;

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
                database.Command.CommandText = "tblProductDELETE";

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
            Boolean result = true;

            _BrokenRules.List.Clear();

            if (_CategoryID == Guid.Empty)
            {
                result = false;
                BrokenRule rule = new BrokenRule("Invalid ID. ID cannot be empty");
                _BrokenRules.List.Add(rule);
            }
            if (_Name == null || _Name.Trim() == String.Empty)
            {
                result = false;
                BrokenRule rule = new BrokenRule("Invalid Name. Name cannot be empty.");
                _BrokenRules.List.Add(rule);
            }
            if (_Name == null || _Name.Length > 20)
            {
                result = false;
                BrokenRule rule = new BrokenRule("Invalid Name. Name cannot be greater than 20 characters.");
                _BrokenRules.List.Add(rule);
            }
            if (_Description == null || _Description.Trim() == String.Empty)
            {
                result = false;
                BrokenRule rule = new BrokenRule("Invalid Description. Description cannot be empty.");
                _BrokenRules.List.Add(rule);
            }
            if (_Description == null || _Description.Length > 20)
            {
                result = false;
                BrokenRule rule = new BrokenRule("Invalid Description. Description cannot be grater than 20 characters");
                _BrokenRules.List.Add(rule);
            }
            if (_Price == 0)
            {
                result = false;
                BrokenRule rule = new BrokenRule("Invalid Price. Price cannot be empty.");
                _BrokenRules.List.Add(rule);
            }

            return result;
        }
        #endregion

        #region Public Methods
        private Product GetById(Guid id)
        {
            Database database = new Database("Customer");
            DataTable dt = new DataTable();
            database.Command.CommandType = System.Data.CommandType.StoredProcedure;
            database.Command.CommandText = "tblProductGetById";
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
            _CategoryID = (Guid)dr["CategoryID"];
            _Name = dr["Name"].ToString();
            _Description = dr["Description"].ToString();
            _Price = (Decimal)dr["Price"];
            _Image = (Byte[])dr["Image"];
            String filepath = System.IO.Path.Combine(_FilePath, Id.ToString() + ".jpg");
            _RelativeFileName = System.IO.Path.Combine("UploadedImages", Id.ToString() + ".jpg");
            Photo.ByteArrayToFile(_Image, filepath);
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
        public Product Save()
        {
            Boolean result = true;
            Database database = new Database("Customer");

            if (base.IsNew == true && IsSavable() == true)
            {
                result = Insert(database);
            }
            else if (base.Deleted == true && base.IsDirty)
            {
                result = Delete(database);
            }
            else if (base.IsNew == false && IsSavable() == true)
            {
                result = Update(database);
            }

            if (result == true)
            {
                base.IsDirty = false;
                base.IsNew = false;
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