using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MX_Simulator_Track_Scaler
{
    public static class Enums
    {
        public enum TrackFileTypes
        { 
            TimingGates,
            Gradients,
            Flaggers,
            Decals,
            Statues,
            Billboards
        }

        public static readonly Dictionary<TrackFileTypes, string> TrackFileNamesMap = new Dictionary<TrackFileTypes, string>{
            { TrackFileTypes.TimingGates, "timing_gates" },
            { TrackFileTypes.Gradients, "edinfo" },
            { TrackFileTypes.Flaggers, "flaggers" },
            { TrackFileTypes.Decals, "decals" },
            { TrackFileTypes.Statues, "statues" },
            { TrackFileTypes.Billboards, "billboards" }
        };
    }
}
