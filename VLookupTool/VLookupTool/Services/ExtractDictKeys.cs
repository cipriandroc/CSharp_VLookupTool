namespace VLookupTool.Services
{
    public static class ExtractDictKeys
    {

        public static List<string> Execute(Dictionary<string, string> dict)
        {
            List<string> list = new List<string>();

            foreach (string key in dict.Keys)
            {
                list.Add(key);
            }

            return list;
        }
    }
}
