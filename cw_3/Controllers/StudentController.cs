using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using cw_3.Service;
using cw_3.Controllers;
using cw_3.Models;
using System.Text.Json;

namespace cw_3.Controllers
{
    [ApiController]
    [Route("/students")]
    public class StudentController : ControllerBase
    {
        /*private readonly IDbService _dbService;

        public StudentController(IDbService dbService)
        {
            this._dbService = dbService;
        }*/

        [HttpGet]
        public IActionResult getStudents()
        {
            Students reader = new Students("data.csv");
            reader.readCSV();

            List<Student> list = reader.getExtent();

            string json = JsonSerializer.Serialize(list);

            return Ok(json);
        }

        [HttpGet("{idStudent}")]
        public IActionResult getThisStudent(string idStudent)
        {
            string json = "no such student";
            Students reader = new Students("data.csv");
            reader.readCSV();

            List<Student> list = reader.getExtent();

            Student myStudent = list.Find(i => i.IndexNumber == idStudent);
            if (myStudent == null)
            {
                return NotFound(json);
            }
            else
            {
                json = JsonSerializer.Serialize(myStudent);
                return Ok(json);
            }

        }
        [HttpPut]

        public IActionResult createStudent([FromBody] Student student)
        {
            Students reader = new Students("data.csv");
            reader.readCSV();
            if (student != null)
            {
                bool test = reader.AddStudent(student);
                if (test)
                    return Ok("student added: " + JsonSerializer.Serialize(student));
                else return BadRequest("student id taken or invalid data");
            }
            else return NotFound("Wrong input");

        }
        [HttpPut("{idStudent}")]
        public IActionResult modifyStudent([FromBody] Student student, string idStudent)
        {
            Students reader = new Students("data.csv");
            reader.readCSV();
            List<Student> list = reader.getExtent();


            Student myStudent = list.Find(i => i.IndexNumber == idStudent);
            if (myStudent == null)
            {
                return NotFound("no such id in the database");
            }
            else
            {
                reader.modifyStudent(student, idStudent);
                return Ok("student modified: " + JsonSerializer.Serialize(student));
            }

        }


        [HttpDelete("{idStudent}")]
        public IActionResult removeStudent(string idStudent)
        {
            Students reader = new Students("data.csv");
            reader.readCSV();
            List<Student> list = reader.getExtent();

            Student myStudent = list.Find(i => i.IndexNumber == idStudent);
            if (myStudent == null)
            {
                return NotFound("no such id in the database");
            }
            else
            {
                bool test = reader.deleteStudent(myStudent);
                if (test)
                    return Ok("student deleted: " + JsonSerializer.Serialize(myStudent));
                else return NotFound("student id appears to be invalid");
            }
        }


    }
}

