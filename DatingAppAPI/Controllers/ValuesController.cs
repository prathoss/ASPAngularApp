﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatingAppAPI.Models;
using DatingAppAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DatingAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        IValueService _service;

        public ValuesController(IValueService service)
        {
            _service = service;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetValue(int id)
        {
            Value value = await _service.GetByIdAsync(id);
            if (value == null) return NotFound();
            return Ok(value);
        }

        [HttpGet]
        public async Task<IActionResult> GetValues()
        {
            return Ok(await _service.GetAllAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Create(Value value)
        {
            Value item = await _service.Create(value);
            return CreatedAtRoute("GetValue",new { id = item.Id }, item);
        }
    }
}