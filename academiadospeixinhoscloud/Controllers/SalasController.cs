﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using academiadospeixinhoscloud.Data;
using academiadospeixinhoscloud.Models;
using Microsoft.AspNetCore.Authorization;

namespace academiadospeixinhoscloud.Controllers
{
    [Authorize(Roles = "ADMIN")]
    public class SalasController : Controller
    {
        private readonly academiadospeixinhoscloudContext _context;

        public SalasController(academiadospeixinhoscloudContext context)
        {
            _context = context;
        }

        // GET: Salas
        public async Task<IActionResult> Index()
        {
            return View(await _context.Sala.ToListAsync());
        }

        // GET: Salas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sala = await _context.Sala
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sala == null)
            {
                return NotFound();
            }

            return View(sala);
        }

        // GET: Salas/Create
        public IActionResult Create()
        {
            ViewBag.Subscricoes = new SelectList(_context.Subscricao, "Nome", "Nome");
            return View();
        }

        // POST: Salas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Descricao,VagasPreenchidas,VagasTotal,Educadora,Auxiliar1,Auxiliar2,Auxiliar3,NomesSubscricoes")] Sala sala)
        {
            ViewBag.Subscricoes = new SelectList(_context.Subscricao, "Nome", "Nome");
            if (ModelState.IsValid)
            {
                _context.Add(sala);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(sala);
        }

        // GET: Salas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            ViewBag.NomesSubscricoes = new SelectList(_context.Subscricao, "Nome", "Nome");
            if (id == null)
            {
                return NotFound();
            }

            var sala = await _context.Sala.FindAsync(id);
            if (sala == null)
            {
                return NotFound();
            }
            return View(sala);
        }

        // POST: Salas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Descricao,VagasPreenchidas,VagasTotal,Educadora,Auxiliar1,Auxiliar2,Auxiliar3,NomesSubscricoes")] Sala sala)
        {
            if (id != sala.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sala);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SalaExists(sala.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(sala);
        }

        // GET: Salas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sala = await _context.Sala
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sala == null)
            {
                return NotFound();
            }

            return View(sala);
        }

        // POST: Salas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sala = await _context.Sala.FindAsync(id);
            if (sala != null)
            {
                _context.Sala.Remove(sala);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SalaExists(int id)
        {
            return _context.Sala.Any(e => e.Id == id);
        }
    }
}
