using Dapper;
using ModelLayer.Address;
using RepositoryLayer.Context;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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

    }
}
