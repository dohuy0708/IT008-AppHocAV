﻿using System.Data.SqlClient;

namespace IT008_AppHocAV.Repositories.DbConnection
{
    public class DbConnection
    {
        #region Declare Fields
            private readonly SqlConnection _sqlConnection;
            private readonly Authentication _authentication;
            private readonly EssayQ _essayQ;
            private readonly Register _register;
            private readonly ExamQ _examQ;
        #endregion

        #region Declare Properties

        public Authentication Authentication => _authentication;

        public EssayQ EssayQ => _essayQ;

        public Register Register => _register;

        public ExamQ ExamQ => _examQ;
        
        #endregion

        #region Declare Constructors
        public DbConnection()
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = Properties.Settings.Default.DataSource;
            builder.UserID = Properties.Settings.Default.DbUserID;
            builder.Password = Properties.Settings.Default.DbPassword;
            builder.InitialCatalog = Properties.Settings.Default.InitialCatalog;
            _sqlConnection = new SqlConnection(builder.ConnectionString);
            _authentication = new Authentication(_sqlConnection);
            _essayQ = new EssayQ(_sqlConnection);
            _register = new Register(_sqlConnection);
        }
        #endregion
        
    }
}