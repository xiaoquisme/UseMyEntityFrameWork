using System.ComponentModel.DataAnnotations;

namespace TestMyEntityFramework.Models
{
    public class Users
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "不能为空")]
        public string Name { get; set; }
        public string pwd { get; set; }
    }
}
 