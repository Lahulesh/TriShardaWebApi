using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AmazePay.Security;
using Microsoft.Data.SqlClient;

namespace AmazePay.Repository
{
    public class DataAccessLayerSQL
    {
        SqlCommand _sqlCommand;
        SqlDataAdapter _sqlDataAdater;
        DataSet _dataSet;
        DataTable _dataTable;
        Logger _logger;
        SqlConnection _sqlConnection;
        public DataAccessLayerSQL()
        {
            _sqlCommand = new SqlCommand();
            _sqlDataAdater = new SqlDataAdapter();
            _dataSet = new DataSet();
            _dataTable = new DataTable();
            _logger = new Logger();
            _sqlConnection = new SqlConnection(new ConfigurationBuilder()
    .AddJsonFile("appsettings.json").Build().GetSection("ConnectionStrings")["Database"]);
        }

        /// <summary>
        /// DB connection string
        /// </summary>



        /// <summary>
        /// Method to execute procedure without parameter
        /// </summary>
        /// <returns>Dataset</returns>
        public virtual DataSet ExecuteProcedure(string strProcedureName)
        {
            try
            {
                if (string.IsNullOrEmpty(strProcedureName))
                    throw new ArgumentNullException("Procedure Name is null or empty");

                _sqlCommand.CommandText = strProcedureName;
                _sqlCommand.Connection = _sqlConnection;
                _sqlCommand.CommandType = CommandType.Text;
                _sqlCommand.Parameters.Clear();
                _sqlCommand.CommandTimeout = 100000;
                _sqlDataAdater.SelectCommand = _sqlCommand;
                if (_dataSet.Tables.Contains(strProcedureName))
                    _dataSet.Tables[strProcedureName].Clear();
                _sqlDataAdater.Fill(_dataSet, strProcedureName);
            }
            catch (Exception ex)
            {
                _logger.WriteErrorToFile("Error while accessing database in ExecuteProcedure method. error message: " + ex.Message);
            }
            return _dataSet;
        }

        /// <summary>
        /// Method to execute procedure with parameter
        /// </summary>
        /// <returns>Dataset</returns>
       
        //public virtual DataSet ExecuteProcedure(string strProcedureName, object listdata)
        //{
        //    try
        //    {
        //        if (string.IsNullOrEmpty(strProcedureName))
        //            throw new ArgumentNullException("Procedure Name is null or empty");
        //        _sqlCommand.CommandText = strProcedureName;
        //        _sqlCommand.Connection = _sqlConnection;
        //        _sqlCommand.CommandType = CommandType.StoredProcedure;

        //        _sqlCommand.Parameters.Clear();
        //        foreach (PropertyInfo propertyInfo in listdata.GetType().GetProperties())
        //        {
        //            object value = propertyInfo.GetValue(listdata, null);
        //            if (value != null)
        //                _sqlCommand.Parameters.AddWithValue("@" + propertyInfo.Name, propertyInfo.GetValue(listdata));
        //        }
        //        _sqlCommand.CommandTimeout = 100000;
        //        _sqlDataAdater.SelectCommand = _sqlCommand;
        //        if (_dataSet.Tables.Contains(strProcedureName))
        //            _dataSet.Tables[strProcedureName].Clear();
        //        _sqlDataAdater.Fill(_dataSet, strProcedureName);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.WriteErrorToFile("Error while accessing database in method: ExecuteProcedure(string,object) error message: " + ex.Message);
        //    }
        //    return _dataSet;
        //}

        /// <summary>
        /// Method to execute procedure with parameter and boolan output
        /// </summary>
        /// <returns>Dataset</returns>
      
        //public virtual bool ExecuteProcedureBoolOutput(string strProcedureName, object listdata)
        //{
        //    try
        //    {
        //        if (string.IsNullOrEmpty(strProcedureName))
        //            throw new ArgumentNullException("Procedure Name is null or empty");
        //        _sqlCommand.CommandText = strProcedureName;
        //        _sqlCommand.Connection = _sqlConnection;
        //        _sqlCommand.CommandType = CommandType.StoredProcedure;

        //        _sqlCommand.Parameters.Clear();

        //        foreach (PropertyInfo propertyInfo in listdata.GetType().GetProperties())
        //        {
        //            object value = propertyInfo.GetValue(listdata, null);
        //            if (value != null)
        //                _sqlCommand.Parameters.AddWithValue("@" + propertyInfo.Name, propertyInfo.GetValue(listdata));
        //        }
        //        _sqlCommand.CommandTimeout = 100000;
        //        if (_sqlConnection != null && _sqlConnection.State == ConnectionState.Closed)
        //            _sqlConnection.Open();
        //        int iCount = _sqlCommand.ExecuteNonQuery();
        //        _sqlConnection.Close();
        //        return iCount > 0;
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.WriteErrorToFile("Error while accessing database error in method: ExecuteProcedureBoolOutput error message: " + ex.Message);
        //        Console.WriteLine(ex.Message);
        //        return false;
        //    }
        //}

        /// <summary>
        /// Method to execute procedure with parameter and integer output
        /// </summary>
        /// <returns>Dataset</returns>

        public virtual int ExecuteProcedureIntOutput(string strProcedureName, object listdata)
        {
            try
            {
                if (string.IsNullOrEmpty(strProcedureName))
                    throw new ArgumentNullException("Procedure Name is null or empty");
                _sqlCommand.CommandText = strProcedureName;
                _sqlCommand.Connection = _sqlConnection;
                _sqlCommand.CommandType = CommandType.StoredProcedure;

                _sqlCommand.Parameters.Clear();

                foreach (PropertyInfo propertyInfo in listdata.GetType().GetProperties())
                {
                    object value = propertyInfo.GetValue(listdata, null);
                    if (value != null)
                        _sqlCommand.Parameters.AddWithValue("@" + propertyInfo.Name, propertyInfo.GetValue(listdata));
                }
                _sqlCommand.CommandTimeout = 100000;

                //
                SqlParameter returnvalue = new SqlParameter("returnVal", SqlDbType.Int);
                returnvalue.Direction = ParameterDirection.ReturnValue;
                _sqlCommand.Parameters.Add(returnvalue);
                if (_sqlConnection != null && _sqlConnection.State == ConnectionState.Closed)
                    _sqlConnection.Open();
                _sqlCommand.ExecuteNonQuery();
                int count = Convert.ToInt32(returnvalue.Value);
                _sqlConnection.Close();




                return count;
            }
            catch (Exception ex)
            {
                _logger.WriteErrorToFile("Error while accessing database error in method: ExecuteProcedureBoolOutput error message: " + ex.Message);
                Console.WriteLine(ex.Message);
                return 4;
            }
        }
    }
}
