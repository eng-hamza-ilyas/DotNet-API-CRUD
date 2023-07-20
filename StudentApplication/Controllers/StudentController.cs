using Microsoft.AspNetCore.Mvc;

using HelpingLibrary;


namespace StudentApplication.Controllers
{
    public class StudentController : ControllerBase

        

    {

        HelpingClass obj = new HelpingClass();
    
    

        [HttpGet("GetStudent")]
        public List<Person> Gets()
        {


            List <Person> response = obj.GetAllEmployee();
            return response;


        }

        [HttpPost("PostStudent")]
        public String Save(Person person)
        {
            var response = obj.SavePerson(person);
            return response;
        }

        [HttpGet("GetById")]

        public Person GetById(Guid id)
        {
            var response = obj.getbyId(id);
            return response;
        }


        [HttpDelete("DeleteStudent")]
        public bool DeleteStudent(Guid id)
        {
            bool response = obj.DeletePerson(id);
            return response;
        }

        [HttpPut("UpdateStudent")]
        public bool UpdateStudent(Person person)
        {
            bool response = obj.UpdatePerson(person);
            return response;
        }


    }
}
