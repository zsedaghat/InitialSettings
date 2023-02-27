using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml;

namespace SpaceManagment.Model
{
    public class SeriLog
    {
        public int Id { get; set; }

        [Column("Message", TypeName = "nvarchar(max)")]
        public string Message { get; set; }

        [Column("MessageTemplate", TypeName = "nvarchar(max)")]
        public string MessageTemplate { get; set; }

        [Column("Level", TypeName = "nvarchar(128)")]
        public string Level { get; set; }

        [Column("TimeStamp", TypeName = "datetimeoffset(7)")]
        public DateTimeOffset TimeStamp { get; set; }

        [Column("Exception", TypeName = "nvarchar(max)")]
        public string Exception { get; set; }

        [Column("Properties", TypeName = "xml")]
        public string Properties { get; set; }

        [Column("LogEvent", TypeName = "nvarchar(max)")]
        public string LogEvent { get; set; }

        [Column("UserName", TypeName = "nvarchar(200)")]
        public string UserName { get; set; }

        [Column("IP", TypeName = "varchar(200)")]
        public string IP { get; set; }
    }
}
