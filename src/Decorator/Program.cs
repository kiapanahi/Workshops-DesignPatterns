using System;
using Decorator.Instagramock;

namespace Decorator
{
    internal class Program
    {
        private static IPicture _samplePicture;

        private static void Main()
        {
            _samplePicture = new RawPicture(500);
            Report();

            //_samplePicture =  _samplePicture
            //    .ApplyGrayScale()
            //    .ApplyTouchUp()
            //    .AddEmoji();

            //Report();


            //_samplePicture = new GrayScaleFilter(_samplePicture);
            //Report();

            //_samplePicture = new EmojiFilter(_samplePicture);
            //Report();

            //_samplePicture = new BlueEmphasizeFilter(_samplePicture);
            //Report();

            //_samplePicture = new RedEmphasizeFilter(_samplePicture);
            //Report();

            //_samplePicture = new TouchUpFilter(_samplePicture);
            //Report();



            _samplePicture = new TouchUpFilter(
                new RedEmphasizeFilter(
                    new BlueEmphasizeFilter(
                        new GrayScaleFilter(_samplePicture)
                    )
                )
            );
            Report();
        }

        private static void Report()
        {
            var report = $@"
Filters: {string.Join(", ", _samplePicture.Filters)}
Size: {_samplePicture.SizeOnDisk} KB
===================================";
            Console.WriteLine(report);
        }
    }
}