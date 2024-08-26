using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace WpfApp1.Models
{
    public class Movie
    {
        private static int counter = 0;
        public Movie()
        {
            // the counter will increase by 1 every time a new movie is made
            Id = ++counter; 
        }

        public int Id { get; set; }
        
        public string Title { get; set; }

        public string Director { get; set; }

        public int Year { get; set; }

        public string Genre { get; set; }

        public bool IsSelected { get; set; }


    }
   
}
