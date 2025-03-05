using ModelLayer.Model;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Service
{
    public class GreetingRL : IGreetingRL
    {
        HelloGreetingContext _helloGreetingContext;
        public GreetingRL(HelloGreetingContext helloGreetingContext)
        {
            _helloGreetingContext = helloGreetingContext;
        }
        public GreetingEntity SaveGreeting(GreetingModel greetingModel)
        {
            GreetingEntity greetingEntity = new GreetingEntity()
            {
                Greeting = greetingModel.GreetingMessage,
            };
            _helloGreetingContext.Greetings.Add(greetingEntity);
            _helloGreetingContext.SaveChanges();
            return greetingEntity;
        }

        public GreetingModel GetGreetingById(int id)
        {
            GreetingEntity greetingEntity = _helloGreetingContext.Greetings.Find(id);
            GreetingModel greetingModel = new GreetingModel()
            {
                GreetingMessage = greetingEntity.Greeting,
            };
            return greetingModel;
        }
    }

}