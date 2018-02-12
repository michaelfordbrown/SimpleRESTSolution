using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleRESTServer.Models
{
    /// <summary>
    /// Person Class {ID, Last Name, First Name, Pay Rate, Start Date and End Date}
    /// </summary>
    public class Person
    {
        /// <summary>
        /// ID - Primary key
        /// </summary>
        public long ID { get; set; }
        /// <summary>
        /// LastName - Surename (string)
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// FirstName - Given name (string)
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// PayRate - Daily rate of pay (float)
        /// </summary>
        public Double PayRate { get; set; }
        /// <summary>
        /// StartDate - First date of employment
        /// </summary>
        public DateTime StartDate { get; set; }
        /// <summary>
        /// LastDate - Last date of employment
        /// </summary>
        public DateTime EndDate { get; set; }
        
    }
}