using Nascimento.Software.Livraria.Dominio.Dominios;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Dapper;
using System.Threading.Tasks;

namespace Nascimento.Software.Livraria.Infraestrutura.Repositorio
{
    public class LivroRepositorio : IConnection, IRepositorio<Livro>
    {
        private SqlConnection _dbConnection;

        public LivroRepositorio() => _dbConnection = new SqlConnection(GetConnectionString());
        public DbConnection GetConnection() => _dbConnection;
        public string GetConnectionString() => Settings.ConnectionString;

        public async Task<bool> Add(Livro entidade)
        {
            var param = new DynamicParameters();
            param.Add("Nome", entidade.Nome);
            param.Add("Descricao", entidade.Descricao);
            param.Add("CategoriaId", entidade.CategoriaId);
            param.Add("FotoId", entidade.FotoId);
            param.Add("AutorId", entidade.AutorId);
            param.Add("QtdePaginas", entidade.QtdePaginas);

            var query = $@"INSERT INTO Livro(NOME,DESCRICAO,CATEGORIAID,FOTOID,AUTORID,QTDEPAGINAS)
                            VALUES(@Nome, @Descricao, @CategoriaId, @FotoId, @AutorId, @QtdePaginas)";

            return await _dbConnection.ExecuteAsync(query, param: param, commandType: System.Data.CommandType.Text).ConfigureAwait(false) > 0;

        }

        public async Task<bool> Delete(Livro entidade)
        {
            var param = new DynamicParameters();
            param.Add("Id", entidade.Id);

            var query = $"DELETE FROM Livro WHERE Id = @Id";
            return await _dbConnection.ExecuteAsync(query, param: param, commandType: System.Data.CommandType.Text).ConfigureAwait(false) > 0;
        }

        public async Task<Livro> Get(int id)
        {
            var param = new DynamicParameters();
            param.Add("Id", id);
            var query = $@"SELECT ID,NOME,DESCRICAO,CATEGORIAID,FOTOID,AUTORID,QTDEPAGINAS FROM LIVRO WHERE ID = @ID";
            return await _dbConnection.QueryFirstOrDefaultAsync<Livro>(query, param: param, commandType: System.Data.CommandType.Text).ConfigureAwait(false);
        }

        public async Task<IEnumerable<Livro>> GetAll()
        {
            var query = $@"SELECT ID,NOME,DESCRICAO,CATEGORIAID,FOTOID,AUTORID,QTDEPAGINAS FROM LIVRO";
            return await _dbConnection.QueryAsync<Livro>(query, commandType: System.Data.CommandType.Text).ConfigureAwait(false);
        }
        public async Task<bool> Update(Livro entidade)
        {
            var param = new DynamicParameters();
            param.Add("Id", entidade.Id);
            param.Add("Nome", entidade.Nome);
            param.Add("Descricao", entidade.Descricao);
            param.Add("CategoriaId", entidade.CategoriaId);
            param.Add("FotoId", entidade.FotoId);
            param.Add("AutorId", entidade.AutorId);
            param.Add("QtdePaginas", entidade.QtdePaginas);

            var query = $@"UPDATE Livro SET NOME = @NOME, DESCRICAO = @DESCRICAO, CATEGORIAID = @CATEGORIAID, FOTOID = @FOTOID, AUTORID = @AUTORID,
                            QTDEPAGINAS = @QTDEPAGINAS WHERE ID = @ID";

            return await _dbConnection.ExecuteAsync(query, param: param, commandType: System.Data.CommandType.Text).ConfigureAwait(false) > 0;
        }
    }
}
