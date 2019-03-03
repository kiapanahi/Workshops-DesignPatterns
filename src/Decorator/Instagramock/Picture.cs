using System.Collections.Generic;
using System.Linq;

namespace Decorator.Instagramock
{
    public interface IPicture
    {
        List<string> Filters { get; set; }
        double SizeOnDisk { get; set; }
    }

    public class RawPicture : IPicture
    {
        public RawPicture(double size)
        {
            Filters = new List<string>();
            SizeOnDisk = size;
        }

        public List<string> Filters { get; set; }
        public double SizeOnDisk { get; set; }
    }

    public class SepiaFilter : IPicture
    {
        public SepiaFilter(IPicture picture)
        {
            Filters = picture.Filters.Union(new[] { "Sepia" }).ToList();
            SizeOnDisk = picture.SizeOnDisk + picture.SizeOnDisk * .4;
        }

        public List<string> Filters { get; set; }
        public double SizeOnDisk { get; set; }
    }

    public class GrayScaleFilter : IPicture
    {
        public GrayScaleFilter(IPicture picture)
        {
            Filters = picture.Filters.Union(new []{"Gray-Scale"}).ToList();
            SizeOnDisk = picture.SizeOnDisk + picture.SizeOnDisk*.2;
        }

        public List<string> Filters { get; set; }
        public double SizeOnDisk { get; set; }
    }

    public class RedEmphasizeFilter : IPicture
    {
        public RedEmphasizeFilter(IPicture picture)
        {
            Filters = picture.Filters.Union(new[] { "Red-Emphasized" }).ToList();
            SizeOnDisk = picture.SizeOnDisk + picture.SizeOnDisk * .4;
        }

        public List<string> Filters { get; set; }
        public double SizeOnDisk { get; set; }
    }

    public class BlueEmphasizeFilter : IPicture
    {
        public BlueEmphasizeFilter(IPicture picture)
        {
            Filters = picture.Filters.Union(new[] { "Blue-Emphasized" }).ToList();
            SizeOnDisk = picture.SizeOnDisk + picture.SizeOnDisk * .45;
        }

        public List<string> Filters { get; set; }
        public double SizeOnDisk { get; set; }
    }

    public class TouchUpFilter : IPicture
    {
        public TouchUpFilter(IPicture picture)
        {
            Filters = picture.Filters.Union(new[] { "Touch-Up" }).ToList();
            SizeOnDisk = picture.SizeOnDisk + picture.SizeOnDisk * .8;
        }

        public List<string> Filters { get; set; }
        public double SizeOnDisk { get; set; }
    }

}