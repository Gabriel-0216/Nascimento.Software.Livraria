using Nascimento.Software.Livraria.Dominio.Dominios.Emprestimos;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using System.Text;
using System.Threading.Tasks;
using Nascimento.Software.Livraria.Dominio.Dominios.Identity;

namespace Nascimento.Software.Livraria.Infraestrutura.Emprestimos
{
    public class processo_devolucao : IConnection
    {
        private SqlConnection _sqlConnection;
        public DbConnection GetConnection() => _sqlConnection;
        public string GetConnectionString() => Settings.ConnectionString;

        public processo_devolucao()
        {
            _sqlConnection = new SqlConnection(GetConnectionString());
        }

        private async Task<int> GeradorId()
        {
            var query = $@"SELECT MAX(Id) FROM DEVOLUCAO_EMPRESTIMO";
            var retorno = await GetConnection().ExecuteScalarAsync(query, commandType: System.Data.CommandType.Text).ConfigureAwait(false);
            if(!Convert.IsDBNull(retorno) || !(retorno != null))
            {
                return Convert.ToInt32(retorno);
            }
            return 0;
        }

        public async Task<bool> IniciarDevolucao(Devolucao devolucao)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("Id", await GeradorId());

                param.Add("EmprestimoId", devolucao.IdEmprestimo);

                param.Add("LivroId", devolucao.LivroId);

                param.Add("UserId", devolucao.UserId);
                param.Add("Email", devolucao.UserEmail);
                param.Add("LivroNome", devolucao.NomeLivro);
                param.Add("DataDevolucaoAgendada", devolucao.DataDevolucao);
                param.Add("DataEmprestimo", devolucao.DataEmprestimo);
                param.Add("DataDevolucaoConfirmada", DateTime.Today);

                //atualizar tabela emprestimos com checbokx em devolvido

                var query = $@"begin transaction;
                                --registra a devolução na tabela de empréstimos
                            INSERT INTO DEVOLUCAO_EMPRESTIMO(Id, IdEmprestimo, LivroId, UserId, UserEmail, DataDevolucao, DataEmprestimo, DataDevolucaoConfirmada, NomeLivro) VALUES(@Id,
                            @EmprestimoId, @LivroId, @UserId, @Email, @DataDevolucaoAgendada, @DataEmprestimo, @DataDevolucaoConfirmada, @LivroNome)
                                --atualiza o registro de aluguel pra devolvido
                        UPDATE Aluguel_Livro SET Devolvido = 'S' where Id = @EmprestimoId AND UserId = @UserId;
                            commit;";

                return await GetConnection().ExecuteAsync(query, param: param, commandType: System.Data.CommandType.Text).ConfigureAwait(false) > 0;
            }
            catch (Exception)
            {
                return false;
            }

        }
        public async Task<IEnumerable<Devolucao>> GetDevolucoes(Usuario user)
        {
            var param = new DynamicParameters();
            param.Add("UserId", user.Id);
            var query = $@"SELECT Id, IdEmprestimo, LivroId, UserId, UserEmail, DataDevolucao, DataDevolucaoConfirmada FROM DEVOLUCAO_EMPRESTIMO WHERE UserId = @UserId";

            return await GetConnection().QueryAsync<Devolucao>(query, param: param, commandType: System.Data.CommandType.Text).ConfigureAwait(false);
        }
    }
}
