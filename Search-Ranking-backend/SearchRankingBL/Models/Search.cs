namespace SearchRankingBL
{
    /// <summary>
    /// Data class used for performing searches
    /// </summary>
    public class Search
    {
        public string SearchTerm { get; set; }
        public int SearchTermID { get; set; }
        public uint MaxResults { get; set; } = 100;
        public string[] URLs { get; set; }
    }
}
