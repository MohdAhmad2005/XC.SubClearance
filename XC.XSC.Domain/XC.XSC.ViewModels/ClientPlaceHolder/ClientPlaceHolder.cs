namespace XC.XSC.ViewModels.ClientPlaceHolder
{
    public class ClientPlaceHolder
    {
        public List<PlaceHolder> PlaceHolders { get; set; }
    }

    public class PlaceHolder
    {
        public string Field { get; set; }
        public string ColumnName { get; set; }
    }
}
