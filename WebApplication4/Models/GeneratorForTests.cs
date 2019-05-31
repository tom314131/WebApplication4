using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication4.Models
{
    public class GeneratorForTests
    {
        int counter;

        private static GeneratorForTests instane = null;
        public static GeneratorForTests Instance
        {
            get
            {
                if (instane == null)
                {
                    instane = new GeneratorForTests();
                    instane.counter = 1;
                }

                return instane;
            }
        }

        public double[] newPoint
        {
            get
            {
                double[] point = new double[2];
                point[0] = counter * 1.12 - 30;
                point[1] = counter * 1.18 - 30;
                counter++;
                return point;
            }
        }
    }
}