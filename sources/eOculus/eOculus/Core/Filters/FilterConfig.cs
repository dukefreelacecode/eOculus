namespace eOculus.Core.Filters
{
    public class FilterConfig
    {
        public static readonly FilterConfig Config1 = new FilterConfig(new int[3, 3] { { 1, 2, 1 }, { 2, 4, 2 }, { 1, 2, 1 } }, 16);

        private int[,] mask;
        private int k;

        public FilterConfig(int[,] mask, int k)
        {
            this.mask = mask;
            this.k = k;
        }

        public int[,] Mask
        {
            get { return mask; }
        }

        public int K
        {
            get { return k; }
        }
    }
}
