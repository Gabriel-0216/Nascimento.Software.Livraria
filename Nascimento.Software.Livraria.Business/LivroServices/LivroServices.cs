using Nascimento.Software.Livraria.Infraestrutura.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nascimento.Software.Livraria.Dominio;
using System.Threading.Tasks;
using Nascimento.Software.Livraria.Dominio.Dominios;
using Nascimento.Software.Livraria.Infraestrutura.Processos;

namespace Nascimento.Software.Livraria.Business.LivroServices
{
    public class LivroServices
    {
        private LivroRepositorio _livroRepositorio;
        private FotoRepositorio _fotoRepositorio;
        private prc_inserir_livro _prc_Inserir_Livro;
        private LivroAutorRepositorio _livroAutorRepositorio;

        public LivroServices()
        {
            _livroRepositorio = new LivroRepositorio();
            _fotoRepositorio = new FotoRepositorio();
            _livroAutorRepositorio = new LivroAutorRepositorio();
            _prc_Inserir_Livro = new prc_inserir_livro();
        }

        public async Task<bool> InserirLivro(Livro livro, Foto foto)
        {//inserir livro e inserir na tabela foto
           if(livro == null || foto == null)
            {
                return false;
            }
            try
            {
                await _prc_Inserir_Livro.InserirLivroAsync(livro, foto);


                return true;
            }
            catch (Exception)
            {
                await _fotoRepositorio.Delete(foto);
                await _livroRepositorio.Delete(livro);

                return false;
            }        
        }
        public async Task<bool> RemoverLivro(Livro livro, Foto foto)
        {
            if (livro == null || foto == null)
            {
                return false;
            }
            try
            {
                await _prc_Inserir_Livro.DeleteLivroAsync(livro);
                return true;
            }
            catch (Exception)
            {
               
                return false;
            }
        }
        private async Task<LivroAutor> GetLivroAutor(Livro livro)
        {
            return new LivroAutor()
            {
                AutorId = livro.AutorId,
                LivroId = livro.Id,
            };     
        }
        public async Task<bool> UpdateLivro(Livro livro, Foto foto)
        {//update livro e inserir na tabela foto
            if(livro == null || foto == null)
            {
                return false;
            }
            try
            {
                await _livroRepositorio.Update(livro);
                await _fotoRepositorio.Update(foto);
                await _livroAutorRepositorio.Update(await GetLivroAutor(livro));
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task<Dominio.Dominios.Livro> GetLivro(int id)
        {
            var lista = await _prc_Inserir_Livro.GetLivro(id);
            if (lista == null)
            {
                return null;
            }
            return lista;
        }
        public async Task<IEnumerable<Dominio.Dominios.Livro>> Get()
        {
            var lista = await _prc_Inserir_Livro.GetLivros();
            if (lista == null)
            {
                return null;
            }
            return lista;
        }
        public async Task<Foto> GetFoto(int id)
        {
            var lista = await _fotoRepositorio.Get(id);
            if (lista == null)
            {
                return null;
            }
            return lista;
        }
        public async Task<IEnumerable<Foto>> GetFotos()
        {
            var lista = await _fotoRepositorio.GetAll();
            if (lista == null)
            {
                return null;
            }
            return lista;
        }

    }
}
