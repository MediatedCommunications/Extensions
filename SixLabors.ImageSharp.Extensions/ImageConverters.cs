namespace SixLabors.ImageSharp {
    public static class ImageConverters {

        public static class ScreenShots {
            public static ImageConverter Default { get; private set; } = new BlackAndWhiteScreenShotImageConverter();
        }

        public static class SnapShots {
            public static ImageConverter Default { get; private set; } = new LowResolutionSnapShotImageConverter();
        }

    }

}
