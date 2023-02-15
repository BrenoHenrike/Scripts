namespace SkuaGoogleForm
{
    public class GoogleFormResponseBuilder
    {
        protected GoogleFormResponse _root;
        public GoogleFormResponseBuilder(GoogleFormResponse googleFormResponse)
        {
            _root = googleFormResponse;
        }

        public GoogleFormResponseBuilder AddBodyValue(string entryKey, string? entryValue)
        {
            _root.addFormBodyValue(entryKey, entryValue);

            return this;
        }
        
        public GoogleFormResponse FormUrlEncodedContent()
        {
            _root.FormUrlEncodedContent();
            return _root;
        }
    }
}