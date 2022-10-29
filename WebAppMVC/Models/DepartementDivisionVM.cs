namespace WebAppMVC.Models
{
    public class DepartementDivisionVM
    {
        public DepartementDivisionVM(string depName, int divId, string divName)
        {
            DepName = depName;
            DivId = divId;
            DivName = divName;
        }

        public string DepName { get; set; }
        public int DivId { get; set; }
        public string DivName { get; set; }

    }
}
