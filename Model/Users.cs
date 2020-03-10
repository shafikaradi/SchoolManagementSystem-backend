using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolWebServiceWebAPO
{
    public class Users
    {

       [Key]
       public int ID { set; get; }
       public string FName { get; set; }
       public string LName { get; set; }
       public string UserName { get; set; }
       public string PASSWORD { get; set; }
       public string MobileNo { get; set; }
    }
}