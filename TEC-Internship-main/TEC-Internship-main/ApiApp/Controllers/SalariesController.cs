﻿using Internship.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Internship.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalariesController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            var db = new APIDbContext();
            var list = db.Salaries.ToList();
            return Ok(list);
        }

        [HttpDelete("{Id}")]
        public IActionResult Delete(int Id)
        {
            var db = new APIDbContext();
            Salary salary = db.Salaries.Find(Id);
            if (salary == null)
                return NotFound();
            else
            {
                db.Salaries.Remove(salary);
                db.SaveChanges();
                return NoContent();
            }
        }
    }
}
