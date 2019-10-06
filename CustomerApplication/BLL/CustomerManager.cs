using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerApplication.Repository;
using System.Data;
using CustomerApplication.Model;

namespace CustomerApplication.BLL
{
    public class CustomerManager
    {
        CustomerRepository _CR = new CustomerRepository();
        public DataTable ComboLoad()
        {
            return _CR.ComboLoad();
        }

        public List<DistrictModel> ComboLoadByList()
        {
            return _CR.ComboLoadByList();
        }

        public bool IsCodeUnique(string Code, int CustId)
        {
            return _CR.IsCodeUnique(Code, CustId);
        }

        public bool IsContactUnique(string Contact, int CustId)
        {
            return _CR.IsContactUnique(Contact, CustId);
        }

        public bool Insert(Customer C)
        {
            return _CR.Insert(C);
        }

        public DataTable ShowData()
        {
            return _CR.ShowData();
        }

        public bool Update(Customer c)
        {
            return _CR.Update(c);
        }

        public DataTable Search(string Code)
        {
            return _CR.Search(Code);
        }
       
    }
}
