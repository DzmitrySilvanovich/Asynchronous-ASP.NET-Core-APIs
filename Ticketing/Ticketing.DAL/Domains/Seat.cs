using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ticketing.DAL.Domain
{
    public class Seat
    {
        public int Id { get; set; }
        public int RowNumber { get; set; }
        public int SeatNumber { get; set; }
        public int SeatStatusId { get; set; }
        public SeatStatus? SeatStatus { get; set; }
        public int SectionId { get; set; }
        public Section? Section { get; set; }
    }
}
