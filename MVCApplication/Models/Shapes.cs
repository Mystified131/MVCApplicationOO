using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCApplication.Models
{

    public class Shape
    {
        public string Name { get; set; }



        public Shape(string name)
        {
            Name = name;

        }

    }

    public class Cube: Shape
    {

        public double Sidelength { get; set; } 


        public Cube(string Name, double sidelength) : base(Name)
        {
            Sidelength = sidelength;

        }

        public double Volume(double Sidelength)
        {

            double Vol = Sidelength * Sidelength * Sidelength;

            return Vol;

        }

        public double Surfacearea(double Sidelength)
        {

            double Sar = Sidelength * Sidelength * 6;

            return Sar;

        }
    }

    public class Square: Shape
    {

        public double Sidelength { get; set; }



        public Square(string Name, double sidelength) : base(Name)
        {
            Sidelength = sidelength;

        }

    }

    public class Segment: Shape
    {

        public double Sidelength { get; set; }



        public Segment(string Name, double sidelength) : base(Name)
        {
            Sidelength = sidelength;

        }

    }
}