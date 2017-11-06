using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [PrimaryKey("Id")]
    public class Company:BaseModel
    {
        [Cellphone]
      //  [Email]
        [Length(MinLength =10,MaxLength =20)]
        [Required]
        public string Name { get; set; }
        public DateTime CreateTime { get; set; }

        public int? CreatorId { get; set; }

        public int? LastModifierId { get; set; }

        public DateTime? LastModifyTime { get; set; }

        [PropReplaceAttribute("state")]
        public int status { get; set; }
    }
}
