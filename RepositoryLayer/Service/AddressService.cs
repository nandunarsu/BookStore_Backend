using Dapper;
using ModelLayer.Address;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Service
{
    public class AddressService :IAddress
    {
        private readonly DapperContext context;
        public AddressService(DapperContext _context) { 
            this.context = _context;
        }
        public string AddAddress(AddressRequest address, int userId)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@name", address.name);
                parameters.Add("@mobileNumber", address.mobileNumber);
                parameters.Add("@address", address.address);
                parameters.Add("@city", address.city);
                parameters.Add("@state", address.state);
                parameters.Add("@type", address.type);
                parameters.Add("@userId", userId);

                using (var connection = context.CreateConnection())
                {
                    var result = connection.Execute("spAddAddress", parameters, commandType: CommandType.StoredProcedure);
                    if (result > 0)
                        return "Address Added Successfully";
                    else
                        throw new Exception("Failed to add address.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error: {ex.Message}");
            }
        }
        public List<Object> GetAddress(int userId)
        {
            try
            {
                using (var connection = context.CreateConnection())
                {
                    var addresses = connection.Query<Object>("spGetAddress", new { userId = userId }, commandType: CommandType.StoredProcedure);
                    return (List<object>)addresses;
                }
            }
            catch(SqlException ex)
            {
                throw ex;
            }
        }
        public Address UpdateAddress(int userId, Address addressRequest)
        {
            try
            {
                using (var connection = context.CreateConnection())
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@addressId", addressRequest.addressId);
                    parameters.Add("@name", addressRequest.name);
                    parameters.Add("@mobileNumber", addressRequest.mobileNumber);
                    parameters.Add("@address", addressRequest.address);
                    parameters.Add("@city", addressRequest.city);
                    parameters.Add("@type", addressRequest.type);
                    parameters.Add("@state", addressRequest.state);
                    parameters.Add("@userId", userId);

                    connection.Execute("spUpdateAddress", parameters, commandType: CommandType.StoredProcedure);
                    return addressRequest;
                }
            }catch(SqlException ex)
            {
                throw ex;
            }
           
        }
        public bool DeleteAddress(int addressId)
        {
            try
            {
                using (var connection = context.CreateConnection())
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@addressId", addressId);


                    var result = connection.Execute("Deleteaddress", parameters, commandType: CommandType.StoredProcedure);
                    return result > 0;
                }
            }catch (SqlException ex)
            {
                throw ex; 
            }
        }


    }
}
