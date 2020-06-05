using System;
using System.Collections.Generic;
using System.Text;

namespace ConnectMedia.Common.DTO
{
    public class PlaylistDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<int> BuildingId { get; set; }
        public int entryBy { get; set; }

    }
}
