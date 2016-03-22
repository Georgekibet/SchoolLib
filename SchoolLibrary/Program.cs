using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SchoolLibrary
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter message In this format:");
            Console.WriteLine("Reg no: CS/20/2016, booK: PE 1478 S65 1991," +
                              " DATE: 21/01/2016-30/01/2016Reg no: CS/20/2016, " +
                              "booK: PE 1478 S65 1991, DATE: 21/01/2016-30/01/20");
            Console.WriteLine("Or");
            Console.WriteLine("CS/20/2016,PE 1478 S65 1991," +
                              "21/01/2016-30/01/2016Reg no: CS/20/2016, " +
                              "PE 1478 S65 1991,21/01/2016-30/01/20");
         string message=Console.ReadLine();
            if (Isvalid(message))
            {
             // Console.WriteLine("message is valid");  
                BorrowBook borrowBook = InterPretMessage(message);

                

                var json = JsonConvert.SerializeObject(borrowBook);

                Console.WriteLine("Output will be:");
                Console.WriteLine(json);




            }
                
            else Console.WriteLine("message is Invalid");
            while (true)
            {
                 Console.ReadLine();
            }
           
        }

        private static BorrowBook InterPretMessage(string message)
        {
            var dataInMessage = message.Split(',').ToList();
            var studentReg = dataInMessage.ElementAt(0).ToLower().Replace("reg no:", "");
            var bookNumber = dataInMessage.ElementAt(1).ToLower().Replace("book:","");
           

            var dates = GetDates(dataInMessage.ElementAt(2));

            return  new BorrowBook()
            {BookNumber = bookNumber,
            StudentReg = studentReg,
            EndDate = dates.FirstOrDefault(),
            StartDate = dates.LastOrDefault(),
            Id = Guid.NewGuid()
                
            };
        }

        private static List<DateTime> GetDates(string date)
        {
            DateTime dateTimeS, dateTimeE;
            List<DateTime> datesToReturn=new List<DateTime>();
            var dataInMessage = date.Split('-').ToList();
            if (dataInMessage.Count == 2)
            {
              
                    
                    
                    if(DateTime.TryParse(dataInMessage.FirstOrDefault().ToLower().Replace("date:",""), out dateTimeS))
                    datesToReturn.Add(dateTimeS);
                    if(DateTime.TryParse(dataInMessage.LastOrDefault(), out dateTimeE))
                    datesToReturn.Add(dateTimeE);

                if (datesToReturn.Count == 2)
                    return datesToReturn;
                


            }
            
            return new List<DateTime>();
        } 
        private static bool Isvalid(string message)
        {

            if (message.Split(',').ToList().Count < 3)
                return false;

            return true;
        }



    }

    class BorrowBook
    {
        public  string StudentReg { get; set; }
        public string BookNumber { get; set; }

        public Guid Id { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime StartDate { get; set; }


    }
}
