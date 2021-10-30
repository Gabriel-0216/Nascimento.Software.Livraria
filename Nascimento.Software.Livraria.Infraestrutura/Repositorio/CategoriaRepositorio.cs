using Nascimento.Software.Livraria.Dominio.Dominios;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace Nascimento.Software.Livraria.Infraestrutura.Repositorio
{
    public class CategoriaRepositorio : IRepositorio<Categoria>, IConnection
    {
        private SqlConnection _dbConnection;
        public CategoriaRepositorio() => _dbConnection = new SqlConnection(GetConnectionString());
        public DbConnection GetConnection() => _dbConnection;
        public string GetConnectionString() => Settings.ConnectionString;

        public async Task<bool> Add(Categoria entidade)
        {
            var param = new DynamicParameters();
            param.Add("Nome", entidade.Nome);

            var query = $@"INSERT INTO CATEGORIA(Nome) VALUES (@Nome)";

            return await _dbConnection.ExecuteAsync(query, param: param, commandType: System.Data.CommandType.Text).ConfigureAwait(false) > 0;
        }

        public async Task<bool> Delete(Categoria entidade)
        {
            var param = new DynamicParameters();
            param.Add("Id", entidade.Id);
            var query = $@"DELETE FROM CATEGORIA WHERE ID = @ID";
            return await _dbConnection.ExecuteAsync(query, param: param, commandType: System.Data.CommandType.Text).ConfigureAwait(false) > 0;
        }

        public async Task<Categoria> Get(int id)
        {
            var param = new DynamicParameters();
            param.Add("Id", id);
            var query = $@"SELECT ID, NOME FROM CATEGORIA WHERE ID = @ID";
            return await _dbConnection.QueryFirstOrDefaultAsync<Categoria>(query, param: param, commandType: System.Data.CommandType.Text).ConfigureAwait(false);
        }

        public async Task<IEnumerable<Categoria>> GetAll()
        {
            var query = $@"SELECT ID, NOME FROM CATEGORIA";
            return await _dbConnection.QueryAsync<Categoria>(query, commandType: System.Data.CommandType.Text).ConfigureAwait(false);

        }

        public async Task<bool> Update(Categoria entidade)
        {
            var param = new DynamicParameters();
            param.Add("Id", entidade.Id);
            param.Add("Nome", entidade.Nome);
            var query = $@"UPDATE CATEGORIA SET NOME = @NOME WHERE ID = @ID";
            return await _dbConnection.ExecuteAsync(query, param: param, commandType: System.Data.CommandType.Text).ConfigureAwait(false) > 0;
        }
    }
}
