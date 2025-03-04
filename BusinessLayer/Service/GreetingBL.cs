using System;
using BusinessLayer.Interface;

namespace HelloGreetingApplication.BusinessLayer
{
    public class GreetingBL : IGreetingBL
    {
        public string GetGreeting(string firstName = "", string lastName = "")
        {
           
            if (!string.IsNullOrEmpty(firstName) && !string.IsNullOrEmpty(lastName))
            {
                return $"Hello {firstName} {lastName}";
            }
            
            else if (!string.IsNullOrEmpty(firstName))
            {
                return $"Hello {firstName}";
            }
          
            else if (!string.IsNullOrEmpty(lastName))
            { 
                return $"Hello {lastName}";
            }
           
            else
            {
                return "Hello World";
            }
        }
    }
}