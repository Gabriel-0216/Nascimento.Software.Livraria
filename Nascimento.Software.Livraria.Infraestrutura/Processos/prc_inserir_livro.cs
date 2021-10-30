using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dapper;
using System.Threading.Tasks;
using Nascimento.Software.Livraria.Dominio.Dominios;
using System.Data.SqlClient;

namespace Nascimento.Software.Livraria.Infraestrutura.Processos
{
    public class prc_inserir_livro
    {
        private SqlConnection _connection;
        public prc_inserir_livro()
        {
            _connection = new SqlConnection(GetConnectionString());
        }
        private string GetConnectionString()
        {
            return Settings.ConnectionString;
        }
        public async Task<bool> InserirLivroAsync(Livro livro, Foto foto)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("Nome", livro.Nome);
                param.Add("Descricao", livro.Descricao);
                param.Add("CategoriaId", livro.CategoriaId);
                param.Add("AutorId", livro.AutorId);
                param.Add("QtdePaginas", livro.QtdePaginas);
                //Nome, Descricao, CategoriaId, FotoId, AutorId, QtdePaginas
                var query = $@"INSERT INTO Livro(Nome,Descricao,CategoriaId,AutorId,QtdePaginas)OUTPUT INSERTED.Id VALUES(@Nome,@Descricao,
                            @CategoriaId, @AutorId, @QtdePaginas)";

                int outputId =  await _connection.ExecuteScalarAsync<int>(query, param: param, commandType: System.Data.CommandType.Text).ConfigureAwait(false);

                var query_02 = $@"UPDATE Livro SET FotoId = @FotoId where Id = @FotoId";
                var param_02 = new DynamicParameters();
                param_02.Add("FotoId", outputId);

                await _connection.ExecuteAsync(query_02, param_02, commandType: System.Data.CommandType.Text).ConfigureAwait(false);

                var query_03 = $@"INSERT INTO Foto VALUES (@FotoId, @ImagemUrl)";
                var param_03 = new DynamicParameters();
                param_03.Add("FotoId", outputId);
                param_03.Add("ImagemUrl", foto.ImagemURL);

                await _connection.ExecuteAsync(query_03, param_03, commandType: System.Data.CommandType.Text).ConfigureAwait(false);

                var query_04 = $@"INSERT INTO LivroAutor VALUES (@LivroId, @AutorId)";
                var param_04 = new DynamicParameters();
                param_04.Add("LivroId", outputId);
                param_04.Add("AutorId", livro.AutorId);


                return await _connection.ExecuteAsync(query_04, param_04, commandType: System.Data.CommandType.Text).ConfigureAwait(false) > 0;

             


            }
            catch (Exception)
            {
                return false;
            }


              
        }

        public async Task<bool> DeleteLivroAsync(Livro livro)
        {
            var param = new DynamicParameters();
            param.Add("Id", livro.Id);
            var query = $@"DELETE FROM LIVRO WHERE FotoId = @Id DELETE FROM FOTO WHERE FotoId = @Id DELETE FROM LivroAutor WHERE LivroId = @Id";

            return await _connection.ExecuteAsync(query, param: param, commandType: System.Data.CommandType.Text).ConfigureAwait(false) > 0;
        }

        public async Task<IEnumerable<Livro>> GetLivros()
        {
            var query = $@"SELECT Id, Nome, Descricao, CategoriaId, AutorId, QtdePaginas, ImagemURL FROM LIVRO C
                                                        INNER JOIN FOTO F ON C.Id = F.FotoId";

            return await _connection.QueryAsync<Livro>(query, commandType: System.Data.CommandType.Text).ConfigureAwait(false);
        }
        public async Task<Livro> GetLivro(int id)
        {

            var param = new DynamicParameters();
            param.Add("LivroId", id);
            var query = $@"SELECT Id, Nome, Descricao, CategoriaId, AutorId, QtdePaginas, ImagemURL FROM LIVRO C
                                                        INNER JOIN FOTO F ON C.Id = F.FotoId where ID = @LivroId";
            return await _connection.QueryFirstOrDefaultAsync<Livro>(query, param: param, commandType: System.Data.CommandType.Text).ConfigureAwait(false);
        }
   
    }
}
