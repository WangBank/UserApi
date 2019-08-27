using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserApi.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Company { get; set; }
        public string Title { get; set; }//职位
        public string Phone { get; set; }
        public string Avatar { get; set; }
        public int Gender { get; set; }

        public string Address { get; set; }
        public string Email { get; set; }
        public string Tel { get; set; }
        public string ProvinceId { get; set; }

        public string Province { get; set; }
        public string CityId { get; set; }
        public string City { get; set; }
        public string NameCard { get; set; }

        /// <summary>
        /// 不可以单独查询，UserTag和BPFile则不是
        /// </summary>
        public List<UserProperty> Properties  { get; set; }

    }
}
