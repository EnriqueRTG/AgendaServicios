using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AgendaServicios.Web.Data;
using AgendaServicios.Web.Models;

namespace AgendaServicios.Web.Controllers
{
    public class TurnosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TurnosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Turnos
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Turnos.Include(t => t.Cliente).Include(t => t.EstadoTurno).Include(t => t.Servicio);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Turnos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Turnos == null)
            {
                return NotFound();
            }

            var turno = await _context.Turnos
                .Include(t => t.Cliente)
                .Include(t => t.EstadoTurno)
                .Include(t => t.Servicio)
                .FirstOrDefaultAsync(m => m.TurnoId == id);
            if (turno == null)
            {
                return NotFound();
            }

            return View(turno);
        }

        // GET: Turnos/Create
        public IActionResult Create()
        {
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "ClienteId", "Apellido");
            ViewData["EstadoTurnoId"] = new SelectList(_context.EstadosTurnos, "EstadoTurnoId", "Descripcion");
            ViewData["ServicioId"] = new SelectList(_context.Servicios, "ServicioId", "Descripcion");
            return View();
        }

        // POST: Turnos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TurnoId,FechaTurno,HoraTurno,EstadoTurnoId,ClienteId,ServicioId,Observacion")] Turno turno)
        {
            if (ModelState.IsValid)
            {
                _context.Add(turno);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "ClienteId", "Apellido", turno.ClienteId);
            ViewData["EstadoTurnoId"] = new SelectList(_context.EstadosTurnos, "EstadoTurnoId", "Descripcion", turno.EstadoTurnoId);
            ViewData["ServicioId"] = new SelectList(_context.Servicios, "ServicioId", "Descripcion", turno.ServicioId);
            return View(turno);
        }

        // GET: Turnos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Turnos == null)
            {
                return NotFound();
            }

            var turno = await _context.Turnos.FindAsync(id);
            if (turno == null)
            {
                return NotFound();
            }
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "ClienteId", "Apellido", turno.ClienteId);
            ViewData["EstadoTurnoId"] = new SelectList(_context.EstadosTurnos, "EstadoTurnoId", "Descripcion", turno.EstadoTurnoId);
            ViewData["ServicioId"] = new SelectList(_context.Servicios, "ServicioId", "Descripcion", turno.ServicioId);
            return View(turno);
        }

        // POST: Turnos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TurnoId,FechaTurno,HoraTurno,EstadoTurnoId,ClienteId,ServicioId,Observacion")] Turno turno)
        {
            if (id != turno.TurnoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(turno);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TurnoExists(turno.TurnoId))
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
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "ClienteId", "Apellido", turno.ClienteId);
            ViewData["EstadoTurnoId"] = new SelectList(_context.EstadosTurnos, "EstadoTurnoId", "Descripcion", turno.EstadoTurnoId);
            ViewData["ServicioId"] = new SelectList(_context.Servicios, "ServicioId", "Descripcion", turno.ServicioId);
            return View(turno);
        }

        // GET: Turnos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Turnos == null)
            {
                return NotFound();
            }

            var turno = await _context.Turnos
                .Include(t => t.Cliente)
                .Include(t => t.EstadoTurno)
                .Include(t => t.Servicio)
                .FirstOrDefaultAsync(m => m.TurnoId == id);
            if (turno == null)
            {
                return NotFound();
            }

            return View(turno);
        }

        // POST: Turnos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Turnos == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Turnos'  is null.");
            }
            var turno = await _context.Turnos.FindAsync(id);
            if (turno != null)
            {
                _context.Turnos.Remove(turno);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TurnoExists(int id)
        {
          return (_context.Turnos?.Any(e => e.TurnoId == id)).GetValueOrDefault();
        }
    }
}
