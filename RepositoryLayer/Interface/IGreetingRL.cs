using ModelLayer.Model;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interface
{
    public interface IGreetingRL
    {
        public GreetingModel GetGreetingById(int id);
        public GreetingEntity SaveGreeting(GreetingModel greetingModel);

        public List<GreetingModel> GetAllGreetings();

    }
}