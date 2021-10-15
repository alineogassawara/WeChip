using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WeChip.Models;

namespace WeChip.Controllers
{
    public class ClienteController : Controller
    {
        private readonly WeChipContext _context;

        public ClienteController(WeChipContext context)
        {
            this._context = context;
        }

        public async Task<ActionResult> Index()
        {
            var clientes = await _context.Clientes.OrderBy(x => x.Nome).AsNoTracking().ToListAsync();
            return View(clientes);
        }

        [HttpGet]
        public async Task<IActionResult> Cadastrar (int? id)
        {

            if (id.HasValue)
            {
                var cliente = await _context.Clientes.FindAsync(id);
                if (cliente == null)
                {
                    return NotFound();
                }
                return View(cliente);
            }
            return View(new ClienteModel());
        }

        private bool ClienteExiste(int id)
        {
            return _context.Clientes.Any(x => x.IdCliente == id);
        }

        [HttpPost]
        public async Task<IActionResult> Cadastrar (int? id, [FromForm] ClienteModel cliente)
        {
            if (ModelState.IsValid)
            {
                if (id.HasValue)
                {
                    if (ClienteExiste(id.Value))
                    {
                        _context.Clientes.Update(cliente);
                        if (await _context.SaveChangesAsync() > 0)
                        {
                            TempData["mensagem"] = MensagemModel.Serializar("Cliente alterado com sucesso.");
                        }
                        else
                        {
                            TempData["mensagem"] = MensagemModel.Serializar("Erro ao alterar cliente.", TipoMensagem.Erro);
                        }
                    }
                    else
                    {
                        TempData["mensagem"] = MensagemModel.Serializar("Cliente não encontrado.", TipoMensagem.Erro);
                    }
                }
                else
                {
                    _context.Clientes.Add(cliente);

                    if (await _context.SaveChangesAsync() > 0)
                    {
                        TempData["mensagem"] = MensagemModel.Serializar("Cliente cadastrado com sucesso.");
                    }
                    else
                    {
                        TempData["mensagem"] = MensagemModel.Serializar("Erro ao cadastrar cliente.", TipoMensagem.Erro);
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            else 
            {
                return View(cliente);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Excluir (int ? id)
        {
            if (!id.HasValue)
            {
                TempData["mensagem"] = MensagemModel.Serializar("Cliente não informado.", TipoMensagem.Erro);
                return RedirectToAction(nameof(Index));
            }

            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null)
            {
                TempData["mensagem"] = MensagemModel.Serializar("Cliente não encontrado.", TipoMensagem.Erro);
                return RedirectToAction(nameof(Index));
            }
            return View(cliente);
        }

        [HttpPost]
        public async Task<IActionResult> Excluir(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente != null)
            {
                _context.Clientes.Remove(cliente);
                if (await _context.SaveChangesAsync() > 0)
                     TempData["mensagem"] = MensagemModel.Serializar("Cliente excluído com sucesso.");
                else
                     TempData["mensagem"] = MensagemModel.Serializar("Não foi possível excluir o cliente.", TipoMensagem.Erro);   
                return RedirectToAction(nameof(Index));       
            }
            else
            {
                TempData["mensagem"] = MensagemModel.Serializar("Cliente não encontrado.", TipoMensagem.Erro);
                return RedirectToAction(nameof(Index));
            }
        }
    }
}