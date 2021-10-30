using Dapper;
using Nascimento.Software.Livraria.Dominio.Dominios;
using Nascimento.Software.Livraria.Infraestrutura;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nascimento.Software.Livraria.Infraestrutura.Repositorio
{
    public class AutorRepositorio : IRepositorio<Autor>, IConnection
    {
        #region Construtores e propriedades privadas
        private DbConnection _dbConnection;

        public AutorRepositorio()
        {
            _dbConnection = new SqlConnection(GetConnectionString());
        }
        public DbConnection GetConnection()
        {
            return _dbConnection;
        }

        public string GetConnectionString()
        {
            return Settings.ConnectionString;
        }

        #endregion
        public async Task<bool> Delete(Autor entidade)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("Id", entidade.Id);
                var query = $"DELETE FROM Autor WHERE Id = @Id";

                return await _dbConnection.ExecuteAsync(query, param: param, commandType: System.Data.CommandType.Text).ConfigureAwait(false) > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task<bool> Add(Autor entidade)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("Id", entidade.Id);
                param.Add("Nome", entidade.Nome);
                param.Add("Sobrenome", entidade.Sobrenome);
                param.Add("Email", entidade.Email);
                param.Add("Telefone", entidade.Telefone);
                param.Add("DataNascimento", entidade.DataNascimento);

                var query = @$"INSERT INTO Autor(Nome, Sobrenome, Email, Telefone, DataNascimento)
                                VALUES(@Nome,@Sobrenome,@Email,@Telefone,@DataNascimento)";

                return await _dbConnection.ExecuteAsync(query, param: param, commandType: System.Data.CommandType.Text).ConfigureAwait(false) > 0;

            }
            catch(Exception)
            {
                return false;
            }
        }
        public async Task<Autor> Get(int id)
        {
            var param = new DynamicParameters();
            param.Add("Id", id);
            var query = @$"SELECT ID,NOME,SOBRENOME,EMAIL,TELEFONE,DATANASCIMENTO FROM AUTOR WHERE Id = @Id";

            return await _dbConnection.QueryFirstOrDefaultAsync<Autor>(query, param: param, commandType: System.Data.CommandType.Text).ConfigureAwait(false);
        }
        public async Task<IEnumerable<Autor>> GetAll()
        {
            var query = @$"SELECT ID,NOME,SOBRENOME,EMAIL,TELEFONE,DATANASCIMENTO FROM AUTOR";
            return await _dbConnection.QueryAsync<Autor>(query, commandType: System.Data.CommandType.Text).ConfigureAwait(false);
        }
        public async Task<bool> Update(Autor entidade)
        {
            var param = new DynamicParameters();
            param.Add("Id", entidade.Id);
            param.Add("Nome", entidade.Nome);
            param.Add("Sobrenome", entidade.Sobrenome);
            param.Add("Email", entidade.Email);
            param.Add("Telefone", entidade.Telefone);
            param.Add("DataNascimento", entidade.DataNascimento);

            var query = @$"UPDATE Autor SET Nome = @Nome, Sobrenome = @Sobrenome, Email = @Email,
                            Telefone = @Telefone, DataNascimento = @DataNascimento WHERE Id = @Id";

            return await _dbConnection.ExecuteAsync(query, param: param, commandType: System.Data.CommandType.Text).ConfigureAwait(false) > 0;
        }

    }
}
