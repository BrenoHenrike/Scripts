namespace SkuaGoogleForm
{
    public class GoogleFormResponse
    {
        public static string Hash;
        public string Key, Value;
        public FormUrlEncodedContent Content; 

        public List<GoogleFormResponse> BodyValue = new List<GoogleFormResponse>();

        protected GoogleFormResponse() {}

        public GoogleFormResponse(string key, string value)
        {
            Key = key;
            Value = value;
        }

        public void addFormBodyValue(string entryKey, string? entryValue)
        {
            Key = entryKey;
            Value = entryValue;
        }

        public void FormUrlEncodedContent()
        {
            Content = new FormUrlEncodedContent(BodyValue.Select(x => new KeyValuePair<string, string>(x.Key, x.Value)));
        }

        public async Task<HttpResponseMessage> SendPostAsync() 
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://docs.google.com/forms/d/e/");
                return await client.PostAsync($"{Hash}/formResponse", Content);
            }
        }

        public static GoogleFormResponseBuilder Create(string hash) 
        {
            Hash = hash;
            return new GoogleFormResponseBuilder(new GoogleFormResponse());
        }
    }
}