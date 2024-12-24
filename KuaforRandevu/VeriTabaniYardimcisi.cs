using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KuaforRandevu
{
    public static class VeriTabaniYardimcisi
    {
        private static readonly string _connectionString = "server=localhost; port=5432; Database=kuafor2; username=postgres; password=12345";

        // Bağlantı nesnesi oluşturur
        public static NpgsqlConnection GetConnection()
        {
            return new NpgsqlConnection(_connectionString);
        }
    }
}
