namespace Decorator.Instagramock
{
    public static class PictureExtensions
    {
        public static IPicture ApplyGrayScale(this IPicture self)
        {
            self = new GrayScaleFilter(self);
            return self;
        }
        public static IPicture ApplyTouchUp(this IPicture self)
        {
            self = new TouchUpFilter(self);
            return self;
        }

        public static IPicture AddEmoji(this IPicture self)
        {
            self = new EmojiFilter(self);
            return self;
        }
    }
}