using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HelpingLibrary
{
    public class HelpingClass
    {
        static string clientId = "1f1ea975-58e3-4148-a5d6-cef223781e1b";
        static string clientSecret = "Noi8Q~desg~sig2sEJSasFkaQuoBNw2i~lDs7aPg";
        static string baseURL = "https://orgcc4de934.crm4.dynamics.com";

        public static ServiceClient _service = new ServiceClient($"AuthType={"ClientSecret"};url={baseURL};ClientId={clientId};ClientSecret={clientSecret}");

        List<Person> personlist = new List<Person>();  
        
        public List<Person> GetAllEmployee()
        {
            QueryExpression query = new QueryExpression
            {
                EntityName = "cr298_employee", 
                ColumnSet = new ColumnSet(true),  

            };

            QueryExpression node = new QueryExpression
            {
                
            };
            EntityCollection entitycollection = new EntityCollection();
            entitycollection = _service.RetrieveMultiple(query);
            foreach (Entity entity in entitycollection.Entities)
            {
                Person person= new Person();
                person.Name = entity.GetAttributeValue<string>("cr298_name");
                person.Blood = entity.GetAttributeValue<string>("cr298_blood");

                person.Id = entity.GetAttributeValue<Guid>("cr298_employeeid");
                personlist.Add(person); 
                
            }

            return personlist;

        }

        public Person getbyId(Guid id)
        {
            QueryExpression query = new QueryExpression
            {
                EntityName = "cr298_employee",
                ColumnSet = new ColumnSet(true),

            };
            Entity entity = new Entity();
            entity = _service.Retrieve("cr298_employee", id, new ColumnSet(true));

            Person person = new Person();
            person.Name = entity.GetAttributeValue<string>("cr298_name");
            person.Blood = entity.GetAttributeValue<string>("cr298_blood");
            person.Id = entity.GetAttributeValue<Guid>("cr298_employeeid");

            return person;

        }

        public bool DeletePerson(int id)
        {
            throw new NotImplementedException();
        }


        public string SavePerson(Person person)
        {
            if (string.IsNullOrEmpty(person.Name))
            {
                Console.WriteLine("false");
                return "Please enter your correct information";
            }
            else
            {
                Entity entity = new Entity("cr298_employee");

                entity["cr298_name"] = person.Name;
                entity["cr298_blood"] = person.Blood;
                var isValid = Regex.IsMatch(person.Name, @"^[a-zA-Z]+$");
                
                if (!isValid)
                {
                    return "Invalid Format";
                }
                var bValid = Regex.IsMatch(person.Blood, @"^[a-zA-Z]+-$");
                if (!bValid)
                {
                    return "Invalid Format";
                }
                var id = _service.Create(entity);

                Console.WriteLine(id);
                return "Your record has been added against this id = "+ id.ToString();   
            }

          
          
        }

        public bool DeletePerson(Guid id)
        {
            string EntityName = "cr298_employee";
            
            var response =  _service.DeleteAsync(EntityName, id);
            if (response.IsCompletedSuccessfully)
            {
                return true;
            }
            else
            {
                return false;
            }

            //Hi this is for prac

        }
        public bool UpdatePerson(Person person)
        {
           
            if (person == null)
            {
                return false;
            }
            else
            {
               Entity entity = _service.Retrieve("cr298_employee", person.Id, new ColumnSet(true));
                entity["cr298_name"] = person.Name;
                entity["cr298_blood"] = person.Blood;
                _service.Update(entity);

                return true;
            }
            

        }

    }
    public class Person
    {
        public string Name { get; set; }
        public string Blood { get; set; }

        public Guid Id { get; set; }

        internal int FindIndex(Func<object, bool> value)
        {
            throw new NotImplementedException();
        }
    }
}
