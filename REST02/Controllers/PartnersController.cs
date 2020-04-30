using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using REST02.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace REST02.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PartnersController : ControllerBase
    {
        PartnersContext db;
        public PartnersController(PartnersContext context)
        {
            db = context;
            //Если БД пуста, то заполнить тестовыми данными
            if (!db.Partners.Any())
            {
                db.Partners.Add(new Partner { Name = "Alpha", Type = Enums.PartnerType.LegalEntity, INN = "6465154", KPP = "4816151" });
                db.Partners.Add(new Partner { Name = "Beta", Type = Enums.PartnerType.IP, INN = "4686816", KPP = "04108606" });
                db.Partners.Add(new Partner { Name = "Gamma", Type = Enums.PartnerType.IP, INN = "0555457", KPP = "045734" });
                db.SaveChanges();
            }
        }

        // GET: api/partners
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Partner>>> Get()
        {
            return await db.Partners.ToListAsync();
        }

        // GET api/partners/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Partner>> Get(int id)
        {
            Partner partner = await db.Partners.FirstOrDefaultAsync(x => x.Id == id);
            if (partner == null)
            {
                return NotFound();
            }            
            return new ObjectResult(partner);
        }

        // POST api/partners
        [HttpPost]
        public async Task<ActionResult<Partner>> Post(Partner partner)
        {
            if (!partner.Valid())
            {
                return BadRequest();
            }
            //Проверка Контрагента на наличие в БД по параметру Name
            if (db.Partners.Any(x => x.Name == partner.Name) || db.Partners.Any(x => x.Id == partner.Id))
            {
                return BadRequest();
            }

            db.Partners.Add(partner);
            await db.SaveChangesAsync();
            return Ok(partner);
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Partner>> Put(Partner partner)
        {
            if (!partner.Valid())
            {
                return BadRequest();
            }
            if (!db.Partners.Any(x => x.Id == partner.Id))
            {
                return NotFound();
            }
            //Проверка Контрагента на наличие в БД по параметру Name. Ограничение на создание записи с таким же именем
            if (db.Partners.Any(x => x.Name == partner.Name))
            {
                return BadRequest();
            }


            db.Update(partner);
            await db.SaveChangesAsync();
            return Ok(partner);
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Partner>> Delete(int id)
        {
            Partner partner = db.Partners.FirstOrDefault(x => x.Id == id);
            if (partner == null)
            {
                return NotFound();
            }
            db.Partners.Remove(partner);
            await db.SaveChangesAsync();
            return Ok(partner);
        }
    }
}
