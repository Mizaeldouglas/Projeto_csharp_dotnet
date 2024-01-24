using Ecommerce.Data;
using Ecommerce.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace Ecommerce.Controllers;

public class ProdutoController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<ProdutoController> _logger;

    public ProdutoController(ILogger<ProdutoController> logger,ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }
    // GET
    
    public IActionResult Index()
    {
        var produtos = _context.Produtos.ToList();
        return View(produtos);
    }
    
    public IActionResult Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var produto = _context.Produtos.FirstOrDefault(p => p.Id == id);

        if (produto == null)
        {
            return NotFound();
        }

        return View(produto);
    }

    
    
    public IActionResult Create()
    {
        return View();
    }

    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Produto produto)
    {
        if (ModelState.IsValid)
        {
            _context.Add(produto);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        return View(produto);
    }

    public IActionResult Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var produto = _context.Produtos.FirstOrDefault(p => p.Id == id);

        if (produto == null)
        {
            return NotFound();
        }

        return View(produto);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int? id, Produto produto)
    {
        if (id != produto.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(produto);
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
                throw;
            }

            return RedirectToAction(nameof(Index));
        }
        
        return View(produto);
    }
    public IActionResult Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var produto = _context.Produtos.FirstOrDefault(p => p.Id == id);

        if (produto == null)
        {
            return NotFound();
        }

        return View(produto);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id)
    {
        var produto = _context.Produtos.FirstOrDefault(p => p.Id == id);
        _context.Produtos.Remove(produto);
        _context.SaveChanges();
        return RedirectToAction(nameof(Index));
    }
}