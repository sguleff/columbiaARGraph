using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AR.Core.Types
{
    public static class GraphConfiguration
    {
        public static float EDGE_WIDTH = .10f;
        public static Int32 GRAPHBOUNDINGBOX_XMIN = -20;
        public static Int32 GRAPHBOUNDINGBOX_XMAX = 20;
        public static Int32 GRAPHBOUNDINGBOX_YMIN = 0;
        public static Int32 GRAPHBOUNDINGBOX_YMAX = 20;
        public static Int32 GRAPHBOUNDINGBOX_ZMIN = -20;
        public static Int32 GRAPHBOUNDINGBOX_ZMAX = 20;
        public static float FORCE_DIRECTED_REPULSIVE_CONST = 10000;
        public static float FORCE_DIRECTED_ATTRACTIVE_CONST = .1f;
        public static float FORCE_DIRECTED_SPRING_LEN = 100;
        public static Int32 FORCE_DIRECTED_ITERATIONS = 75;
        public static float FORCE_DIRECTED_DEFAULT_DAMPING = 0.5f;

    }
}
