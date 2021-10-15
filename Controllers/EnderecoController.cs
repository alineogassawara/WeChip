using System;
using System.Linq;
using System.Threading.Tasks;
using WeChip.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WeChip.Controllers
{
    public class EnderecoController : Controller
    {
        private readonly WeChipContext _context;

        public EnderecoController(WeChipContext context)
        {
            this._context = context;
        }

        public async Task<IActionResult> Index(int? cid)
        {
            if (cid.HasValue)
            {
                var cliente = await _context.Clientes.FindAsync(cid);
                if (cliente != null)
                {
                    _context.Entry(cliente).Collection(c => c.Endereco).Load();
                    ViewBag.Cliente = cliente;
                    return View(cliente.Endereco);
                }
                else
                {
                    TempData["mensagem"] = MensagemModel.Serializar("Cliente não encontrado.", TipoMensagem.Erro);
                    return RedirectToAction("Index", "Cliente");
                }
            }
            else
            {
                TempData["mensagem"] = MensagemModel.Serializar("Só é possível mostrar endereços de um cliente específico.", TipoMensagem.Erro);
                return RedirectToAction("Index", "Cliente");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Cadastrar(int? cid, int? eid)
        {
            if (cid.HasValue)
            {
                var cliente = await _context.Clientes.FindAsync(cid);
                if (cliente != null)
                {
                    ViewBag.Cliente = cliente;
                    if (eid.HasValue)
                    {
                        _context.Entry(cliente).Collection(c => c.Endereco).Load();
                        var endereco = cliente.Endereco.FirstOrDefault(e => e.Id == eid);
                        if (endereco != null)
                        {
                            return View(endereco);
                        }
                        else
                        {
                            TempData["mensagem"] = MensagemModel.Serializar("Endereço não encontrado.", TipoMensagem.Erro);
                        }
                    }
                    else
                    {
                        return View(new EnderecoModel());
                    }
                }
                else
                {
                    TempData["mensagem"] = MensagemModel.Serializar("Cliente não encontrado.", TipoMensagem.Erro);
                }
                return RedirectToAction("Index");
            }
            else
            {
                TempData["mensagem"] = MensagemModel.Serializar("Nenhum proprietário de endereços foi informado.", TipoMensagem.Erro);
                return RedirectToAction("Index", "Cliente");
            }
        }

        private bool EnderecoExiste(int cid, int eid)
        {
            return _context.Clientes.FirstOrDefault(c => c.IdCliente == cid)
                .Endereco.Any(e => e.Id == eid);
        }

        [HttpPost]
        public async Task<IActionResult> Cadastrar([FromForm] int? idCliente,
            [FromForm] EnderecoModel endereco)
        {
            if (idCliente.HasValue)
            {
                var cliente = await _context.Clientes.FindAsync(idCliente);
                ViewBag.Cliente = cliente;

                if (ModelState.IsValid)
                {
                    if (cliente.Endereco.Count() == 0) endereco.Selecionado = true;
                    endereco.CEP = ObterCepNormalizado(endereco.CEP);
                    if (endereco.Id > 0)
                    {
                        if (endereco.Selecionado)
                            cliente.Endereco.ToList().ForEach(e => e.Selecionado = false);

                        if (EnderecoExiste(idCliente.Value, endereco.Id))
                        {
                            var enderecoAtual = cliente.Endereco.FirstOrDefault(e => e.Id == endereco.Id);
                            _context.Entry(enderecoAtual).CurrentValues.SetValues(endereco);
                            if (_context.Entry(enderecoAtual).State == EntityState.Unchanged)
                            {
                                TempData["mensagem"] = MensagemModel.Serializar("Nenhum dado do endereço foi alterado.");
                            }
                            else
                            {
                                if (await _context.SaveChangesAsync() > 0)
                                {
                                    TempData["mensagem"] = MensagemModel.Serializar("Endereço alterado com sucesso.");
                                }
                                else
                                {
                                    TempData["mensagem"] = MensagemModel.Serializar("Erro ao alterar endereço.");
                                }
                            }
                        }
                        else
                        {
                            TempData["mensagem"] = MensagemModel.Serializar("Endereço não encontrado.", TipoMensagem.Erro);
                        }
                    }
                    else
                    {
                        var idEndereco = cliente.Endereco;
                        _context.Clientes.FirstOrDefault(c => c.IdCliente== idCliente).Endereco.Add(endereco);
                        if (await _context.SaveChangesAsync() > 0)
                        {
                            TempData["mensagem"] = MensagemModel.Serializar("Endereço cadastrado com sucesso.");
                        }
                        else
                        {
                            TempData["mensagem"] = MensagemModel.Serializar("Erro ao cadastrar endereço.");
                        }
                    }
                    return RedirectToAction("Index", "Endereco", new { cid = idCliente });
                }
                else
                {
                    return View(endereco);
                }
            }
            else
            {
                TempData["mensagem"] = MensagemModel.Serializar("Nenhum proprietário de endereços foi informado.", TipoMensagem.Erro);
                return RedirectToAction("Index", "Cliente");
            }
        }

        private string ObterCepNormalizado(string cep)
        {
            string cepNormalizado = cep.Replace("-", "").Replace(".", "").Trim();
            return cepNormalizado.Insert(5, "-");
        }

[HttpGet]
        public async Task<IActionResult> Excluir(int? cid, int? eid)
        {
            if (!cid.HasValue)
            {
                TempData["mensagem"] = MensagemModel.Serializar("Cliente não informado.", TipoMensagem.Erro);
                return RedirectToAction("Index", "Cliente");
            }

            if (!eid.HasValue)
            {
                TempData["mensagem"] = MensagemModel.Serializar("Endereço não informado.", TipoMensagem.Erro);
                return RedirectToAction("Index", new { cid = cid });
            }

            var cliente = await _context.Clientes.FindAsync(cid);
            var endereco = cliente.Endereco.FirstOrDefault(e => e.Id == eid);
            if (endereco == null)
            {
                TempData["mensagem"] = MensagemModel.Serializar("Endereço não encontrado.", TipoMensagem.Erro);
                return RedirectToAction("Index", new { cid = cid });
            }

            ViewBag.Cliente = cliente;
            return View(endereco);
        }

        [HttpPost]
        public async Task<IActionResult> Excluir(int idCliente, int id)
        {
            var cliente = await _context.Clientes.FindAsync(idCliente);
            var endereco = cliente.Endereco.FirstOrDefault(e => e.Id == id);
            if (endereco != null)
            {
                cliente.Endereco.Remove(endereco);
                if (await _context.SaveChangesAsync() > 0)
                {
                    TempData["mensagem"] = MensagemModel.Serializar("Endereço excluído com sucesso.");
                    if (endereco.Selecionado && cliente.Endereco.Count() > 0)
                    {
                        cliente.Endereco.FirstOrDefault().Selecionado = true;
                        await _context.SaveChangesAsync();
                    }
                }
                else
                    TempData["mensagem"] = MensagemModel.Serializar("Não foi possível excluir o endereço.", TipoMensagem.Erro);                
            }
            else
            {
                TempData["mensagem"] = MensagemModel.Serializar("Endereço não encontrado.", TipoMensagem.Erro);                
            }
            return RedirectToAction("Index", new { cid = idCliente });
        }
    }
}