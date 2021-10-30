using Nascimento.Software.Livraria.Dominio;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using System.Text;
using System.Threading.Tasks;

namespace Nascimento.Software.Livraria.Infraestrutura.Repositorio
{
    public class LivroAutorRepositorio : IConnection
    {
        private SqlConnection _dbConnection;

        public LivroAutorRepositorio() => _dbConnection = new SqlConnection(GetConnectionString());
        public DbConnection GetConnection() => _dbConnection;

        public string GetConnectionString() => Settings.ConnectionString;
        public async Task<bool> Add(LivroAutor entidade)
        {
            var param = new DynamicParameters();
            param.Add("AutorId", entidade.AutorId);
            param.Add("LivroId", entidade.LivroId);

            var query = $@"INSERT INTO LivroAutor(AutorId, LivroId) VALUES (@AutorId, @LivroId)";
            return await _dbConnection.ExecuteAsync(query, param: param, commandType: System.Data.CommandType.Text).ConfigureAwait(false) > 0;
        }

        public async Task<bool> Delete(LivroAutor entidade)
        {
            var param = new DynamicParameters();
            param.Add("AutorId", entidade.AutorId);
            param.Add("LivroId", entidade.LivroId);
            var query = $@"DELETE FROM LivroAutor WHERE AutorId = @AutorId AND LivroId = @LivroId";
            return await _dbConnection.ExecuteAsync(query, param: param, commandType: System.Data.CommandType.Text).ConfigureAwait(false) > 0;
        }

        public async Task<LivroAutor> Get(int id)
        {
            throw new NotImplementedException();
        }
        public async Task<IEnumerable<LivroAutor>> GetAll()
        {
            var query = $@"SELECT AUTORID, LIVROID FROM LIVROAUTOR";
            return await _dbConnection.QueryAsync<LivroAutor>(query, commandType: System.Data.CommandType.Text).ConfigureAwait(false);
        }
        public async Task<bool> Update(LivroAutor entidade)
        {
            throw new NotImplementedException();
        }
    }
}
