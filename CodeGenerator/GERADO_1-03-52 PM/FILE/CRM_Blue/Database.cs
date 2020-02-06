using System;
using System.Collections.Generic;
using System.Web;
using System.Data.SqlClient;
using System.Data.Common;
using System.Data;
using System.Configuration;

/// <summary>
/// Classe base para acesso o banco de dados
/// </summary>
namespace CRM_Blue
{
    public class Database
    {
        private DbConnection _db = new SqlConnection();
        private string _databaseName = string.Empty;

        private void Init()
        {
            if (HttpContext.Current.Items["DB"] != null)
            {
                _db = (DbConnection)HttpContext.Current.Items["DB"];
            }
            else
            {
                _db = new SqlConnection();

                HttpContext.Current.Items.Add("DB", _db);
            }

            if (_db.State != ConnectionState.Open)
            {
                _db.ConnectionString = ConfigurationManager.ConnectionStrings[_databaseName].ToString();

                _db.Open();
            }
        }

        public void AddInParameter(DbCommand command, string name, DbType type, object value)
        {
            DbParameter objParam = command.CreateParameter();

            objParam.ParameterName = name;
            objParam.DbType = type;
            objParam.Value = value;
            objParam.Direction = ParameterDirection.Input;

            command.Parameters.Add(objParam);
        }

        public void AddOutParameter(DbCommand command, string name, DbType type, object value)
        {
            DbParameter objParam = command.CreateParameter();

            objParam.ParameterName = name;
            objParam.DbType = type;
            objParam.Value = value;
            objParam.Direction = ParameterDirection.InputOutput;

            command.Parameters.Add(objParam);
        }

        public DbCommand CreateCommand()
        {
            Init();

            return _db.CreateCommand();
        }

        public DbConnection CreateConnection()
        {
            return _db;
        }

        public DataTable ExecuteDataTable(DbCommand command)
        {
            DataTable dataTable = new DataTable();

            dataTable.Load(command.ExecuteReader());
            
            return dataTable;
        }

        public DataTable ExecuteDataTable(CommandType commandType, string commandText)
        {
            DbCommand command = CreateCommand();

            command.CommandText = commandText;
            command.CommandType = commandType;
            
            return ExecuteDataTable(command);
        }

        public void ExecuteNonQuery(DbCommand command)
        {
            command.ExecuteNonQuery();
        }

        public IDataReader ExecuteReader(DbCommand command)
        {
            IDataReader dataReader = command.ExecuteReader();

            return dataReader;
        }

		public DataSet ExecuteDataSet(DbCommand command)
        {
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = (SqlCommand)command;
            DataSet ds = new DataSet();
            da.Fill(ds, command.CommandText);
            return ds;
        }

        public DataSet ExecuteDataSet(CommandType commandType, string commandText)
        {
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand.CommandType = commandType;
            da.SelectCommand.CommandText = commandText;            
            DataSet ds = new DataSet();
            da.Fill(ds, commandText);
            return ds;
        }

        public object ExecuteScalar(DbCommand command)
        {
            return command.ExecuteScalar();
        }

        public object GetParameterValue(DbCommand command, string parameterName)
        {
            return command.Parameters[parameterName].Value;
        }

        public DbCommand GetSqlStringCommand(string sql)
        {
            DbCommand command = CreateCommand();

            command.CommandText = sql;
            command.CommandType = CommandType.Text;
            
            return command;
        }

        public DbCommand GetStoredProcCommand(string procedureName)
        {
            DbCommand command = CreateCommand();

            command.CommandText = procedureName;
            command.CommandType = CommandType.StoredProcedure;
            
            return command;
        }

        public Database(string strDataBaseName)
        {
            _databaseName = strDataBaseName;
        }

        public Database()
            : this("db")
        {
        }
    }
}
