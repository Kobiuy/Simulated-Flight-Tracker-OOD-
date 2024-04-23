using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOD_Proj_1
{
    public static class Utils
    {
        public static double GetProgress(int toftime, int lndtime, int nwtime)
        {
            double a, b;

            if (toftime > nwtime && lndtime > toftime) return 0;
            if (toftime > nwtime && lndtime < toftime && lndtime < nwtime) return 1;
            if (toftime < nwtime && lndtime < nwtime && lndtime > toftime) return 1;

            if (toftime > lndtime)
            {
                if (nwtime < toftime)
                {
                    lndtime += 24 * 60 * 60; // Add one day
                    nwtime += 24 * 60 * 60; // Add one day
                }
                else
                    lndtime += 24 * 60 * 60; // Add one day
            }
            a = nwtime - toftime;
            b = lndtime - toftime;
            double progress = a / b;

            return progress;
        }
    }
}
