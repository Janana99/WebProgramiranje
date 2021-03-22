using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Parkingbackend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Logging;

namespace parkingbackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ParkingController : ControllerBase
    {
        public ParkingContext Context {get; set;}

        public ParkingController(ParkingContext context)
        {
            Context=context;
        }

        [Route("PreuzmiParking")]
        [HttpGet]
        public async Task<List<Parking>> PreuzmiParking()
        {
            return await Context.Parkinzi.Include(p => p.ParkingPolja).ThenInclude(p => p.Automobili).ToListAsync();
        }

        [Route("UpisiParking")]
        [HttpPost]
        public async Task UpisiParking([FromBody] Parking parking)
        {
            Context.Parkinzi.Add(parking);
            await Context.SaveChangesAsync();
        }

        [Route("IzmeniParking")]
        [HttpPut]
        public async Task IzmeniParking([FromBody] Parking parking)
        {
            Context.Update<Parking>(parking);
            await Context.SaveChangesAsync();
        }

        [Route("IzbrisiParking/{id}")]
        [HttpDelete]
        public async Task IzbrisiParking (int id)
        {
            var parking=await Context.Parkinzi.FindAsync(id);
            Context.Remove(parking);
            await Context.SaveChangesAsync();
        }

        [Route("PreuzmiPolje")]
        [HttpGet]
        public async Task<List<Polje>> PreuzmiPolje()
        {
            return await Context.Polja.Include(p => p.Automobili).ToListAsync();
        }

        [Route("UpisiPolje/{idParkinga}")]  
        [HttpPost]
        public async Task<IActionResult> UpisiPolje(int idParkinga, [FromBody] Polje polje) 
        {
            var parking = await Context.Parkinzi.FindAsync(idParkinga);
            polje.Parking=parking;
            polje.Maxkapacitet=parking.Kapacitet;  //polje.Maxkapacitet
            polje.Idparkinga=idParkinga;

            if(Context.Polja.Any(p => p.Nazivpolja==polje.Nazivpolja && (p.X != polje.X || p.Y != polje.Y) && p.Idparkinga==polje.Idparkinga))
            {
                var polje1=Context.Polja.Where(p => p.Nazivpolja==polje.Nazivpolja && p.Idparkinga==polje.Idparkinga).FirstOrDefault();
                return BadRequest(new {X=polje1?.X, Y=polje1?.Y});
            }
            if(Context.Polja.Any(p => (p.X==polje.X && p.Y==polje.Y) && p.Idparkinga ==polje.Idparkinga))
            {
                return StatusCode(406);
            }
            else
            {
                Context.Polja.Add(polje);
                await Context.SaveChangesAsync();
                return Ok();
            }
        }

        [Route("IzmeniPolje")] 
        [HttpPut]
        public async Task IzmeniPolje([FromBody] Polje polje)
        {
            Context.Update<Polje>(polje);
            await Context.SaveChangesAsync();
        }

        [Route("IzbrisiPolje/{idPolja}")]
        [HttpDelete]
        public async Task IzbrisiPolje(int idPolja)
        {
            var polje=Context.Polja.Include(p =>p.Automobili).Where(p =>p.ID==idPolja).FirstOrDefault();
            var tmp=polje.Automobili.Select(p =>p.ID).ToList();
            foreach (int element in tmp)
                await IzbrisiAutomobil(idPolja, element);
            Context.Remove(polje);
            await Context.SaveChangesAsync();
        }

        [Route("PreuzmiAutomobil")]
        [HttpGet]
        public async Task<List<Automobil>> PreuzmiAutomobil()
        {
            return await Context.Automobili.ToListAsync();
        }

        [Route("UpisiAutomobil/{idPolja}")]
        [HttpPost]
        public async Task<IActionResult> UpisiAutomobil(int idPolja, [FromBody] Automobil automobil)
        {
            var polje=await Context.Polja.FindAsync(idPolja);
            automobil.Polje=polje;
            automobil.Idpolja=polje.ID;

            if(polje.Maxkapacitet < polje.Brojautomobila + automobil.Automobilibroj)
            {
                return StatusCode(400);
            }
            if(Context.Automobili.Any(p => p.Tip==automobil.Tip && p.Idpolja ==automobil.Idpolja))
            {
                return StatusCode(400);
            }
            polje.Brojautomobila += automobil.Automobilibroj;
            Context.Add(automobil);
            await Context.SaveChangesAsync();
            return Ok();
        }

        [Route("IzbrisiAutomobil/{idPolja}/{idAutomobila}")] 
        [HttpDelete]
        public async Task IzbrisiAutomobil(int idPolja, int idAutomobila)
        {
            var polje= await Context.Polja.FindAsync(idPolja);
            if(polje !=null)
            {
                var automobil= await Context.Automobili.FindAsync(idAutomobila);
                Context.Remove(automobil);
                await Context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Polje i automobil ne postoje!");
            }
        }
    }
}
