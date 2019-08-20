using Dapper.Contrib.Extensions;

namespace DAL.Template.Entities
{
    [Table("foo")]
    public class Foo : Entity
    {
        /// <summary>
        /// 
        /// </summary>
        public long field { get; set; }

    }
}
