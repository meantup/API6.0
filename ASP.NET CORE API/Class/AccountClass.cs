using ASP.NET_CORE_API.Model;
using ASP.NET_CORE_API.Repository;
using Dapper;
using System.Data.SqlClient;

namespace ASP.NET_CORE_API.Class
{
    public class AccountClass : IAccount
    {
        private readonly IConfiguration config;
        private SqlConnection sql;
        public AccountClass(IConfiguration _config)
        {
            config = _config;
            sql = new SqlConnection(config["ConnectionStrings:Account"]);
        }

        public async Task<ServiceResponse<object>> getUserLogin(string username)
        {
            var response = new ServiceResponse<object>();
            try
            {
                if (sql.State == System.Data.ConnectionState.Closed)
                    sql.Open();
                var dynamic = new DynamicParameters();
                dynamic.Add("type", "getUser");
                dynamic.Add("username", username);
                //dynamic.Add("password", password);
                dynamic.Add("isvalid", dbType: System.Data.DbType.Int32, size: 100, direction: System.Data.ParameterDirection.Output);

                var res = await sql.QueryAsync<object>("usp_Account", dynamic, commandType: System.Data.CommandType.StoredProcedure);
                var ret = dynamic.Get<int>("isvalid");
                response.code = ret.Equals(1) ? 200 : 400;
                response.message = ret.Equals(1) ? "Successfully Login!" : ret.Equals(2) ? "Username or Password is Incorrect! Please Try Again." : "No Record Found!";
                response.Data = res.FirstOrDefault();
            }
            catch (SqlException sqlex)
            {
                response.code = 500;
                response.message = "Sql Exception Error";
                response.Data = new { stacktrace = sqlex.StackTrace, msg = sqlex.Message, sqlErrorCode = sqlex.ErrorCode };
            }
            catch (Exception ex)
            {
                response.code = 500;
                response.message = "Exception Error";
                response.Data = new { stacktrace = ex.StackTrace, msg = ex.Message };
            }
            return response;
        }

        public async Task<ServiceResponse1<object>> register(AccountModel accmodel)
        {
            var response = new ServiceResponse1<object>();
            try
            {
                if (sql.State == System.Data.ConnectionState.Closed)
                    sql.Open();
                var dynamic = new DynamicParameters();
                dynamic.Add("type","insert");
                dynamic.Add("isvalid", dbType: System.Data.DbType.Int32, size: 100, direction: System.Data.ParameterDirection.Output);
                var property = accmodel.GetType().GetProperties();
              
                foreach (var item in property)
                {
                    var name = item.Name;
                    var value = item.GetValue(accmodel);
                    dynamic.Add(name, value);   
                }
                var res = await sql.ExecuteAsync("usp_Account",dynamic,commandType: System.Data.CommandType.StoredProcedure);
                var retval = dynamic.Get<int>("isvalid");
                response.message = retval.Equals(1) ? "You are Successfully Registered!" 
                                 : retval.Equals(2) ? "You have an account already" 
                                 : retval.Equals(3) ? "Invalid Password! Must be start UPPERCASE & include special character !@#$%^&*()-_+=.,;:~" 
                                 : retval.Equals(4) ? "Invalid Username Format! ex.herra@gmail.com" 
                                                        : "Username already Taken!";
                response.code = retval.Equals(1) ? 200 : 400;
            }
            catch (SqlException sql)
            {
                response.code = 500;
                response.message = new { stacktrace = sql.StackTrace, msg = sql.Message, SqlErrorCode = sql.ErrorCode };
            }
            catch (Exception ex)
            {
                response.code = 500;
                response.message = new { stacktrace = ex.StackTrace, msg = ex.Message };
            }
            return response;
        }
        public async Task<ServiceResponse<object>> selectALLregister()
        {
            var response = new ServiceResponse<object>();
            try
            {
                if (sql.State == System.Data.ConnectionState.Closed)
                    sql.Open();
                var dynamic = new DynamicParameters();
                dynamic.Add("type", "select");
                dynamic.Add("isvalid", dbType: System.Data.DbType.Int32, size: 100, direction: System.Data.ParameterDirection.Output);
                var res = await sql.QueryAsync<object>("usp_Account",dynamic,commandType:System.Data.CommandType.StoredProcedure);
                var ret = dynamic.Get<int>("isvalid");
                response.code = ret.Equals(1) ? 200 : 400;
                response.message = ret.Equals(1) ? "Successfully Fetch Record!" : "No Record to Fetch!";
                response.Data = res.ToList();
            }
            catch (SqlException sqlex)
            {
                response.code = 500;
                response.message = "Sql Exception Error";
                response.Data = new { stacktrace = sqlex.StackTrace, msg = sqlex.Message, sqlErrorCode = sqlex.ErrorCode };
            }
            catch (Exception ex)
            {
                response.code = 500;
                response.message = "Exception Error";
                response.Data = new { stacktrace = ex.StackTrace, msg = ex.Message };
            }
            return response;
        }
    }
}
