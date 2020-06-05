using System;
using System.Collections.Generic;
using System.Text;

namespace ConnectMedia.Common.Model
{
    public class PlaylistBuilding: BaseModel
    {
        public int Id { get; set; }
        public int PlaylistId { get; set; }
        public int BuildingId { get; set; }
    }
}
